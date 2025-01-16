using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dbapp.Models;

namespace dbapp.Controllers
{
    public class PrivilegesController : Controller
    {
        private readonly TablesContext _context;

        public PrivilegesController(TablesContext context)
        {
            _context = context;
        }

        // GET: Privileges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Privileges.ToListAsync());
        }

        // GET: Privileges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges
                .FirstOrDefaultAsync(m => m.PrivilegeId == id);
            if (privilege == null)
            {
                return NotFound();
            }

            return View(privilege);
        }

        // GET: Privileges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Privileges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrivilegeId,PrivilegeName")] Privilege privilege)
        {
            if (ModelState.IsValid)
            {
                _context.Add(privilege);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(privilege);
        }

        // GET: Privileges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges.FindAsync(id);
            if (privilege == null)
            {
                return NotFound();
            }
            return View(privilege);
        }

        // POST: Privileges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrivilegeId,PrivilegeName")] Privilege privilege)
        {
            if (id != privilege.PrivilegeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privilege);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivilegeExists(privilege.PrivilegeId))
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
            return View(privilege);
        }

        // GET: Privileges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges
                .FirstOrDefaultAsync(m => m.PrivilegeId == id);
            if (privilege == null)
            {
                return NotFound();
            }

            return View(privilege);
        }

        // POST: Privileges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var privilege = await _context.Privileges.FindAsync(id);
            if (privilege != null)
            {
                _context.Privileges.Remove(privilege);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrivilegeExists(int id)
        {
            return _context.Privileges.Any(e => e.PrivilegeId == id);
        }
    }
}
