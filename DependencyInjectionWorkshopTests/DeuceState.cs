namespace DependencyInjectionWorkshopTests;

public class DeuceState : BaseTennisState
{
    public DeuceState(TennisBox tennisBox) : base(tennisBox)
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