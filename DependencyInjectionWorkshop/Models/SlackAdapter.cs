using SlackAPI;

namespace DependencyInjectionWorkshop.Models;

public class SlackAdapter
{
    public void Notify(string account)
    {
        var message = $"{account} try to login fail.";
        var slackClient = new SlackClient("my api token");
        slackClient.PostMessage(response1 => { }, "my channel", message, "my bot name");
    }
}