﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AccountDtos
{
    public class EmailDto
    {   public EmailDto(string to, string subject, string body)
            {
                To = to;
                Subject = subject;
                Body = body;
            }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        
    }
}
