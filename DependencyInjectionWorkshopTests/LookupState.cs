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
        return $"{_scoreLookup[_tennisBox._firstPlayerScore]} {_scoreLookup[_tennisBox._secondPlayerScore]}";
    }
}