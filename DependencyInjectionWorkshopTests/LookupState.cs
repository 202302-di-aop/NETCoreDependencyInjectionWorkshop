namespace DependencyInjectionWorkshopTests;

public class LookupState : BaseTennisState
{
    public LookupState(ITennisBoxContext tennisBoxContext) : base(tennisBoxContext)
    {
    }

    public override void NextState()
    {
        if (TennisBoxContext.FirstPlayerScore == TennisBoxContext.SecondPlayerScore)
        {
            if (TennisBoxContext.FirstPlayerScore >= 3)
            {
                GoToDeuceState();
            }
            else
            {
                GoToAllState();
            }
        }
        else
        {
            if (TennisBoxContext.FirstPlayerScore > 3 || TennisBoxContext.SecondPlayerScore > 3)
            {
                GoToWinState();
            }
            else
            {
                GoToLookupState();
            }
        }
    }

    public override string Score()
    {
        return $"{_scoreLookup[TennisBoxContext.FirstPlayerScore]} {_scoreLookup[TennisBoxContext.SecondPlayerScore]}";
    }
}