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
        WhenFirstPlayerGoal();
        ScoreShouldBe("fifteen love");
    }

    [Test]
    public void lookup_state_to_all_state_from_1_0_to_1_1()
    {
        GivenFirstPlayerScore(1);
        WhenSecondPlayerGoal();
        ScoreShouldBe("fifteen all");
    }

    [Test]
    public void all_state_to_lookup_state_from_1_1_to_2_1()
    {
        GivenFirstPlayerScore(1);
        GivenSecondPlayerScore(1);
        WhenFirstPlayerGoal();
        ScoreShouldBe("thirty fifteen");
    }

    private void GivenSecondPlayerScore(int score)
    {
        for (int i = 0; i < score; i++)
        {
            _tennisBox.SecondPlayerGoal();
        }
    }

    private void WhenSecondPlayerGoal()
    {
        _tennisBox.SecondPlayerGoal();
    }

    private void GivenFirstPlayerScore(int score)
    {
        for (int i = 0; i < score; i++)
        {
            _tennisBox.FirstPlayerGoal();
        }
    }

    private void WhenFirstPlayerGoal()
    {
        _tennisBox.FirstPlayerGoal();
    }

    private void ScoreShouldBe(string expected)
    {
        Assert.That(_tennisBox.Score(), Is.EqualTo(expected));
    }
}