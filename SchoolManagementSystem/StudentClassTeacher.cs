using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class StudentClassTeacher : ISelectable
    {
        //public List<StudentClassTeacher> list { get; set; }
        public int StudentClassTeacherID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int ClassTeacherID { get; set; }
        public ClassTeacher ClassTeacher { get; set; }
        public bool IsSelected { get; set; }

        public List<Payment> Payments { get; set; } = new();

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public object Tag { get; set; }

        // ✅ Computed property (C# equivalent of SQL DATEDIFF)
        // How many days left
        public int? DateDiff =>
            (EndDate.HasValue && StartDate.HasValue) && (EndDate == StartDate)
                ? null
                : EndDate.HasValue
                    ? (EndDate.Value - DateTime.Today).Days
                    : (StartDate.Value - DateTime.Today).Days;
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
    }
}
