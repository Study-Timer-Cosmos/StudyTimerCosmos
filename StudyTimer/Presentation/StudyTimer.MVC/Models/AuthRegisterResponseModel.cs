namespace StudyTimer.MVC.Models
{
    public class AuthRegisterResponseModel
    {
        public bool Succeeded { get; set; }
        public IEnumerable<AuthErrorModel> Errors { get; set; }
        public string userToken { get; set; }
    }
}
