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
        if (_tennisBox._firstPlayerScore == 1)
        {
            return "fifteen all";
        }

        return "love all";
    }
}