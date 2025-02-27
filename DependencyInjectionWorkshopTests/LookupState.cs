namespace DependencyInjectionWorkshopTests;

public class LookupState : BaseTennisState
{
    public LookupState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        GoToAllState();
    }

    public override string Score()
    {
        return "fifteen love";
    }
}