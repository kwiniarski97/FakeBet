namespace FakeBet.API.Email
{
    public interface IEmailClient
    {
        void Send(string recipent, string content);

        void NotifyAboutBan(string recipent);

        void NotifyAboutBlockade(string recipent);

        void SendActivationLink(string recipent, string nickname);
    }
}