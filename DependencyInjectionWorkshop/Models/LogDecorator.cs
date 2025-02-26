namespace DependencyInjectionWorkshop.Models;

public class LogDecorator : IAuthentication
{
    private readonly IFailCounter _failCounter;
    private readonly IMyLogger _logger;
    private readonly IAuthentication _authentication;

    public LogDecorator(IAuthentication authentication, IFailCounter failCounter, IMyLogger logger)
    {
        _authentication = authentication;
        _failCounter = failCounter;
        _logger = logger;
    }

    public async Task<bool> IsValid(string account, string password, string otp)
    {
        var isValid = await _authentication.IsValid(account, password, otp);
        if (!isValid)
        {
            var failedCount = await _failCounter.GetFailedCount(account);
            _logger.Info($"accountId:{account} failed times:{failedCount}");
        }

        return isValid;
    }
}