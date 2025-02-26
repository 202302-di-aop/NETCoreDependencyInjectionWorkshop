namespace DependencyInjectionWorkshop.Models
{
    public interface IAuthentication
    {
        Task<bool> IsValid(string account, string password, string otp);
    }

    public class AuthenticationService : IAuthentication
    {
        private readonly IFailCounter _failCounter;
        private readonly IHash _hash;
        private readonly IMyLogger _logger;
        private readonly IOtpProxy _otpProxy;

        private readonly IProfileRepo _profileRepo;
        private readonly FailCounterDecorator _failCounterDecorator;

        public AuthenticationService(IFailCounter failCounter, IHash hash,
            IOtpProxy otpProxy, IProfileRepo profileRepo, IMyLogger logger)
        {
            // _failCounterDecorator = new FailCounterDecorator(this);
            _failCounter = failCounter;
            _hash = hash;
            _otpProxy = otpProxy;
            _profileRepo = profileRepo;
            _logger = logger;
        }

        public AuthenticationService()
        {
            // _failCounterDecorator = new FailCounterDecorator(this);
            _failCounter = new FailCounter();
            _profileRepo = new ProfileRepo();
            _hash = new Sha256Adapter();
            _otpProxy = new OtpProxy();
            _logger = new NLogAdapter();
        }

        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            // await _failCounterDecorator.CheckAccountIsLocked(account);

            var passwordFromDb = _profileRepo.GetPasswordFromDb(account);
            var hashResult = _hash.GetHashResult(password);
            var currentOtp = await _otpProxy.GetCurrentOtp(account);
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                // await _failCounterDecorator.ResetByDecorator(account);
                return true;
            }
            else
            {
                // await _failCounter.AddFailCount(account);
                await LogFailCount(account);
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