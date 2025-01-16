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
    public class UserToRolesController : Controller
    {
        private readonly TablesContext _context;

        public UserToRolesController(TablesContext context)
        {
            _context = context;
        }

        // GET: UserToRoles
        public async Task<IActionResult> Index()
        {
            var tablesContext = _context.UserToRoles.Include(u => u.Role).Include(u => u.User);
            return View(await tablesContext.ToListAsync());
        }

        // GET: UserToRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToRole = await _context.UserToRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserToRoleId == id);
            if (userToRole == null)
            {
                return NotFound();
            }

            return View(userToRole);
        }

        // GET: UserToRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserToRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserToRoleId,UserId,RoleId")] UserToRole userToRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userToRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToRole.UserId);
            return View(userToRole);
        }

        // GET: UserToRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToRole = await _context.UserToRoles.FindAsync(id);
            if (userToRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userToRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToRole.UserId);
            return View(userToRole);
        }

        // POST: UserToRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserToRoleId,UserId,RoleId")] UserToRole userToRole)
        {
            if (id != userToRole.UserToRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserToRoleExists(userToRole.UserToRoleId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userToRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userToRole.UserId);
            return View(userToRole);
        }

        // GET: UserToRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToRole = await _context.UserToRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserToRoleId == id);
            if (userToRole == null)
            {
                return NotFound();
            }

            return View(userToRole);
        }

        // POST: UserToRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userToRole = await _context.UserToRoles.FindAsync(id);
            if (userToRole != null)
            {
                _context.UserToRoles.Remove(userToRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserToRoleExists(int id)
        {
            return _context.UserToRoles.Any(e => e.UserToRoleId == id);
        }
    }
}
