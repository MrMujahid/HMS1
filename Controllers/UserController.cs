using Microsoft.AspNetCore.Mvc;
using HMS.Data;
using HMS.Models.EntityModels;
using HMS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    public class UserController : Controller
    {
        private readonly Db _db;

        public UserController(Db db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 2, string search = "")
        {

            // var Table = db.Users.Where(p => p.Name.Contains(search));                     

            //Linq: Query Syntax

            //var Table2 = _db.Users.FromSql($"");//OLD Version

            //var Table2 = _db.Database.SqlQuery<int>($""); //SELECT
            //var Table2=_db.Database.ExecuteSql<int>($""); //UPDATE, DELETE, INSERT

            var Table = from u in _db.Users
                        join r in _db.Roles
                        on u.RoleId equals r.Id
                        select new
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Role = r.Name,
                            Inactive = u.Inactive,
                            ContactNo = u.ContactNo
                        };

            Pagination p = new Pagination(Table.Count(), page, pageSize);
            ViewBag.Pagination = p;

            var users = await Table.Skip(p.Skip).Take(pageSize).ToListAsync();

            return View(users);
        }

        public IActionResult Create()
        {

            ICollection<Role> roles = _db.Roles.ToList();
            return View(roles);

        }

        [HttpPost]
        public IActionResult Create([Bind("RoleId", "Name","FullName","Inactive","ContactNo", "Email", "Photo", "Password")] User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        

        public IActionResult Edit(int id)
        {
            var u = _db.Users.Find(id);
            var r = _db.Roles;
            ViewBag.Roles = r;
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("Id,Name,FullName,RoleId,Inactive,Password,Email,ContactNo,Photo")] User user)

       {
            if (id != user.Id)
            {
                return NotFound();
            }
            _db.Users.Update(user);
            _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //return View();
        }



        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);


            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
