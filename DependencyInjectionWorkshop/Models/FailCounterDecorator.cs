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
        var isLocked = await _failCounter.IsLocked(account);
        if (isLocked)
        {
            throw new FailedTooManyTimesException() { Account = account };
        }

        var isValid = await _authentication.IsValid(account, password, otp);
        if (isValid)
        {
            await _failCounter.Reset(account);
        }
        else
        {
            await _failCounter.AddFailCount(account);
        }

        return isValid;
    }

    public Task<int> GetFailedCount(string account)
    {
        return _failCounter.GetFailedCount(account);
    }
}