namespace DependencyInjectionWorkshopTests;

public class TennisBox
{
    private AllState _currentState;

    public TennisBox()
    {
        _currentState = new AllState();
    }

    public void FirstPlayerGoal()
    {
        _currentState = new LookupState();
    }

    public string Score()
    {
        return _currentState.Score();
    }
}