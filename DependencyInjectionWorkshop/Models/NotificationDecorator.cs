namespace DependencyInjectionWorkshop.Models;

public class NotificationDecorator : IAuthentication
{
    private readonly INotification _notification;
    private IAuthentication _authentication;

    public NotificationDecorator(IAuthentication authentication, INotification notification)
    {
        _authentication = authentication;
        _notification = notification;
    }

    public async Task<bool> IsValid(string account, string password, string otp)
    {
        var isValid = await _authentication.IsValid(account, password, otp);
        if (!isValid)
        {
            _notification.Notify(account);
        }

        return isValid;
    }
}