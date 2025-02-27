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

    public void ChangeState(AllState state)
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
        //todo: should use _currentState
        _currentState.NextState();
    }
}