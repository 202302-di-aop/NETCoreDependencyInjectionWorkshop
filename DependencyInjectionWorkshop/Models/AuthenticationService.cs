using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using SlackAPI;

namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://joey.com/") };
            var isLockedResponse = await httpClient.PostAsJsonAsync("api/failedCounter/IsLocked", account);

            isLockedResponse.EnsureSuccessStatusCode();
            if (await isLockedResponse.Content.ReadAsAsync<bool>())
            {
                throw new FailedTooManyTimesException() { Account = account };
            }

            var passwordFromDb = GetPasswordFromDb(account);

            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            var hashResult = hash.ToString();

            var response = await httpClient.PostAsJsonAsync("api/otps", account);

            var currentOtp = await response.Content.ReadAsAsync<string>();
            if (passwordFromDb == hashResult && otp == currentOtp)
            {
                var resetResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Reset", account);
                resetResponse.EnsureSuccessStatusCode();
                return true;
            }
            else
            {
                //失敗
                var addFailedCountResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Add", account);
                addFailedCountResponse.EnsureSuccessStatusCode();

                var failedCountResponse =
                    await httpClient.PostAsJsonAsync("api/failedCounter/GetFailedCount", account);

                failedCountResponse.EnsureSuccessStatusCode();

                var failedCount = await failedCountResponse.Content.ReadAsAsync<int>();
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info($"accountId:{account} failed times:{failedCount}");

                var message = $"{account} try to login fail.";
                var slackClient = new SlackClient("my api token");
                slackClient.PostMessage(response1 => { }, "my channel", message, "my bot name");

                return false;
            }
        }

        private static string GetPasswordFromDb(string account)
        {
            string passwordFromDb;
            using (var connection = new SqlConnection("my connection string"))
            {
                passwordFromDb = connection.Query<string>("spGetUserPassword", new { Id = account },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

            return passwordFromDb;
        }
    }

    public class FailedTooManyTimesException : Exception
    {
        public string Account { get; set; }
    }
}