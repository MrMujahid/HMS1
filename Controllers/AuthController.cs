using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMS.Data;
using HMS.Models.ViewModels;
using System.Linq;
using HMS.Models.EntityModels;


namespace HMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly Db _db;
        public AuthController(Db db)
        {
            this._db = db;
        }

        //--1. ------Login Process-----
        public IActionResult Index()
        {
            return View();
        }

        //---2.
        [HttpPost]
        public IActionResult SignIn([Bind("Name,Password")] Auth auth)
        {

            //if("jahid"==auth.Name && "111111" == auth.Password)
            //{
            //    return RedirectToAction(nameof(Dashboard));
            //}


            var user = _db.Users.Where(u => u.Name == auth.Name && u.Password == auth.Password).FirstOrDefault<User>();

            // return Ok(user);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }


            //1, Collection of claims
            List<Claim> userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.RoleId.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("ContactNo",user.ContactNo),
                    new Claim("Photo",user.Photo),
                    new Claim("FullName",user.FullName)

               };

            //2. Make Claim Identity
            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            //3. Make Claim Principal
            var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

            HttpContext.SignInAsync(userPrinciple);


            return RedirectToAction("Index", "Home");


        }




        //--3.-------Signup Process------
        public IActionResult Signup()
        {

            ICollection<Role> roles = _db.Roles.ToList();
            return View(roles);

        }

        //--.4---
        [HttpPost]
        public IActionResult Signup([Bind("RoleId", "Name", "Password")] User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));//--go to 1
        }

        //----SignOut Process----

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
    }
}
