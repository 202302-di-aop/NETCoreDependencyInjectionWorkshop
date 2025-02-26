using DependencyInjectionWorkshop.Models;
using NSubstitute;

namespace DependencyInjectionWorkshopTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService _authenticationService;
        private IFailCounter _failCounter;
        private IHash _hash;
        private IMyLogger _myLogger;
        private INotification _notification;
        private IOtpProxy _otpProxy;
        private IProfileRepo _profileRepo;

        [SetUp]
        public void SetUp()
        {
            _failCounter = Substitute.For<IFailCounter>();
            _hash = Substitute.For<IHash>();
            _notification = Substitute.For<INotification>();
            _otpProxy = Substitute.For<IOtpProxy>();
            _profileRepo = Substitute.For<IProfileRepo>();
            _myLogger = Substitute.For<IMyLogger>();
            _authenticationService =
                new AuthenticationService(_failCounter, _hash, _notification, _otpProxy, _profileRepo, _myLogger);
        }

        [Test]
        public async Task is_valid()
        {
            GivenPasswordFromDb("joey", "hashed_abc");
            GivenHashResult("abc", "hashed_abc");
            GivenCurrentOtp("joey", "123456");

            await ShouldBeValid("joey", "abc", "123456");
        }

        [Test]
        public async Task reset_fail_count_when_valid()
        {
            await WhenValid("joey");
            await _failCounter.Received(1).Reset("joey");
        }

        [Test]
        public async Task when_account_is_locked()
        {
            _failCounter.IsLocked("joey").Returns(true);
            GivenPasswordFromDb("joey", "hashed_abc");
            GivenHashResult("abc", "hashed_abc");
            GivenCurrentOtp("joey", "123456");

            ShouldThrow<FailedTooManyTimesException>();
        }

        [Test]
        public async Task is_invalid()
        {
            GivenPasswordFromDb("joey", "hashed_abc");
            GivenHashResult("abc", "hashed_abc");
            GivenCurrentOtp("joey", "123456");

            await ShouldBeInvalid("joey", "abc", "wrong otp");
        }

        [Test]
        public async Task add_fail_count_when_invalid()
        {
            await WhenInvalid("joey");
            await ShouldAddFailCount("joey");
        }

        [Test]
        public async Task should_notify_user_when_invalid()
        {
            await WhenInvalid("joey");
            _notification.Received(1).Notify(Arg.Is<string>(s => s.Contains("joey")));
        }

        [Test]
        public async Task should_log_fail_count_when_invalid()
        {
            await WhenInvalid("joey");
            _myLogger.Received(1).Info(Arg.Is<string>(s => s.Contains("joey")));
        }

        private void ShouldThrow<TException>() where TException : Exception
        {
            AsyncTestDelegate action = async () => await _authenticationService.IsValid("joey", "abc", "123456");
            Assert.ThrowsAsync<TException>(action);
        }

        private async Task ShouldAddFailCount(string account)
        {
            await _failCounter.Received(1).AddFailCount(account);
        }

        private async Task WhenInvalid(string account)
        {
            GivenPasswordFromDb(account, "hashed_abc");
            GivenHashResult("abc", "hashed_abc");
            GivenCurrentOtp(account, "123456");

            await _authenticationService.IsValid(account, "abc", "wrong otp");
        }

        private async Task WhenValid(string account)
        {
            GivenPasswordFromDb("joey", "hashed_abc");
            GivenHashResult("abc", "hashed_abc");
            GivenCurrentOtp("joey", "123456");

            await _authenticationService.IsValid(account, "abc", "123456");
        }

        private async Task ShouldBeInvalid(string account, string password, string wrongOtp)
        {
            var isValid = await _authenticationService.IsValid(account, password, wrongOtp);
            Assert.IsFalse(isValid);
        }

        private async Task ShouldBeValid(string account, string password, string otp)
        {
            var isValid = await _authenticationService.IsValid(account, password, otp);
            Assert.IsTrue(isValid);
        }

        private void GivenCurrentOtp(string account, string currentOtp)
        {
            _otpProxy.GetCurrentOtp(account).Returns(currentOtp);
        }

        private void GivenHashResult(string password, string hashedResult)
        {
            _hash.GetHashResult(password).Returns(hashedResult);
        }

        private void GivenPasswordFromDb(string account, string passwordFromDb)
        {
            _profileRepo.GetPasswordFromDb(account).Returns(passwordFromDb);
        }
    }
}