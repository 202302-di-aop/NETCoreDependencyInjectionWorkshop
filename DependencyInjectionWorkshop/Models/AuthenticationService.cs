namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        private readonly IFailCounter _failCounter;
        private readonly IHash _hash;
        private readonly INotification _notification;
        private readonly IOtpProxy _otpProxy;
        private readonly IProfileRepo _profileRepo;
        private readonly IMyLogger _logger;

        public AuthenticationService(IFailCounter failCounter, IHash hash, INotification notification, IOtpProxy otpProxy, IProfileRepo profileRepo, IMyLogger logger)
        {
            _failCounter = failCounter;
            _hash = hash;
            _notification = notification;
            _otpProxy = otpProxy;
            _profileRepo = profileRepo;
            _logger = logger;
        }

        public AuthenticationService()
        {
            _failCounter = new FailCounter();
            _profileRepo = new ProfileRepo();
            _hash = new Sha256Adapter();
            _otpProxy = new OtpProxy();
            _notification = new SlackAdapter();
            _logger = new NLogAdapter();
        }

        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            var isLocked = await _failCounter.IsLocked(account);
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