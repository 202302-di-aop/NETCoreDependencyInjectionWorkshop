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
        NextStateByAllState();
    }

    public string Score()
    {
        return _currentState.Score();
    }

    public void SecondPlayerGoal()
    {
        _secondPlayerScore++;
        ChangeState(new AllState(this));
    }

    private void ChangeState(AllState state)
    {
        _currentState = state;
    }

    private void NextStateByAllState()
    {
        ChangeState(new LookupState(this));
    }
}