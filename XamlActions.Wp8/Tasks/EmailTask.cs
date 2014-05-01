using Microsoft.Phone.Tasks;

namespace XamlActions.Tasks {
    public class EmailTask : IEmailTask {
        public void Send(Email email) {
            new EmailComposeTask {
                Subject = email.Subject,
                Body = email.Body,
                To = email.To,
                Cc = email.Cc,
                Bcc = email.Bcc
            }.Show();
        }
    }
}