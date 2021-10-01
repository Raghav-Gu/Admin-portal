﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Models
{
    public class SmtpConfigModel
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
