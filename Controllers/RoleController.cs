using HMS.Data;
using HMS.Models.EntityModels;
using HMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HMS.Controllers
{
    public class RoleController : Controller
    {
        private readonly Db db;

        public RoleController(Db db)
        {
            this.db = db;
        }
      


        public async Task<IActionResult> Index(int page = 1, int pageSize = 2, string search = "")
        {
            var Table = db.Roles.Where(p => p.Name.Contains(search));

            Pagination p = new Pagination(Table.Count(), page, pageSize);
            ViewBag.Pagination = p;

            var roles = await Table.Skip(p.Skip).Take(pageSize).ToListAsync<Role>();

            return View(roles);
        }





        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "Name")] Role role)
        {
            try
            {
                db.Add(role);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }


            // return View(role);
        }







        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await db.Roles.Include(s => s.Users).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (role == null)
            {
                return NotFound();
            }


            return View(role);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var role = await db.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            db.Update(role);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {

            var role = await db.Roles.FirstOrDefaultAsync(m => m.Id == id);
            return View(role);

        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var role = await db.Roles.FindAsync(id);

            if (role != null)
            {
                db.Roles.Remove(role);

            }

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}
