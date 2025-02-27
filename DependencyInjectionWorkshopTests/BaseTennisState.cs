namespace DependencyInjectionWorkshopTests;

public abstract class BaseTennisState
{
    protected readonly TennisBox _tennisBox;

    protected Dictionary<int, string> _scoreLookup = new Dictionary<int, string>()
        { { 0, "love" }, { 1, "fifteen" }, { 2, "thirty" }, };

    protected BaseTennisState(TennisBox tennisBox)
    {
        _tennisBox = tennisBox;
    }

    public abstract void NextState();
    public abstract string Score();

    protected void GoToLookupState()
    {
        _tennisBox.ChangeState(new LookupState(_tennisBox));
    }

    protected void GoToAllState()
    {
        _tennisBox.ChangeState(new AllState(_tennisBox));
    }
}