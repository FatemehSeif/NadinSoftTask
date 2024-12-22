using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProduceDate { get; set; }

        [Required]
        [Phone]
        public string ManufacturePhone { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public string CreatedById { get; set; } 
        public User CreatedBy { get; set; }
    }
}
