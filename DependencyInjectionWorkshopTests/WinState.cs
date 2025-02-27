namespace DependencyInjectionWorkshopTests;

public class WinState : BaseTennisState
{
    public WinState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        throw new NotImplementedException();
    }

    public override string Score()
    {
        return $"{TennisBoxContext.GetAdvPlayer()} win";
    }
}