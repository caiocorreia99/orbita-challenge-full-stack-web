using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Portal.Models.DataBase
{
    public partial class Students
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RA { get; set; }
        public string CPF { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? LastLogin { get; set; } = DateTime.Now;
    }
}
