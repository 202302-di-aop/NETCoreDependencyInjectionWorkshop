using DependencyInjectionWorkshop.Models;

namespace DependencyInjectionWorkshopTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void is_valid()
        {
            var authenticationService = new AuthenticationService();
            // string account;
            // string password;
            // string otp;
            // authenticationService.IsValid(account, password, otp)
            Assert.Inconclusive();
        }
    }
}