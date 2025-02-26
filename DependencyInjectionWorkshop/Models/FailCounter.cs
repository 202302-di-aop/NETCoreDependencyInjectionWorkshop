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
}