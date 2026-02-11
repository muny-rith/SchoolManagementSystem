using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Student : IPrototype<Student>,ISelectable
    {

        static public int countFeild = 14 + 1;
        static public string sep = "___";

        #region field
        private int studentID;
        private string nameEng = "";
        private string nameKh = "";
        private string sex = "";
        private DateTime dob = new DateTime();
        private string pob = "";
        private string currentPlace = "";
        private string father = "";
        private string mother = "";
        private int member = 0;
        private string phone = "";
        private string photo = "";
        private bool isActive = true;
        public bool IsSelected { get; set; }


        #endregion

        #region properties
        public int StudentID { get { return this.studentID; } set { this.studentID = value; } }
        public string NameEng { get { return this.nameEng; } set { this.nameEng = value; } }
        public string NameKh { get { return this.nameKh; } set { this.nameKh = value; } }
        public string Sex { get { return this.sex; } set { this.sex = value; } }
        public DateTime DOB { get { return this.dob; } set { this.dob = value; } }
        public string POB { get { return this.pob; } set { this.pob = value; } }
        public string CurrentPlace { get { return this.currentPlace; } set { currentPlace = value; } }
        public string Father { get { return this.father; } set { this.father = value; } }
        public string Mother { get =>  this.mother; set { this.mother = value; } }
        public int Member { get { return this.member; } set { this.member = value; } }

        public string Phone { get { return this.phone; } set { this.phone = value; } }
        public string Photo { get { return this.photo; } set { this.photo = value; } }
        public bool IsActive { get { return isActive; }set { isActive = value; } }
         public object Tag { get; set; }

        public List<StudentClassTeacher> StudentClassTeachers { get; set; } = new List<StudentClassTeacher>();

        #endregion

        #region method 

        //public string getInfoForFile
        //{
        //    get { return studentID + sep + nameEng + sep + nameKh + sep + sex + sep + gradeID + sep + dob + sep + pob + sep + currentPlace + sep + status + sep + father+ sep+ mother + sep + member + sep + startDate + sep + endDate + sep + phone + sep + photo; }
        //    //$"{student.Id}{sep}{student.NameEng}{sep}{student.NameKh}{sep}{student.Sex}{sep}{student.Cls.gradeclass}{sep}{student.DOB.Month}-{student.DOB.Day}-{student.DOB.Year}{sep}{student.POB}{sep}{student.CurrentPlace}{sep}{student.Status.status}{sep}{student.Parrent[0]}-{student.Parrent[1]}{sep}{student.MemberFamily}{sep}{student.StartDate.Month}-{student.StartDate.Day}-{student.StartDate.Year}{sep}{student.EndDate.Month}-{student.EndDate.Day}-{student.EndDate.Year}{sep}{student.Phone}{sep}{student.FilePic}";
        //}
        //public static Student FromReader(SqlDataReader reader)
        //{
        //    return new Student
        //    {
        //        StudentID = reader["StudentID"].ToString(),
        //        NameEng = reader["NameEng"].ToString(),
        //        NameKh = reader["NameKh"].ToString(),
        //        Sex = reader["Sex"].ToString(),
        //        GradeID = reader["GradeID"].ToString(),
        //        Phone = reader["Phone"].ToString(),

        //        dateDiff = reader["IsPaidToday"].ToString()
        //    };
        //}
        //public Grade findGrade(List<Grade> grades)
        //{
        //    foreach (Grade grade in grades)
        //    {
        //        if (string.Compare(grade.GradeID, GradeID) == 0)
        //        {
        //            return grade;
        //        }
        //    }
        //    return null;
        //}
        public StudentMemento createState()
        {
            return new StudentMemento(this);
        }
        public void restoreState(StudentMemento studentMemento)
        {
            studentID = studentMemento.StudentID;
            nameEng = studentMemento.NameEng;
            nameKh = studentMemento.NameKh;
            sex = studentMemento.Sex;
            dob = studentMemento.DOB;
            pob = studentMemento.POB;
            currentPlace = studentMemento.CurrentPlace;
            father = studentMemento.Father;
            mother = studentMemento.Mother;
            member = studentMemento.MemberFamily;

            phone = studentMemento.Phone;
            photo = studentMemento.FilePic;
        }

        public Student clone()
        {
            return new Student()
            {
                studentID = this.studentID,
                nameEng = this.nameEng,
                nameKh = this.nameKh,
                sex = this.sex,

                dob = this.dob,
                pob = this.pob,
                currentPlace = this.currentPlace,
                father = this.father,
                mother = this.mother,
                member = this.member,

                phone = this.phone,
                photo = this.photo,
                Tag = this.Tag
            };
        }
        #endregion
    }
}
