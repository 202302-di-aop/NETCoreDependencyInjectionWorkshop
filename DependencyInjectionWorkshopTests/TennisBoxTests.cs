namespace DependencyInjectionWorkshopTests;

[TestFixture]
public class TennisBoxTests
{
    [Test]
    public void init_state_is_all_state()
    {
        var tennisBox = new TennisBox();
        Assert.That(tennisBox.Score(), Is.EqualTo("love all"));
    }
}