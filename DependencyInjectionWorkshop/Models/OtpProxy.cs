namespace DependencyInjectionWorkshop.Models;

public interface IOtpProxy
{
    Task<string> GetCurrentOtp(string account);
}

public class OtpProxy : IOtpProxy
{
    public async Task<string> GetCurrentOtp(string account)
    {
        var response = await new HttpClient() { BaseAddress = new Uri("http://joey.com/") }.PostAsJsonAsync("api/otps", account);

        return await response.Content.ReadAsAsync<string>();
    }
}