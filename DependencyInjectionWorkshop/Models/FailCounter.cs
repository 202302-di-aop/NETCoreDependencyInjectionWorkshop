namespace DependencyInjectionWorkshop.Models;

public class FailCounter
{
    public async Task AddFailCount(string account, HttpClient httpClient)
    {
        var addFailedCountResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Add", account);
        addFailedCountResponse.EnsureSuccessStatusCode();
    }

    public async Task Reset(string account, HttpClient httpClient)
    {
        var resetResponse = await httpClient.PostAsJsonAsync("api/failedCounter/Reset", account);
        resetResponse.EnsureSuccessStatusCode();
    }

    public async Task<bool> IsLocked(string account)
    {
        var isLockedResponse = await new HttpClient() { BaseAddress = new Uri("http://joey.com/") }.PostAsJsonAsync("api/failedCounter/IsLocked", account);

        isLockedResponse.EnsureSuccessStatusCode();
        return await isLockedResponse.Content.ReadAsAsync<bool>();
    }

    public async Task<int> GetFailedCount(string account, HttpClient httpClient)
    {
        var failedCountResponse =
            await httpClient.PostAsJsonAsync("api/failedCounter/GetFailedCount", account);

        failedCountResponse.EnsureSuccessStatusCode();

        var failedCount = await failedCountResponse.Content.ReadAsAsync<int>();
        return failedCount;
    }
}