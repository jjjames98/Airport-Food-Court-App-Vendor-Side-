using Airport_Food_Court_App__Vendor_Side_.Data;
using Airport_Food_Court_App__Vendor_Side_.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

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

            var menu = _db.Menus.FirstOrDefault(m => m.UserId == UserManager.GetUserAsync(User).Result.Id);
            var categories = _db.MenuCategories.Where(c => c.MenuId == menu.Id).ToList();
            ViewBag.Categories = categories;
            var items = _db.MenuItems.Where(i => categories.Contains(i.MenuCategory)).ToList();
            ViewBag.Items = items;
            return View(menu);
        }

        public IActionResult CreateCategory()
        {
            var menu = _db.Menus.FirstOrDefault(m => m.UserId == UserManager.GetUserAsync(User).Result.Id);
            ViewBag.MenuId = menu.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(MenuCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.Menus.First(menu => menu.UserId == UserManager.GetUserAsync(User).Result.Id).MenuCategories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult CreateItem()
        {
            var menu = _db.Menus.First(menu => menu.UserId == UserManager.GetUserAsync(User).Result.Id);
            var categories = _db.MenuCategories.Where(c => c.MenuId == menu.Id).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", 1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(MenuItem obj)
        {
            if (ModelState.IsValid)
            {
                _db.MenuCategories.First(c => c.Id == obj.MenuCategoryId).MenuItems.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult EditCategory(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.MenuCategories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(MenuCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.MenuCategories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult EditItem(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var itemFromDb = _db.MenuItems.Find(id);

            if (itemFromDb == null)
            {
                return NotFound();
            }

            var menu = _db.Menus.First(menu => menu.UserId == UserManager.GetUserAsync(User).Result.Id);
            var categories = _db.MenuCategories.Where(c => c.MenuId == menu.Id).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(itemFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(MenuItem obj)
        {
            if (ModelState.IsValid)
            {
                _db.MenuItems.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.MenuCategories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategoryConfirm(MenuCategory obj)
        {
            _db.MenuCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var itemFromDb = _db.MenuItems.Find(id);

            if (itemFromDb == null)
            {
                return NotFound();
            }

            return View(itemFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItemConfirm(MenuItem obj)
        {
            _db.MenuItems.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
