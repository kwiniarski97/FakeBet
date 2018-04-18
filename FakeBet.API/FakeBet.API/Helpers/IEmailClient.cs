namespace FakeBet.API.Helpers
{
    public interface IEmailClient
    {
        void Send(string recipent, string content);

        void NotifyAboutBan(string recipent);

        void NotifyAboutBlockade(string recipent);
    }
}