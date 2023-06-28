using MimeKit;
using MimeKit.Utils;
using System.Text;

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
            email.Body = GetEmailBuilder(token).ToMessageBody();

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.elasticemail.com", 2525, false);

                smtp.Authenticate("wealthwiseapp@gmail.com", "9CCC568CE6E6D2B06C612165BB5860FAFCB1");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        private BodyBuilder GetEmailBuilder(string token)
        {

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = @"Welcome to the WealthWise App. Start tracking your finances by activating your account now!";
            var image = bodyBuilder.LinkedResources.Add(@"UserFiles\images\wealthwise.jpg");
            image.ContentId = MimeUtils.GenerateMessageId();
            bodyBuilder.HtmlBody = string.Format(@"<img src=""cid:{0}"">", image.ContentId);
            bodyBuilder.HtmlBody += $"<a href='http://127.0.0.1:5173/activate-account/{token}\'><button>Confirm Account</button></a>";

            return bodyBuilder;
        }
    }
}
