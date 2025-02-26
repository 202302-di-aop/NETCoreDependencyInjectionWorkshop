namespace DependencyInjectionWorkshop.Models;

public class OtpProxy
{
    public async Task<string> GetCurrentOtp(string account)
    {
        var response = await new HttpClient() { BaseAddress = new Uri("http://joey.com/") }.PostAsJsonAsync("api/otps", account);

        return await response.Content.ReadAsAsync<string>();
    }
}