namespace DependencyInjectionWorkshopTests;

public class DeuceState : BaseTennisState
{
    public DeuceState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        throw new NotImplementedException();
    }

    public override string Score()
    {
        return "deuce";
    }
}