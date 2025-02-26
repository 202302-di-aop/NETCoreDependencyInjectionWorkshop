namespace DependencyInjectionWorkshop.Models;

public class OtpProxy
{
    public async Task<string> GetCurrentOtp(string account, HttpClient httpClient)
    {
        var response = await httpClient.PostAsJsonAsync("api/otps", account);

        return await response.Content.ReadAsAsync<string>();
    }
}