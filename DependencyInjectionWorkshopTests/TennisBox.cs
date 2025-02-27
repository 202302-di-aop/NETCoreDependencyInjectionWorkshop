namespace DependencyInjectionWorkshopTests;

public class AllState
{
    public virtual string Score()
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

    public void FirstPlayerGoal()
    {
        _currentState = new LookupState();
    }

    public string Score()
    {
        return _currentState.Score();
    }
}

public class LookupState : AllState
{
    public override string Score()
    {
        return "fifteen love";
    }
}