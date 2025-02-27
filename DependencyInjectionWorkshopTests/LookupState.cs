namespace DependencyInjectionWorkshopTests;

public class LookupState : BaseTennisState
{
    public LookupState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        if (_tennisBox._firstPlayerScore == _tennisBox._secondPlayerScore)
        {
            GoToAllState();
        }
        else
        {
            GoToLookupState();
        }
    }

    public override string Score()
    {
        return $"{_scoreLookup[_tennisBox._firstPlayerScore]} {_scoreLookup[_tennisBox._secondPlayerScore]}";
    }
}