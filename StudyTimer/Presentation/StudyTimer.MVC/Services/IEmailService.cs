namespace StudyTimer.MVC.Services
{
    public interface IEmailService
    {
        public Task PrepareAndSendVerifyEmail(string token, string email);
        public Task<string> PrepareVerifyEmail(string token, string email);
        public Task EmailSendAsync(string to, string subject, string htmlBody);
    }
}
