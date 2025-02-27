namespace DependencyInjectionWorkshopTests;

public class AllState
{
    private readonly TennisBox _tennisBox;

    public AllState(TennisBox tennisBox)
    {
        _tennisBox = tennisBox;
    }

    public void NextState()
    {
        _tennisBox.ChangeState(new LookupState(_tennisBox));
    }

    public virtual string Score()
    {
        if (_tennisBox._firstPlayerScore == 1)
        {
            return "fifteen all";
        }

        return "love all";
    }
}