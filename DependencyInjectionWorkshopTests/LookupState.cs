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
        var scoreLookup = new Dictionary<int, string>() { { 0, "love" }, { 1, "fifteen" }, { 2, "thirty" }, };

        return $"{scoreLookup[_tennisBox._firstPlayerScore]} {scoreLookup[_tennisBox._secondPlayerScore]}";
    }
}