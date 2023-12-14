namespace StudyTimer.MVC.Services
{
    public interface IToastService
    {
        public void SuccessMessage(string message);
        public void FailureMessage(string message);
    }
}
