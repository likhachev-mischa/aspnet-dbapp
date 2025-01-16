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
    public class UserToPrivilegesController : Controller
    {
        private readonly TablesContext _context;

        public UserToPrivilegesController(TablesContext context)
        {
            _context = context;
        }

        // GET: UserToPrivileges
        public async Task<IActionResult> Index()
        {
            var tablesContext = _context.UserToPrivileges.Include(u => u.Privilege).Include(u => u.User);
            return View(await tablesContext.ToListAsync());
        }

        // GET: UserToPrivileges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToPrivilege = await _context.UserToPrivileges
                .Include(u => u.Privilege)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserToPrivilegeId == id);
            if (userToPrivilege == null)
            {
                return NotFound();
            }

            return View(userToPrivilege);
        }

        // GET: UserToPrivileges/Create
        public IActionResult Create()
        {
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserToPrivileges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserToPrivilegeId,UserId,PrivilegeId")] UserToPrivilege userToPrivilege)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToPrivilege);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", userToPrivilege.PrivilegeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToPrivilege.UserId);
            return View(userToPrivilege);
        }

        // GET: UserToPrivileges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToPrivilege = await _context.UserToPrivileges.FindAsync(id);
            if (userToPrivilege == null)
            {
                return NotFound();
            }
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", userToPrivilege.PrivilegeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToPrivilege.UserId);
            return View(userToPrivilege);
        }

        // POST: UserToPrivileges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserToPrivilegeId,UserId,PrivilegeId")] UserToPrivilege userToPrivilege)
        {
            if (id != userToPrivilege.UserToPrivilegeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToPrivilege);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserToPrivilegeExists(userToPrivilege.UserToPrivilegeId))
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
            ViewData["PrivilegeId"] = new SelectList(_context.Privileges, "PrivilegeId", "PrivilegeId", userToPrivilege.PrivilegeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToPrivilege.UserId);
            return View(userToPrivilege);
        }

        // GET: UserToPrivileges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToPrivilege = await _context.UserToPrivileges
                .Include(u => u.Privilege)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserToPrivilegeId == id);
            if (userToPrivilege == null)
            {
                return NotFound();
            }

            return View(userToPrivilege);
        }

        // POST: UserToPrivileges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userToPrivilege = await _context.UserToPrivileges.FindAsync(id);
            if (userToPrivilege != null)
            {
                _context.UserToPrivileges.Remove(userToPrivilege);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserToPrivilegeExists(int id)
        {
            return _context.UserToPrivileges.Any(e => e.UserToPrivilegeId == id);
        }
    }
}
