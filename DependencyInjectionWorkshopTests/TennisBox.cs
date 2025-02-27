namespace DependencyInjectionWorkshopTests;

public class TennisBox
{
    private BaseTennisState _currentState;
    private string _firstPlayerName;
    public int _firstPlayerScore;
    public int _secondPlayerScore;

    public TennisBox(string firstPlayerName)
    {
        _currentState = new AllState(this);
        _firstPlayerName = firstPlayerName;
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

    public string GetFirstPlayerName()
    {
        return _firstPlayerName;
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