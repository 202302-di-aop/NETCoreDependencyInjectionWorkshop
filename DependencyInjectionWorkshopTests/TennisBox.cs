namespace DependencyInjectionWorkshopTests;

public class AllState
{
    public string Score()
    {
        return "love all";
    }
}

public class TennisBox
{
    private AllState _currentState;

    public TennisBox()
    {
        _currentState = new AllState();
    }

    public string Score()
    {
        return _currentState.Score();
    }
}