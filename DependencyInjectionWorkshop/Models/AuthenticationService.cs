namespace DependencyInjectionWorkshop.Models
{
    public interface IAuthentication
    {
        Task<bool> IsValid(string account, string password, string otp);
    }

    public class AuthenticationService : IAuthentication
    {
        private readonly IHash _hash; 
        private readonly IOtpProxy _otpProxy; 
        private readonly IProfileRepo _profileRepo;

        public AuthenticationService(IHash hash,
            IOtpProxy otpProxy, IProfileRepo profileRepo)
        {
            _hash = hash;
            _otpProxy = otpProxy;
            _profileRepo = profileRepo;
        }

        public AuthenticationService()
        {
            _profileRepo = new ProfileRepo();
            _hash = new Sha256Adapter();
            _otpProxy = new OtpProxy();
        }

        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            var passwordFromDb = _profileRepo.GetPasswordFromDb(account);
            var hashResult = _hash.GetHashResult(password);
            var currentOtp = await _otpProxy.GetCurrentOtp(account);
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public FailedTooManyTimesException()
        {
        }

        public FailedTooManyTimesException(string message) : base(message)
        {
        }

        public FailedTooManyTimesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string Account { get; set; }
    }
}