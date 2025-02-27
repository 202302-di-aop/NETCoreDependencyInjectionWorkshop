namespace DependencyInjectionWorkshopTests;

public class AdvState : BaseTennisState
{
    public AdvState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        if (TennisBoxContext.FirstPlayerScore == TennisBoxContext.SecondPlayerScore)
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
        return $"{TennisBoxContext.GetAdvPlayer()} adv";
    }
}