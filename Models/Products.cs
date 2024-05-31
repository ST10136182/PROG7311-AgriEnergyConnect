using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
namespace Agri_Energy_Connect_Platform.Models;




    public class Products
    {
        public int ProductsId { get; set; }
        [Required]
        public int FarmersId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        public Farmers Farmer { get; set; }
    }



