namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        private readonly IFailCounter _failCounter = new FailCounter();
        private readonly IHash _hash = new Sha256Adapter();
        private readonly IMyLogger _logger = new NLogAdapter();
        private readonly INotification _notification = new SlackAdapter();
        private readonly IOtpProxy _otpProxy = new OtpProxy();
        private readonly IProfileRepo _profileRepo = new ProfileRepo();

        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            var isLocked = await new FailCounter().IsLocked(account);
            if (isLocked)
            {
                throw new FailedTooManyTimesException() { Account = account };
            }

            var passwordFromDb = _profileRepo.GetPasswordFromDb(account);
            var hashResult = _hash.GetHashResult(password);
            var currentOtp = await _otpProxy.GetCurrentOtp(account);
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                await _failCounter.Reset(account);
                return true;
            }
            else
            {
                //失敗
                await _failCounter.AddFailCount(account);
                await LogFailCount(account);
                _notification.Notify(account);
                return false;
            }
        }

        private async Task LogFailCount(string account)
        {
            var failedCount = await _failCounter.GetFailedCount(account);
            _logger.Info($"accountId:{account} failed times:{failedCount}");
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public string Account { get; set; }
    }
}