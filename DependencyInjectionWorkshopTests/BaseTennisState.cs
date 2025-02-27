namespace DependencyInjectionWorkshopTests;

public abstract class BaseTennisState
{
    protected readonly ITennisBoxContext TennisBoxContext;

    protected Dictionary<int, string> _scoreLookup = new Dictionary<int, string>()
    {
        { 0, "love" },
        { 1, "fifteen" },
        { 2, "thirty" },
        { 3, "forty" },
    };

    protected BaseTennisState(ITennisBoxContext tennisBoxContext)
    {
        TennisBoxContext = tennisBoxContext;
    }

    public abstract void NextState();
    public abstract string Score();

    protected void GoToAdvState()
    {
        TennisBoxContext.ChangeState(new AdvState(TennisBoxContext));
    }

    protected void GoToAllState()
    {
        TennisBoxContext.ChangeState(new AllState(TennisBoxContext));
    }

    protected void GoToDeuceState()
    {
        TennisBoxContext.ChangeState(new DeuceState(TennisBoxContext));
    }

    protected void GoToLookupState()
    {
        TennisBoxContext.ChangeState(new LookupState(TennisBoxContext));
    }

    protected void GoToWinState()
    {
        TennisBoxContext.ChangeState(new WinState(TennisBoxContext));
    }
}