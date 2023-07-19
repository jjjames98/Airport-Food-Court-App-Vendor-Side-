using System.ComponentModel.DataAnnotations;

namespace Airport_Food_Court_App__Vendor_Side_.Models
{
    public class MenuCategory
    {
        public MenuCategory()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
