namespace DependencyInjectionWorkshopTests;

public class WinState : BaseTennisState
{
    public WinState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        throw new TennisStateException(){TennisContext = TennisBoxContext};
    }

    public override string Score()
    {
        return $"{TennisBoxContext.GetAdvPlayer()} win";
    }
}

public class TennisStateException : Exception
{
    public ITennisBoxContext TennisContext { get; set; }
}