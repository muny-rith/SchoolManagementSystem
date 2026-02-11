using System.Runtime.Remoting;

namespace SchoolManagementSystem
{
    public class TeacherSalarySummary : ISelectable
    {
        public int TeacherID { get; set; }
        public string TeacherNameEng { get; set; }  // added
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalGift { get; set; }
        public decimal TotalOther { get; set; }
        public decimal TotalSalary { get; set; }
        public bool IsSelected { get; set; }
        public Object Tag { get; set; }

    }
    //public partial class mgmSalary
    //{



    //}
}
