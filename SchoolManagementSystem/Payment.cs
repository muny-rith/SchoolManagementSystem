using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Payment : IPrototype<Payment>,ISelectable
    {
        public static string sep = "___";
        public static int countEle = 9;
        public Payment() { }

        #region feild
        private int paymentID;

        private int studentClassTeacherID;


        private DateTime payDate = new DateTime();
        private int duration = 0;
        private DateTime startDate = new DateTime();
        private DateTime endDate = new DateTime();

        private decimal bill = 0.00m;
        private decimal discount = 0.00m;
        private decimal discountValue = 0.00m;
        private decimal amount = 0.00m;

        #endregion

        #region properties
        public int PaymentID { get { return paymentID; } set { paymentID = value; } }
        public int StudentClassTeacherID { get {return studentClassTeacherID ; } set { studentClassTeacherID = value; } }
        public StudentClassTeacher StudentClassTeacher { get; set; }

        public DateTime PayDate { get { return payDate; } set { payDate = value; } }
        public int Duration { get { return duration; } set { duration = value; } }
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set {  endDate = value; } }

        public decimal Bill { get { return bill; } set { bill = value; } }
        public decimal Discount { get { return discount; } set { discount = value; } }
        public decimal DiscountValue { get { return discountValue; } set { discountValue = value; } }
        public decimal Amount { get { return amount; } set { amount = value; } }

        public object Tag { get; set; }
        public bool IsSelected { get; set; }
        //public string getInfoForFile { get { return paymentID + sep + studentID + sep + gradeID + sep + payDate + sep + duration + sep + startDate + sep + endDate + sep + bill + sep + discount + sep + amount; } }
        #endregion


        #region method 
        //public Student findStudent(mgmStudent students)
        //{
        //    foreach(var student in students.Students)
        //    {
        //        if (StudentID == student.StudentID) return student;
        //    }
        //    return null;
        //}

        //public Class findGrade(mgmGrade grades)
        //{
        //    foreach(Class grade in grades.Grades)
        //    {
        //        if(string.Compare(gradeID,grade.GradeID)==0) return grade;
        //    }
        //    return null;
        //}

        public Payment clone()
        {
            return new Payment()
            {
            //    paymentID = this.paymentID,
            //    studentID = this.studentID,
            //    gradeID = this.gradeID,
            //    payDate = this.payDate,
            //    duration = this.duration,
            //    startDate = this.startDate,
            //    endDate = this.endDate,
            //    discount = this.discount,
            //    amount = this.amount,
            //    Tag = this.Tag
            };
        }
        #endregion

    }
}
