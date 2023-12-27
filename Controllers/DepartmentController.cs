using HMS.Data;
using HMS.Models.EntityModels;
using HMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Db db;

        public DepartmentController(Db db)
        {
            this.db = db;
        }



        public async Task<IActionResult> Index(int page = 1, int pageSize = 5, string search = "")
        {
            var Table = db.Departments.Where(p => p.Name.Contains(search));

            Pagination p = new Pagination(Table.Count(), page, pageSize);
            ViewBag.Pagination = p;

            var departments = await Table.Skip(p.Skip).Take(pageSize).ToListAsync<Department>();

            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "Name")] Department department)
        {
            try
            {
                db.Add(department);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }


            // return View(role);
        }


        public async Task<IActionResult> Delete(int? id)
        {

            var department = await db.Departments.FirstOrDefaultAsync(m => m.Id == id);
            return View(department);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var department = await db.Departments.FindAsync(id);

            if (department != null)
            {
                db.Departments.Remove(department);

            }

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Departments == null)
            {
                return NotFound();
            }

            var department = await db.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            db.Update(department);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



    }
}
