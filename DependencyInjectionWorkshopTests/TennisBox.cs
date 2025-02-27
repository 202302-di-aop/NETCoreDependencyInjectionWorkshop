namespace DependencyInjectionWorkshopTests;

public class TennisBox
{
    private AllState _currentState;
    public int _firstPlayerScore;
    private int _secondPlayerScore;

    public TennisBox()
    {
        _currentState = new AllState(this);
    }

    public void FirstPlayerGoal()
    {
        _firstPlayerScore++;
        _currentState = new LookupState(this);
    }

    public string Score()
    {
        return _currentState.Score();
    }

    public void SecondPlayerGoal()
    {
        _secondPlayerScore++;
        _currentState = new AllState(this);
    }
}