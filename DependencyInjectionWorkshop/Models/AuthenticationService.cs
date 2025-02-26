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
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://joey.com/") };
            var isLocked = await IsLocked(account, httpClient);
            if (isLocked)
            {
                throw new FailedTooManyTimesException() { Account = account };
            }

            var passwordFromDb = _profileRepo.GetPasswordFromDb(account);
            var hashResult = _sha256Adapter.GetHashResult(password);
            var currentOtp = await _otpProxy.GetCurrentOtp(account, httpClient);
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                await _failCounter.Reset(account, httpClient);
                return true;
            }
            else
            {
                //失敗
                await _failCounter.AddFailCount(account, httpClient);
                await LogFailCount(account, httpClient);
                _slackAdapter.Notify(account);
                return false;
            }
        }

        private static async Task<bool> IsLocked(string account, HttpClient httpClient)
        {
            var isLockedResponse = await httpClient.PostAsJsonAsync("api/failedCounter/IsLocked", account);

            isLockedResponse.EnsureSuccessStatusCode();
            return await isLockedResponse.Content.ReadAsAsync<bool>();
        }

        private static async Task LogFailCount(string account, HttpClient httpClient)
        {
            var failedCountResponse =
                await httpClient.PostAsJsonAsync("api/failedCounter/GetFailedCount", account);

            failedCountResponse.EnsureSuccessStatusCode();

            var failedCount = await failedCountResponse.Content.ReadAsAsync<int>();
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info($"accountId:{account} failed times:{failedCount}");
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public string Account { get; set; }
    }
}