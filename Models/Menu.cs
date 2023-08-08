using Airport_Food_Court_App__Vendor_Side_.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport_Food_Court_App__Vendor_Side_.Models
{
    public class Menu
    {
        public Menu()
        {
     
        }

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();
    }
}
