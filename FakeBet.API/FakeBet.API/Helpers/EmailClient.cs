using System.Net;
using System.Net.Mail;

namespace FakeBet.API.Helpers
{
    public class EmailClient : IEmailClient
    {
        private SmtpClient _smtpClient;

        public EmailClient(string host, int port, string nickname, string password)
        {
            this._smtpClient = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(nickname, password)
            };
            
            
        }

        
    }
}