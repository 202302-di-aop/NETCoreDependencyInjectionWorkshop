namespace DependencyInjectionWorkshopTests;

public class AdvState : BaseTennisState
{
    public AdvState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        GoToDeuceState();
    }

    public override string Score()
    {
        return $"{_tennisBox.GetAdvPlayer()} adv";
    }
}