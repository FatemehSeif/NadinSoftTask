using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AccountDtos
{
    public class UserDto
    {
        public  string Email { get; set; }
        public string FullName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
