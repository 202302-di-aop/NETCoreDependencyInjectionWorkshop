namespace DependencyInjectionWorkshop.Models;

public class FailCounterDecorator : IAuthentication
{
    private readonly IAuthentication _authentication;
    private readonly IFailCounter _failCounter;

    public FailCounterDecorator(IAuthentication authentication, IFailCounter failCounter)
    {
        _authentication = authentication;
        _failCounter = failCounter;
    }

    public async Task<bool> IsValid(string account, string password, string otp)
    {
        var isValid = await _authentication.IsValid(account, password, otp);
        if (isValid)
        {
            await _failCounter.Reset(account);
        }
        else
        {
            await AddFailCount(account);
        }

        return isValid;
    }

    public Task AddFailCount(string account)
    {
        return _failCounter.AddFailCount(account);
    }

    public Task<int> GetFailedCount(string account)
    {
        return _failCounter.GetFailedCount(account);
    }

    public Task<bool> IsLocked(string account)
    {
        return _failCounter.IsLocked(account);
    }
}