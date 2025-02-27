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
        var firstPlayerName = _tennisBox.GetFirstPlayerName();
        return $"{firstPlayerName} adv";
    }
}