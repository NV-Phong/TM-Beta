using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.Controllers
{
    public class CalendarController : Controller
    {
        private readonly TaskManagerContext _context;

        public CalendarController(TaskManagerContext context)
        {
            _context = context;
        }
        public IActionResult Calendar()
        {
            return View();
        }
        public IActionResult LoadEvents()
        {
            var idUser = User.FindFirst("UserId")?.Value;
            List<Event> events = GetEventsFromDatabaseAsync(int.Parse(idUser));

            return Json(events);
        }

        // Phương thức giả lập để lấy dữ liệu sự kiện từ cơ sở dữ liệu
        private List<Event> GetEventsFromDatabaseAsync(int? idUser)
        {
            // Logic để truy vấn và lấy dữ liệu sự kiện từ cơ sở dữ liệu
            var listTask = _context.Assignments.Where(t => t.Iduser == idUser)
                                                .Include(p => p.IdtaskNavigation)
                                                .ToList();
            List<Event> listEvents = new List<Event>();

            // thêm dữ liệu vào danh sách
            foreach (var task in listTask)
            {
                var Events = new Event
                {
                    title = task.IdtaskNavigation.TaskName,
                    start = task.IdtaskNavigation.DayStart?.ToString("yyyy-MM-dd"),
                    end = task.IdtaskNavigation.Deadline?.ToString("yyyy-MM-dd")
                };
                listEvents.Add(Events);
            }
            // Trả về danh sách các sự kiện
            return listEvents;
        }
    }
}
