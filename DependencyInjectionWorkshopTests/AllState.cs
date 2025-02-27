namespace DependencyInjectionWorkshopTests;

public class AllState : BaseTennisState
{
    public AllState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        GoToLookupState();
    }

    public override string Score()
    {
        return $"{_scoreLookup[_tennisBox._firstPlayerScore]} all";
    }
}