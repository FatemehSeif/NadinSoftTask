﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public override string Email {  get; set; }
        public string FullName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
