// See https://aka.ms/new-console-template for more information

using Autofac;
using DependencyInjectionWorkshop.Models;

internal class Program
{
    private static IContainer _container;

    public static async Task Main(string[] args)
    {
        RegisterContainer();

        var authentication = _container.Resolve<IAuthentication>();

        var isValid = await authentication.IsValid("joey", "abc", "123456");
        // var isValid = await authentication.IsValid("joey", "abc", "wrong otp");
        Console.WriteLine($"result:{isValid}");
    }

    private static void RegisterContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<FakeProfileRepo>().As<IProfileRepo>();
        builder.RegisterType<FakeOtp>().As<IOtpProxy>();
        builder.RegisterType<FakeHash>().As<IHash>();
        // builder.RegisterType<FakeLogger>().As<ILogger>();
        // builder.RegisterType<FakeFailedCounter>().As<IFailedCounter>();
        // builder.RegisterType<FakeSlack>().As<INotification>();
        //
        builder.RegisterType<AuthenticationService>().As<IAuthentication>();
        //
        // builder.RegisterDecorator<FailCounterDecorator, IAuthentication>();
        // builder.RegisterDecorator<LogDecorator, IAuthentication>();
        // builder.RegisterDecorator<NotificationDecorator, IAuthentication>();

        _container = builder.Build();
    }
}

internal class FakeHash: IHash
{
    public string GetHashResult(string password)
    {
        return "3388";
    }
}

internal class FakeOtp: IOtpProxy
{
    public Task<string> GetCurrentOtp(string account)
    {
        return Task.FromResult("123456");
    }
}

internal class FakeProfileRepo:IProfileRepo
{
    public string GetPasswordFromDb(string account)
    {
        return "3388";
    }
}