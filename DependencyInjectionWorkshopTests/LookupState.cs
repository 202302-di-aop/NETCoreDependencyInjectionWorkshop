namespace DependencyInjectionWorkshopTests;

public class LookupState : BaseTennisState
{
    public LookupState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override void NextState()
    {
        _tennisBox.ChangeState(new AllState(_tennisBox));
    }

    public override string Score()
    {
        return "fifteen love";
    }
}