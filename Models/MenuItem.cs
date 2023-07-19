using System.ComponentModel.DataAnnotations;

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
        public int Price { get; set; }
        public bool IsAvailable { get; set; }


    }
}
