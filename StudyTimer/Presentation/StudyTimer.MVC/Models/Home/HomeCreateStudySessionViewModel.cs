using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudyTimer.MVC.Models.Home
{
    public class HomeCreateStudySessionViewModel
    {
        public Guid CategoryId { get; set; }
        public string Topic { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string? SelectedCategoryId { get; set; }
        public List<SelectListItem> TimeOptions { get; set; }
        public int SelectedTime { get; set; }
    }
}
