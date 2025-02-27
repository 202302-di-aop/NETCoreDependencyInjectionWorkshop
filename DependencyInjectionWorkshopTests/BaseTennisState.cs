namespace DependencyInjectionWorkshopTests;

public abstract class BaseTennisState
{
    protected readonly TennisBox _tennisBox;

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
}