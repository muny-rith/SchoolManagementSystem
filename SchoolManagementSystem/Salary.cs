using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Salary : ISelectable
    {
        int salaryID;
        int classTeacherID;
        int month;
        int year;
        decimal baseSalary;
        decimal gift;
        decimal other;
        decimal total;

        public int SalaryID { get { return salaryID; } set { salaryID = value; } }
        public int ClassTeacherID { get { return classTeacherID; } set { classTeacherID = value; } }
        public ClassTeacher ClassTeacher { get; set; }
        public int Month { get =>  month; set { month = value; } }
        public int Year { get =>  year; set { year = value; } }
        public decimal BaseSalary { get { return baseSalary; } set { baseSalary = value; } }
        public decimal Gift { get { return gift; }set { gift = value; } }
        public decimal Other { get { return other; } set { other = value; } }
        public decimal Total { get { return total; } set { total = value; } }
        public object Tag { get; set; }
        public bool IsSelected { get; set; }

        public List<SalaryDetail> SalaryDetails { get; set; }
    }
}
