namespace DependencyInjectionWorkshopTests;

public abstract class BaseTennisState
{
    protected readonly TennisBox _tennisBox;

    protected BaseTennisState(TennisBox tennisBox)
    {
        _tennisBox = tennisBox;
    }

    public abstract void NextState();
    public abstract string Score();
}

public class AllState : BaseTennisState
{
    public AllState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        _tennisBox.ChangeState(new LookupState(_tennisBox));
    }

    public override string Score()
    {
        if (_tennisBox._firstPlayerScore == 1)
        {
            return "fifteen all";
        }

        return "love all";
    }
}