namespace StudyTimer.MVC.Models.Auth
{
    public class AuthResponseModel
    {
        public bool Succeeded { get; set; }
        public IEnumerable<AuthErrorModel> Errors { get; set; }
        public string UserToken { get; set; }
        public string Username { get; set; }
    }
}
