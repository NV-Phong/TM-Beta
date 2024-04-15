using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskManagerContext _context;

        public TasksController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.IdprojectNavigation)
                .Include(t => t.IdstatusNavigation)
                .FirstOrDefaultAsync(m => m.Idtask == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        public async Task<IActionResult> TimeLine()
        {
            return View();
        }

        // GET: Tasks/Create

        public async Task<IActionResult> Create(int? idproject, int? idstatus)
        {
            ViewData["Idproject"] = new SelectList(_context.Projects, "Idproject", "Idproject");
            ViewData["Idstatus"] = new SelectList(_context.Statuses, "Idstatus", "Idstatus");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idtask,Idproject,Idstatus,TaskName,DayCreate,DayStart,Deadline,Hide")] Data.Task task, int? idproject)
        {
            
            _context.Add(task);
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction("GetRole", "Projects", new { id = idproject.Value });
        }
        [Authorize(Roles = "Leader")]
        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? idproject, int? idtask)
        {
            if (idtask == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(idtask);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["Idproject"] = new SelectList(_context.Projects, "Idproject", "Idproject", task.Idproject);
            ViewData["Idstatus"] = new SelectList(_context.Statuses, "Idstatus", "Idstatus", task.Idstatus);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Leader")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Idtask,Idproject,Idstatus,TaskName,DayCreate,DayStart,Deadline,Hide")] Data.Task task, int? idproject)
        {

                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Idtask))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
                return RedirectToAction("DashBoard", "Projects", new { id = idproject.Value });
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? idtask)
        {
            if (idtask == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.IdprojectNavigation)
                .Include(t => t.IdstatusNavigation)
                .FirstOrDefaultAsync(m => m.Idtask == idtask);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idtask)
        {
            var task = await _context.Tasks.FindAsync(idtask);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("DashBoard", "Projects", new { id = task.Idproject });
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Idtask == id);
        }
    }
}
