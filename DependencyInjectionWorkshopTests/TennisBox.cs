namespace DependencyInjectionWorkshopTests;

public class TennisBox
{
    private BaseTennisState _currentState;
    public int _firstPlayerScore;
    public int _secondPlayerScore;

    public TennisBox()
    {
        _currentState = new AllState(this);
    }

    public void ChangeState(BaseTennisState state)
    {
        _currentState = state;
    }

    public void FirstPlayerGoal()
    {
        _firstPlayerScore++;
        _currentState.NextState();
    }

    public string Score()
    {
        return _currentState.Score();
    }

    public void SecondPlayerGoal()
    {
        _secondPlayerScore++;
        _currentState.NextState();
    }
}