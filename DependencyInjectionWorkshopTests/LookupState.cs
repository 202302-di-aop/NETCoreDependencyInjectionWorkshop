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
        var scoreLookup = new Dictionary<int, string>()
        {
            { 1, "fifteen" },
        };
        if (_tennisBox._firstPlayerScore == 2)
        {
            return $"thirty {scoreLookup[_tennisBox._secondPlayerScore]}";
        }

        return "fifteen love";
    }
}