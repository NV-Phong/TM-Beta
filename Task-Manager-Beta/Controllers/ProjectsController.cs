using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;
using Task_Manager_Beta.ViewModels;

namespace Task_Manager_Beta.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly TaskManagerContext _context;

        public ProjectsController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }


        public async Task<IActionResult> DashBoard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Templates)
                .ThenInclude(t => t.IdstatusNavigation)
                .FirstOrDefaultAsync(p => p.Idproject == id);

            if (project == null)
            {
                return NotFound();
            }

            var GetTemplate = project.Templates.Where(m => m.Idproject == id).ToList();

            var GetTask = await _context.Tasks.Where(m => m.Idproject == id).ToListAsync();


            var GetDashBoard = new ProjectViewModel
            {
               Templates = GetTemplate,
               Tasks = GetTask,  
            };

            return View(GetDashBoard);
        }

        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] string newStatusName)
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




        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Idproject == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproject,ProjectName,Idleader,DayCreate,Image,Hide")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();

                //-----------------------------------------------------------------------------------//

                // Lấy danh sách IDStatus từ bảng Status
                var DefaultStatus = await _context.Statuses.Select(t => t.Idstatus).ToListAsync();

                // Thêm các IDStatus vào bảng Template ứng với IDproject vừa tạo
                foreach (var IDStatus in DefaultStatus)
                {
                    var DefaultTemplate = new Template
                    {
                        Idstatus = IDStatus,
                        Idproject = project.Idproject
                    };
                    _context.Templates.Add(DefaultTemplate);
                }
                await _context.SaveChangesAsync();

                //-----------------------------------------------------------------------------------//


                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }


        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproject,ProjectName,Idleader,DayCreate,Image,Hide")] Project project)
        {
            if (id != project.Idproject)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Idproject))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Idproject == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Idproject == id);
        }
    }
}
