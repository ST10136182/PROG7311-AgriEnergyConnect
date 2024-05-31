using Microsoft.AspNetCore.Identity;

namespace Agri_Energy_Connect_Platform.Models
{
    public class Employees 
    {
        public int EmployeesId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Position { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
