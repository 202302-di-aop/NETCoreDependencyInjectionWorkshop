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
        if (_tennisBox._firstPlayerScore == 2)
        {
            return "thirty fifteen";
        }

        return "fifteen love";
    }
}