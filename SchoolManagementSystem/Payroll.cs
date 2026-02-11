using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Payroll : ISelectable
    {
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Rate { get; set; }
        public decimal Other { get; set; }
        public bool IsSelected { get; set; }
        public object Tag { get; set; }
    }
}
