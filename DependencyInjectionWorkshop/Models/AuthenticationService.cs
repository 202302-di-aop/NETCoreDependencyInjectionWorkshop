using SlackAPI;

namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        private readonly ProfileRepo _profileRepo = new ProfileRepo();
        private readonly Sha256Adapter _sha256Adapter = new Sha256Adapter();

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
            var currentOtp = await GetCurrentOtp(account, httpClient);
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                await ResetFailCount(account, httpClient);
                return true;
            }
            else
            {
                //失敗
                await AddFailCount(account, httpClient); 
                await LogFailCount(account, httpClient); 
                Notify(account); 
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

        private static async Task AddFailCount(string account, HttpClient httpClient)
        {
            var addFailedCountResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Add", account);
            addFailedCountResponse.EnsureSuccessStatusCode();
        }

        private static async Task<string> GetCurrentOtp(string account, HttpClient httpClient)
        {
            var response = await httpClient.PostAsJsonAsync("api/otps", account);

            return await response.Content.ReadAsAsync<string>();
        }

        private static void Notify(string account)
        {
            var message = $"{account} try to login fail.";
            var slackClient = new SlackClient("my api token");
            slackClient.PostMessage(response1 => { }, "my channel", message, "my bot name");
        }

        private static async Task ResetFailCount(string account, HttpClient httpClient)
        {
            var resetResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Reset", account);
            resetResponse.EnsureSuccessStatusCode();
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public string Account { get; set; }
    }
}