namespace StudyTimer.MVC.Models.Home
{
    public class HomeGetStatisticsViewModel
    {
        public int TotalStudySessions { get; set; }
        public TimeSpan TotalStudyTime { get; set; }
        public string MostStudiedTopic { get; set; }
        public int TotalCompletedDuties { get; set; }
        public int TotalIncompleteDuties { get; set; }
        public DateTimeOffset LastStudySessionDate { get; set; }
    }
}
