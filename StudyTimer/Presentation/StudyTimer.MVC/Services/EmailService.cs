using Resend;

namespace StudyTimer.MVC.Services
{
    public class EmailService : IEmailService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IResend _resend;

        public EmailService(IWebHostEnvironment environment, IResend resend)
        {
            _environment = environment;
            _resend = resend;
        }

        public async Task EmailSendAsync(string to, string subject, string htmlBody)
        {
            await _resend.EmailSendAsync(new EmailMessage()
            {
                From = "onboarding@resend.dev",
                To = to,
                Subject = subject,
                HtmlBody = htmlBody
            });
        }

        public async Task PrepareAndSendVerifyEmail(string token, string email)
        {
            await EmailSendAsync(email, "Study Timer - E-Posta Doğrulama", await PrepareVerifyEmail(token, email));
        }

        public async Task<string> PrepareVerifyEmail(string token, string email)
        {
            return (await File.ReadAllTextAsync(
                Path.Combine(_environment.WebRootPath, "email-templates", "verify-email.html")))
                .Replace("{{Title}}", "Study Timer - E-Posta Doğrulama")
                .Replace("{{Description}}", "Study Timer uygulamamıza hoş geldiniz. E-posta adresinizi doğrulmak için lütfen aşağıdaki \"Onayla\" butonuna tıklayınız.")
                .Replace("{{ButtonLink}}", $"https://localhost:7154/Auth/VerifyEmail?email={email}&token={token}")
                .Replace("{{ButtonText}}", "Onayla");
        }
    }
}
