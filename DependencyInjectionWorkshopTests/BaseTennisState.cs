namespace DependencyInjectionWorkshopTests;

public abstract class BaseTennisState
{
    protected readonly TennisBox _tennisBox;

    protected Dictionary<int, string> _scoreLookup = new Dictionary<int, string>()
    {
        { 0, "love" },
        { 1, "fifteen" },
        { 2, "thirty" },
        { 3, "forty" },
    };

    protected BaseTennisState(TennisBox tennisBox)
    {
        _tennisBox = tennisBox;
    }

    public abstract void NextState();
    public abstract string Score();

    protected void GoToAdvState()
    {
        _tennisBox.ChangeState(new AdvState(_tennisBox));
    }

    protected void GoToAllState()
    {
        _tennisBox.ChangeState(new AllState(_tennisBox));
    }

    protected void GoToDeuceState()
    {
        _tennisBox.ChangeState(new DeuceState(_tennisBox));
    }

    protected void GoToLookupState()
    {
        _tennisBox.ChangeState(new LookupState(_tennisBox));
    }
}