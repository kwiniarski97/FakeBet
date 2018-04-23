using FakeBet.API.Helpers;

namespace FakeBet.API.Email
{
    public class EmailClient : IEmailClient
    {
        private readonly string _from = "admin@fakebet.ga";

        public EmailClient(string apiKey)
        {
            Api.ApiKey = apiKey;
        }

        public void Send(string recipent, string content)
        {
            Api.Email.Send(@from: _from, to: new[] {recipent}, bodyText: content,
                subject: "You got new message from admin at fakebet.ga");
        }

        public void NotifyAboutBan(string recipent)
        {
            Api.Email.Send(@from: _from, to: new[] {recipent}, subject: "Your account has been banned | fakebet.ga",
                bodyText:
                @"We noticed suspicious behavior on your profile so we banned it.
If you have any questions feel free to contant us. Just reply to this mail");
        }

        public void NotifyAboutBlockade(string recipent)
        {
            Api.Email.Send(@from: _from, to: new[] {recipent}, subject: "Your account has been blocked | fakebet.ga",
                bodyText:
                @"Someone tried to access your profile.
We blocked the attack if you want you can contact the administrator to unlock your account. Contact us by replying to this email");
        }

        public void SendActivationLink(string recipent, string nickname)
        { 
            var encodedNickname = Encryptor.ToBase64(nickname);
            Api.Email.Send(@from: _from, to: new[] {recipent}, subject: "Activation link | fakebet.ga",
                bodyText:
                $"Welcome to fakebet.ga. Click the activation link or paste it in your browser to activate your account. Link: www.fakebet.ga/activate/{encodedNickname}");
        }
    }
}