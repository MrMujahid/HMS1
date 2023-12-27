using Microsoft.AspNetCore.Mvc;
using HMS.Data;
using HMS.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using HMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        public readonly Db _db;
        public DoctorController (Db db) 
        {
            this._db = db;            
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 1, string search = "")
        {
            var Table = from d in _db.Doctors
                        join s in _db.Departments
                        on d.DepartmentId equals s.Id
                        select new
                        {
                            Id = d.Id,
                            Name = d.Name,
                            DepartmentId = s.Name,
                            ContactNo = d.ContactNo,
                            Designation= d.Designation,
                            Remark = d.Remark,
                        };
            Pagination p = new Pagination(Table.Count(), page, pageSize);
            ViewBag.Pagination = p;

            var Doctors = await Table.Skip(p.Skip).Take(pageSize).ToListAsync();

            return View(Doctors);
        }



        public IActionResult Create()
        {

            ICollection<Department> departments = _db.Departments.ToList();
            return View(departments);

        }

        [HttpPost]
        public IActionResult Create([Bind("Name", "DepartmentId", "ContactNo", "Designation", "Remark")] Doctor  doctor)
        {
            _db.Doctors.Add(doctor);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));//--go to 1
        }



        public IActionResult Edit(int id)
        {
            var d = _db.Doctors.Find(id);
            var s = _db.Departments;
            ViewBag.Departments = s;
            return View(d);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("Id,Name","DepartmentId","ContactNo","Designation","Remark")] Doctor doctor)
            
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }
            _db.Doctors.Update(doctor);
            _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //return View();
        }










    }
}
