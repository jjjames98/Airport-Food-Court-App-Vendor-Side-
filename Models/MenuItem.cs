using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport_Food_Court_App__Vendor_Side_.Models
{
    public class MenuItem
    {
        public MenuItem()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }
        [Required]
        [ForeignKey("Menu")]
        public int MenuCategoryId { get; set; }
        public virtual MenuCategory? MenuCategory { get; set; }

    }
}
