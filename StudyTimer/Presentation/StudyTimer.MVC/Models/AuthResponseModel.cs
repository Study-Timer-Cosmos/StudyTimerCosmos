namespace StudyTimer.MVC.Models
{
    public class AuthResponseModel
    {
        public bool Succeeded { get; set; }
        public IEnumerable<AuthErrorModel> Errors { get; set; }
        public string userToken { get; set; }
    }
}
