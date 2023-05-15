using MimeKit;

namespace WealthApi.EmailSender
{
    public interface IEmailSender
    {
        void SendRegisterConfirmationEmail(string recipient, string token);
    }

    public class EmailSender : IEmailSender
    {
        public void SendRegisterConfirmationEmail(string recipient, string token)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Wealth Wise App", "wealthwiseapp@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", recipient));

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<a href='http://127.0.0.1:5173/activate-account/{token}'>Click here to confirm</a>"
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp-relay.sendinblue.com", 587, false);

                smtp.Authenticate("patrykz19@gmail.com", "X8NDYkASEtIV1FQ5");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
