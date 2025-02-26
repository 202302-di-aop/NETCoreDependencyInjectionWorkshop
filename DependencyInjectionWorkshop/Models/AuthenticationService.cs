using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace DependencyInjectionWorkshop.Models
{
    public class AuthenticationService
    {
        [Obsolete("Obsolete")]
        public async Task<bool> IsValid(string account, string password, string otp)
        {
            string passwordFromDb;
            using (var connection = new SqlConnection("my connection string"))
            {
                passwordFromDb = connection.Query<string>("spGetUserPassword", new { Id = account },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            var hashResult = hash.ToString();

            var httpClient = new HttpClient() { BaseAddress = new Uri("http://joey.com/") };
            var response = await httpClient.PostAsJsonAsync("api/otps", account);

            var currentOtp = await response.Content.ReadAsAsync<string>();
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
}