namespace DependencyInjectionWorkshopTests;

public class TennisBox
{
    private BaseTennisState _currentState;
    private string _firstPlayerName;
    public int _firstPlayerScore;
    private string _secondPlayerName;
    public int _secondPlayerScore;

    public TennisBox(string firstPlayerName, string secondPlayerName)
    {
        _currentState = new AllState(this);
        _firstPlayerName = firstPlayerName;
        _secondPlayerName = secondPlayerName;
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

    public string GetAdvPlayer()
    {
        return _firstPlayerScore > _secondPlayerScore
            ? GetFirstPlayerName()
            : GetSecondPlayerName();
    }

    public string GetFirstPlayerName()
    {
        return _firstPlayerName;
    }

    public string GetSecondPlayerName()
    {
        return _secondPlayerName;
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