// See https://aka.ms/new-console-template for more information

using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DependencyInjectionWorkshop.Models;

public class LogInterceptor : IInterceptor
{
    private readonly IMyLogger _logger;

    public LogInterceptor(IMyLogger logger)
    {
        _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
        var signatureContent = $"log by interceptor:{invocation.TargetType.FullName}.{invocation.Method.Name}():" +
                               $"{string.Join("-", (invocation.Arguments.Select(x => (x ?? "").ToString())))}";

        _logger.Info(signatureContent);

        invocation.Proceed();
    }
}

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime End { get; }

    public Period NextPeriod { get; set; }
    public DateTime Start { get; }

    public override string ToString()
    {
        return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(NextPeriod)}: {NextPeriod}";
    }
}

internal class Program
{
    private static IContainer _container;

    public static async Task Main(string[] args)
    {
        RegisterContainer();

        var authentication = _container.Resolve<IAuthentication>();

        var isValid = await authentication.IsValid("joey", "abc", "666666");
        // var isValid = await authentication.IsValid("joey", "abc", "wrong otp");
        Console.WriteLine($"joey result:{isValid}");
    }


    private static void RegisterContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<FakeProfileRepo>().As<IProfileRepo>();
        builder.RegisterType<FakeOtp>().As<IOtpProxy>();
        builder.RegisterType<FakeHash>().As<IHash>();
        // builder.RegisterType<FakeLogger>().As<ILogger>();
        builder.RegisterType<FakeLogger>().As<IMyLogger>();
        // builder.RegisterType<FakeFailedCounter>().As<IFailedCounter>();
        // builder.RegisterType<FakeSlack>().As<INotification>();
        //
        // builder.RegisterType<FeatureToggleDispatcher>().As<IAuthentication>();
        // builder.RegisterType<JoeyToggleManager>().As<IFeatureToggleManager>();
        // builder.RegisterType<OAuth>();
        // builder.RegisterType<OAuth>().As<IAuthentication>();
        // builder.RegisterType<AuthenticationService>();

        builder.RegisterType<LogInterceptor>();

        builder.RegisterType<AuthenticationService>().As<IAuthentication>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(LogInterceptor));
        //
        // builder.RegisterDecorator<FailCounterDecorator, IAuthentication>();
        // builder.RegisterDecorator<LogDecorator, IAuthentication>();
        // builder.RegisterDecorator<NotificationDecorator, IAuthentication>();

        _container = builder.Build();
    }
}

internal class FakeLogger : IMyLogger
{
    public void Info(string message)
    {
        Console.WriteLine($"by joey: {message}");
    }
}

internal class JoeyToggleManager : IFeatureToggleManager
{
    public bool IsEnableOAuth()
    {
        var key = Console.ReadLine();
        return key == "T";
    }
}

internal class FeatureToggleDispatcher : IAuthentication
{
    private readonly IFeatureToggleManager _featureToggleManager;
    private readonly OAuth _newFlow;
    private readonly AuthenticationService _oldFlow;

    public FeatureToggleDispatcher(OAuth newFlow, AuthenticationService oldFlow,
        IFeatureToggleManager featureToggleManager)
    {
        _newFlow = newFlow;
        _oldFlow = oldFlow;
        _featureToggleManager = featureToggleManager;
    }

    public Task<bool> IsValid(string account, string password, string otp)
    {
        if (_featureToggleManager.IsEnableOAuth())
        {
            return _newFlow.IsValid(account, password, otp);
        }

        return _oldFlow.IsValid(account, password, otp);
    }
}

internal interface IFeatureToggleManager
{
    bool IsEnableOAuth();
}

internal class OAuth : IAuthentication
{
    public Task<bool> IsValid(string account, string password, string otp)
    {
        Console.WriteLine("I am OAuth;");
        if (otp == "666666")
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}

internal class FakeSlack : INotification

{
    public void Notify(string account)
    {
        Console.WriteLine($" fake slack - account :{account}");
    }
}

internal class FakeHash : IHash
{
    public string GetHashResult(string password)
    {
        return "3388";
    }
}

internal class FakeOtp : IOtpProxy
{
    public Task<string> GetCurrentOtp(string account)
    {
        return Task.FromResult("123456");
    }
}

internal class FakeProfileRepo : IProfileRepo
{
    public string GetPasswordFromDb(string account)
    {
        return "3388";
    }
}