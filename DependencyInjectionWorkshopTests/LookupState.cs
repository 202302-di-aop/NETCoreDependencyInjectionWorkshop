namespace DependencyInjectionWorkshopTests;

public class LookupState : AllState
{
    public LookupState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public void NextState()
    {
        _tennisBox.ChangeState(new AllState(_tennisBox));
    }

    public override string Score()
    {
        return "fifteen love";
    }
}