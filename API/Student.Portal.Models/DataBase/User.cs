﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Portal.Models.DataBase
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; } = true;
        public bool Admin { get; set; } = false;
        public string? Token { get; set; }

    }
}
