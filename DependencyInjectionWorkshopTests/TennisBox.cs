namespace DependencyInjectionWorkshopTests;

public interface ITennisBoxContext
{
    int FirstPlayerScore { get; set; }
    int SecondPlayerScore { get; set; }
    string GetAdvPlayer();
    void ChangeState(BaseTennisState state);
}

public class TennisBox : ITennisBoxContext
{
    private BaseTennisState _currentState;
    private readonly string _firstPlayerName;
    private int _firstPlayerScore;
    private readonly string _secondPlayerName;
    private int _secondPlayerScore;

    public TennisBox(string firstPlayerName, string secondPlayerName)
    {
        _currentState = new AllState(this);
        _firstPlayerName = firstPlayerName;
        _secondPlayerName = secondPlayerName;
    }

    public int FirstPlayerScore
    {
        get => _firstPlayerScore;
        set => _firstPlayerScore = value;
    }

    public int SecondPlayerScore
    {
        get => _secondPlayerScore;
        set => _secondPlayerScore = value;
    }

    public string GetAdvPlayer()
    {
        return _firstPlayerScore > _secondPlayerScore
            ? _firstPlayerName
            : _secondPlayerName;
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