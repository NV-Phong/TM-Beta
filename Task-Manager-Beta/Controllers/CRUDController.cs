using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_Manager_Beta.Data;
using Task_Manager_Beta.ViewModels;

namespace Task_Manager_Beta.Controllers
{
    public class CRUDController : Controller
    {
        private readonly TaskManagerContext _context;

        public CRUDController(TaskManagerContext context)
        {
            _context = context;
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject([Bind("Idproject,ProjectName,Idleader,DayCreate,Image,Hide")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();

                TempData["IDPROJECT"] = project.Idproject;

                var iduser = User.FindFirst("UserId")?.Value;
                var MEMBER = new Member
                {
                    Idproject = project.Idproject,
                    Iduser = int.Parse(iduser),
                };
                _context.Members.Add(MEMBER);
                await _context.SaveChangesAsync();

                return RedirectToAction("ChooseTemplate");
            }
            return View(project);
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> ChooseTemplate()
        {
            TempData.Keep("IDPROJECT");
            var TEMPLATE = await _context.Templates.ToListAsync();
            var GETTEMPLATE = new DashBoardViewModel
            {
                Templates = TEMPLATE,
            };
            return View(GETTEMPLATE);
        }

        public async Task<IActionResult> LoadTemplate(int? idtemplate)
        {
            int IDProject = (int)TempData["IDPROJECT"];

            var GETLISTTEMPLATE = await _context.Listtemplates
                                  .Where(w => w.Idtemplate == idtemplate)
                                  .Select(s => s.StatusName).ToListAsync();

            foreach (var GetListTemplate in GETLISTTEMPLATE)
            {
                var STATUS = new Status
                {
                    Idproject = IDProject,
                    StatusName = GetListTemplate
                };
                _context.Statuses.Add(STATUS);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Project", "DashBoard", new { idproject = IDProject });

        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> CreateTask(int? idproject, int? idstatus)
        {
            ViewData["Idproject"] = new SelectList(_context.Projects, "Idproject", "Idproject");
            ViewData["Idstatus"] = new SelectList(_context.Statuses, "Idstatus", "Idstatus");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([Bind("Idtask,Idproject,Idstatus,TaskName,DayCreate,DayStart,Deadline,Hide")] Data.Task task, int? idproject)
        {

            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Project", "DashBoard", new { idproject = idproject.Value });

        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> CreateStatus(int? idproject)
        {
            ViewData["Idproject"] = new SelectList(_context.Projects, "Idproject", "Idproject");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStatus([Bind("Idstatus,Idproject,StatusName")] Status status, int? idproject)
        {

            _context.Add(status);
            await _context.SaveChangesAsync();
            return RedirectToAction("Project", "DashBoard", new { idproject = idproject.Value });

        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> UpdateTaskStatus(int id, int idproject, [FromBody] string newStatusName)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    return NotFound();
                }

                // Tìm id của Status tương ứng với newStatusName
                var StatusId = await _context.Statuses
                    .Where(s => s.StatusName == newStatusName)
                    .Where(s => s.Idproject == idproject)
                    .Select(s => s.Idstatus)
                    .FirstOrDefaultAsync();


                // Cập nhật trạng thái mới cho task
                task.Idstatus = StatusId;


                // Lưu thay đổi vào cơ sở dữ liệu
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return Ok("Task status updated successfully!");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------//
        //TODO:
        public async Task<IActionResult> DeleteProject(int? idproject)
        {
            if (idproject == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Idproject == idproject);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjectConfirmed(int idproject)
        {
            //DELETE IN TABLE: STATUS + TASK + MEMBER + PERMISSION + ASSIGNMENT

            var MEMBER = await _context.Members.Where(w => w.Idproject == idproject).ToListAsync();
            var ASSIGNMENT = await _context.Assignments.FindAsync(idproject);
            var PERMISSION = await _context.Permisssions.FindAsync(idproject);
            var TASK = await _context.Tasks.Where(w => w.Idproject == idproject).ToListAsync();
            var STATUS = await _context.Statuses.Where(w => w.Idproject == idproject).ToListAsync();
            var PROJECT = await _context.Projects.FindAsync(idproject);

            if (MEMBER != null)
            {
                foreach (var member in MEMBER)
                {

                    _context.Members.Remove(member);

                }
            }

            if (ASSIGNMENT != null)
            {
                _context.Assignments.Remove(ASSIGNMENT);
            }

            if (PERMISSION != null)
            {
                _context.Permisssions.Remove(PERMISSION);
            }


            if (TASK != null)
            {
                foreach (var task in TASK) 
                {
                
                    _context.Tasks.Remove(task);
                
                }
            }


            if (STATUS != null)
            {
                foreach (var status in STATUS)
                {

                    _context.Statuses.Remove(status);

                }
            }


            if (PROJECT != null)
            {
                _context.Projects.Remove(PROJECT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("DashBoard", "DashBoard", new {iduser = User.FindFirst("UserId")?.Value});
        }

//----------------------------------------------------------------------------------------------------------------------------------//



//----------------------------------------------------------------------------------------------------------------------------------//
    }
}
