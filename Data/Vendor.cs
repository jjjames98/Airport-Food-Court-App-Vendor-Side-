using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport_Food_Court_App__Vendor_Side_.Data
{
    public class Vendor :IdentityUser
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; } = false;
        [NotMapped]
        public IFormFile Logo { get; set; }
    }
}
