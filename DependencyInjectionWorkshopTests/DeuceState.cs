namespace DependencyInjectionWorkshopTests;

public class DeuceState : BaseTennisState
{
    public DeuceState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        GoToAdvState();
    }

    public override string Score()
    {
        return "deuce";
    }
}