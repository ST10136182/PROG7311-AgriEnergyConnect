using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace Agri_Energy_Connect_Platform.Models
{


    public class Products
    {
        public int ProductsId { get; set; }
        public int FarmersId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public Farmers Farmer { get; set; }
    }

}

