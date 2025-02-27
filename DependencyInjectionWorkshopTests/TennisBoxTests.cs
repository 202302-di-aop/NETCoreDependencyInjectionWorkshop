namespace DependencyInjectionWorkshopTests;

[TestFixture]
public class TennisBoxTests
{
    private TennisBox _tennisBox;

    [SetUp]
    public void SetUp()
    {
        _tennisBox = new TennisBox();
    }

    [Test]
    public void init_state_is_all_state()
    {
        ScoreShouldBe("love all");
    }

    [Test]
    public void all_state_to_lookup_state_from_0_0_to_1_0()
    {
        _tennisBox.FirstPlayerGoal();
        ScoreShouldBe("fifteen love");
    }

    private void ScoreShouldBe(string expected)
    {
        Assert.That(_tennisBox.Score(), Is.EqualTo(expected));
    }
}