using Airport_Food_Court_App__Vendor_Side_.Data;
using Airport_Food_Court_App__Vendor_Side_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Airport_Food_Court_App__Vendor_Side_.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<Vendor> SignInManager;
        private readonly UserManager<Vendor> UserManager;

        public MenuController(ApplicationDbContext db, SignInManager<Vendor> SignInManager, UserManager<Vendor> UserManager)
        {
            _db = db;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }

        public IActionResult Index()
        {
            if (_db.Menus.FirstOrDefault(m => m.UserId == UserManager.GetUserAsync(User).Result.Id) == null)
            {
                _db.Menus.Add(new Menu { UserId = UserManager.GetUserAsync(User).Result.Id });
                _db.SaveChanges();
            }

            var menu = _db.Menus.First(menu => menu.UserId == UserManager.GetUserAsync(User).Result.Id);
            return View(menu);
        }
    }
}
