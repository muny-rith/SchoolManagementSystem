using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Teacher   : ISelectable
    {
        public int TeacherID { get; set; }
        public string NameEng { get; set; }
        public string NameKh { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public int PayrollID { get; set; }
        public Payroll Payroll { get; set; }
        public bool IsActive {  get; set; }
        public bool IsSelected { get; set; }

        public object Tag { get; set; }

        public List<ClassTeacher> ClassTeachers { get; set; } = new List<ClassTeacher>();
        public List<Salary> SalaryTeachers { get; set; } = new List<Salary>();

    }
}
