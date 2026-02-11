using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class SalaryDetail
    {
        public int  SalaryDetailID{get;set;}
        public int SalaryID {get;set;}
        public Salary Salary{get;set;}
        public int StudentID {get;set;}
        public Student Student {get;set;}
        public bool IsPaid {get;set;}
        public decimal Amount {get;set;}
        public object Tag {get;set;}
    }
}
