namespace DependencyInjectionWorkshopTests;

public class LookupState : BaseTennisState
{
    private Dictionary<int, string> _scoreLookup = new Dictionary<int, string>()
        { { 0, "love" }, { 1, "fifteen" }, { 2, "thirty" }, };

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