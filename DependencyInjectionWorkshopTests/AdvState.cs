namespace DependencyInjectionWorkshopTests;

public class AdvState : BaseTennisState
{
    public AdvState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        throw new NotImplementedException();
    }

    public override string Score()
    {
        return $"{GetAdvPlayer()} adv";
    }

    private string GetAdvPlayer()
    {
        var advPlayer = _tennisBox._firstPlayerScore > _tennisBox._secondPlayerScore
            ? _tennisBox.GetFirstPlayerName()
            : _tennisBox.GetSecondPlayerName();
        return advPlayer;
    }
}