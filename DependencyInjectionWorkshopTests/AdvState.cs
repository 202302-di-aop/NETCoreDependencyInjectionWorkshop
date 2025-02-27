namespace DependencyInjectionWorkshopTests;

public class AdvState : BaseTennisState
{
    public AdvState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        if (_tennisBox._firstPlayerScore == _tennisBox._secondPlayerScore)
        {
            GoToDeuceState();
        }
        else
        {
            GoToWinState();
        }
        
    }

    public override string Score()
    {
        return $"{_tennisBox.GetAdvPlayer()} adv";
    }
}