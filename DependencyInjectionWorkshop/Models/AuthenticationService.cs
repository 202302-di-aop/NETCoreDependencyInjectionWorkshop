namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        private readonly FailCounter _failCounter = new FailCounter();
        private readonly OtpProxy _otpProxy = new OtpProxy();
        private readonly ProfileRepo _profileRepo = new ProfileRepo();
        private readonly Sha256Adapter _sha256Adapter = new Sha256Adapter();
        private readonly SlackAdapter _slackAdapter = new SlackAdapter();

        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            var isLocked = await new FailCounter().IsLocked(account);
            if (isLocked)
            {
                throw new FailedTooManyTimesException() { Account = account };
            }

            var passwordFromDb = _profileRepo.GetPasswordFromDb(account);
            var hashResult = _sha256Adapter.GetHashResult(password);
            var currentOtp = await _otpProxy.GetCurrentOtp(account, new HttpClient() { BaseAddress = new Uri("http://joey.com/") });
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                await _failCounter.Reset(account, new HttpClient() { BaseAddress = new Uri("http://joey.com/") });
                return true;
            }
            else
            {
                //失敗
                await _failCounter.AddFailCount(account, new HttpClient() { BaseAddress = new Uri("http://joey.com/") });
                await LogFailCount(account, new HttpClient() { BaseAddress = new Uri("http://joey.com/") });
                _slackAdapter.Notify(account);
                return false;
            }
        }

        private static async Task LogFailCount(string account, HttpClient httpClient)
        {
            
            var failedCount = await new FailCounter().GetFailedCount(account, httpClient);
            new NLogAdapter().Info($"accountId:{account} failed times:{failedCount}");
        }
    }

    internal class NLogAdapter
    {
        public void Info(string message)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(message);
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public string Account { get; set; }
    }
}