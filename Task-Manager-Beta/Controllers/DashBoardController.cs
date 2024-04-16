using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;
using Task_Manager_Beta.ViewModels;

namespace Task_Manager_Beta.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly TaskManagerContext _context;
        private readonly EmailService _emailService;
        public DashBoardController(TaskManagerContext context, EmailService emailService)
        {
            _emailService = emailService;
            _context = context;
        }

//----------------------------------------------------------------------------------------------------------------------------------//
        
        public async Task<IActionResult> DashBoard(int? iduser)
        {
            var ListProject = await _context.Members.Where(w => w.Iduser == iduser)
                                                    .Select(s => s.IdprojectNavigation)
                                                    .ToListAsync();

            var DASHBOARD = new DashBoardViewModel
            {
                Projects = ListProject,
            };

            return View(DASHBOARD);

        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> Project(int? idproject)
        {
            var GetStatus = await _context.Statuses.Where(w => w.Idproject == idproject).ToListAsync();
            var GetTask   = await _context.Tasks.Where(w => w.Idproject == idproject).ToListAsync();
            var GetAssignment = await _context.Assignments
                                .Include(i => i.IduserNavigation)
                                .ToListAsync();

            TempData["IDPROJECT"] = idproject;
            TempData["IDSTATUS"]  = GetStatus.FirstOrDefault()?.Idstatus;

            var GetProject = new DashBoardViewModel
            {
                Status = GetStatus,
                Tasks  = GetTask,
                Assignments = GetAssignment,
            };

            return View(GetProject);
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> Assigned(int? iduser, int? idtask, int? idproject)
        {
            if (!iduser.HasValue || !idtask.HasValue || !idproject.HasValue)
            {
                return BadRequest("Invalid parameters");
            }

            var ASSIGNMENT = new Assignment
            {
                Idtask = idtask.Value,
                Iduser = iduser.Value,
            };
            _context.Assignments.Add(ASSIGNMENT);
            await _context.SaveChangesAsync();

            return RedirectToAction("Project", "DashBoard", new { idproject = idproject.Value });
        }

        public async Task<IActionResult> Unassigned(int? idtask, int? iduser, int? idproject)
        {
            if (!idtask.HasValue || !iduser.HasValue)
            {
                return BadRequest("Invalid parameters");
            }

            var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Idtask == idtask && a.Iduser == iduser);
            if (assignment == null)
            {
                return RedirectToAction("Project", "DashBoard", new { idproject = idproject.Value });
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Project", "DashBoard", new { idproject = idproject.Value });
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        public async Task<IActionResult> ProjectSetting(int? idproject)
        {
            var GetProject = await _context.Projects.Where(w => w.Idproject == idproject).ToListAsync();

            var PROJECTSETTING = new DashBoardViewModel
            {
                Projects = GetProject,
            };
            return View(PROJECTSETTING);
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        [HttpGet]
        public IActionResult InviteUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InviteUser(InviteUserViewModel model, int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                string returnUrl = $"[https://localhost:1234/User/Invite?returnUrl=https://localhost:1234/dashboard/{id}]";

                HttpContext.Session.SetString("ReturnUrl", returnUrl);

                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                var project = await _context.Projects.FindAsync(id);
                string projectName = project.ProjectName;
                string subject = $"Thư mời tham gia dự án: {project.ProjectName} ";
                string invitationLink = $"https://localhost:1234/User/Invite?returnUrl=https://localhost:1234/dashboard/{id}";
                string body = $"Bạn được mời tham gia dự án {project.ProjectName}.<br><br>";
                body += $" <a href=\"{invitationLink}\" style=\"display: inline-block; padding: 10px 20px; background-color: #007BFF; color: white; text-decoration: none; border-radius: 5px;\">Join {project.ProjectName} Project</a> <br><br>";
                body += " Vui lòng nhấp vào nút trên để tham gia dự án.";

                await _emailService.SendInvitationEmailAsync(model.UserEmail, subject, body);

                ViewBag.Message = "Email đã được gửi thành công!";
            }

            return View();
        }

//----------------------------------------------------------------------------------------------------------------------------------//
    }
}
