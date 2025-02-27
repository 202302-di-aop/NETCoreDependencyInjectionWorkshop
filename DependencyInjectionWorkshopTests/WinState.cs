namespace DependencyInjectionWorkshopTests;

public class WinState : BaseTennisState
{
    public WinState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        throw new NotImplementedException();
    }

    public override string Score()
    {
        return $"{_tennisBox.GetAdvPlayer()} win";
    }
}