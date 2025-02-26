namespace DependencyInjectionWorkshop.Models;

public class FailCounter
{
    private HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("http://joey.com/") };

    public async Task AddFailCount(string account)
    {
        var addFailedCountResponse =
            await _httpClient.PostAsJsonAsync(
                "api/failedCounter/Add", account);
        addFailedCountResponse.EnsureSuccessStatusCode();
    }

    public async Task<int> GetFailedCount(string account)
    {
        var failedCountResponse =
            await _httpClient.PostAsJsonAsync(
                "api/failedCounter/GetFailedCount", account);

        failedCountResponse.EnsureSuccessStatusCode();

        var failedCount = await failedCountResponse.Content.ReadAsAsync<int>();
        return failedCount;
    }

    public async Task<bool> IsLocked(string account)
    {
        var isLockedResponse =
            await _httpClient.PostAsJsonAsync(
                "api/failedCounter/IsLocked", account);

        isLockedResponse.EnsureSuccessStatusCode();
        return await isLockedResponse.Content.ReadAsAsync<bool>();
    }

    public async Task Reset(string account)
    {
        var resetResponse =
            await _httpClient.PostAsJsonAsync(
                "api/failedCounter/Reset", account);
        resetResponse.EnsureSuccessStatusCode();
    }
}