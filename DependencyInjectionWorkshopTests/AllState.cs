namespace DependencyInjectionWorkshopTests;

public class AllState : BaseTennisState
{
    public AllState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        GoToLookupState();
    }

    public override string Score()
    {
        return $"{_scoreLookup[TennisBoxContext.FirstPlayerScore]} all";
    }
}