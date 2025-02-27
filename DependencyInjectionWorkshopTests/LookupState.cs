namespace DependencyInjectionWorkshopTests;

public class LookupState : AllState
{
    public LookupState(TennisBox tennisBox) : base(tennisBox)
    {
    }

    public override string Score()
    {
        return "fifteen love";
    }
}