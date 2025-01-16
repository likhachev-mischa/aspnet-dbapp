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
    public class RoleToPrivilegesController : Controller
    {
        private readonly TablesContext _context;

        public RoleToPrivilegesController(TablesContext context)
        {
            _context = context;
        }

        // GET: RoleToPrivileges
        public async Task<IActionResult> Index()
        {
            var tablesContext = _context.RoleToPrivileges.Include(r => r.Privilege).Include(r => r.Role);
            return View(await tablesContext.ToListAsync());
        }

        // GET: RoleToPrivileges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleToPrivilege = await _context.RoleToPrivileges
                .Include(r => r.Privilege)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.RoleToPrivilegeId == id);
            if (roleToPrivilege == null)
            {
                return NotFound();
            }

            return View(roleToPrivilege);
        }

        // GET: RoleToPrivileges/Create
        public IActionResult Create()
        {
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: RoleToPrivileges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleToPrivilegeId,RoleId,PrivilegeId")] RoleToPrivilege roleToPrivilege)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleToPrivilege);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", roleToPrivilege.PrivilegeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", roleToPrivilege.RoleId);
            return View(roleToPrivilege);
        }

        // GET: RoleToPrivileges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleToPrivilege = await _context.RoleToPrivileges.FindAsync(id);
            if (roleToPrivilege == null)
            {
                return NotFound();
            }
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", roleToPrivilege.PrivilegeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", roleToPrivilege.RoleId);
            return View(roleToPrivilege);
        }

        // POST: RoleToPrivileges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleToPrivilegeId,RoleId,PrivilegeId")] RoleToPrivilege roleToPrivilege)
        {
            if (id != roleToPrivilege.RoleToPrivilegeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleToPrivilege);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleToPrivilegeExists(roleToPrivilege.RoleToPrivilegeId))
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
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", roleToPrivilege.PrivilegeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", roleToPrivilege.RoleId);
            return View(roleToPrivilege);
        }

        // GET: RoleToPrivileges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleToPrivilege = await _context.RoleToPrivileges
                .Include(r => r.Privilege)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.RoleToPrivilegeId == id);
            if (roleToPrivilege == null)
            {
                return NotFound();
            }

            return View(roleToPrivilege);
        }

        // POST: RoleToPrivileges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleToPrivilege = await _context.RoleToPrivileges.FindAsync(id);
            if (roleToPrivilege != null)
            {
                _context.RoleToPrivileges.Remove(roleToPrivilege);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleToPrivilegeExists(int id)
        {
            return _context.RoleToPrivileges.Any(e => e.RoleToPrivilegeId == id);
        }
    }
}
