using Microsoft.AspNetCore.Identity;

namespace Agri_Energy_Connect_Platform.Models
{
    public class Farmers
    {
        public int FarmersId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
