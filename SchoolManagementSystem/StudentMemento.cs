using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace SchoolManagementSystem
{
    public class StudentMemento
    {

        #region field
        //List<Student> Students = new List<Student> { };

        private int studentID;
        private string nameEng = "unknow";
        private string nameKh = "unknow";
        private string sex = "unknow";
        private string gradeID = "0001";
        private string gradeName = "unknow";
        private DateTime dob = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private string pob = "unknow";
        private string currentPlace = "unknow";
        private string status = "unkonw";
        private string father = "unknow";
        private string mother = "unkonw";
        private int memberFamily = 0;
        private DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private string phone = "unknow";
        private string filePic = "";

        private DateTime changeAt;

        #endregion
        #region properties
        //public List<Student> listStudents { get { return Students; } set { this.Students = value; } }
        public int StudentID { get { return this.studentID; } set { this.studentID = value; } }
        public string NameEng { get { return this.nameEng; } set { this.nameEng = value; } }
        public string NameKh { get { return this.nameKh; } set { this.nameKh = value; } }
        public string Sex { get { return this.sex; } set { this.sex = value; } }
        public string GradeID { get { return this.gradeID; } set { this.gradeID = value; } }
        public string GradeName { get { return this.gradeName; } set { this.gradeName = value; } }
        public DateTime DOB { get { return this.dob; } set { this.dob = value; } }
        public string POB { get { return this.pob; } set { this.pob = value; } }
        public string CurrentPlace { get { return this.currentPlace; } set { currentPlace = value; } }
        public string Status { get { return status; } set { status = value; } }
        public string Father { get  { return father; } set { father = value; } }
        public string Mother { get { return mother; } set { mother = value; } }
        public int MemberFamily { get { return this.memberFamily; } set { this.memberFamily = value; } }
        public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
        public DateTime EndDate { get { return this.endDate; } set { this.endDate = value; } }
        public string Phone { get { return this.phone; } set { this.phone = value; } }
        public string FilePic { get { return this.filePic; } set { this.filePic = value; } }

        public DateTime ChageAt { get => this.changeAt;set { this.changeAt = value; } }
        public object Tag { get; set; }
        #endregion

        public StudentMemento(Student s)
        {
            studentID = s.StudentID;
            nameEng = s.NameEng;
            nameKh = s.NameKh;
            sex = s.Sex;
            dob = s.DOB;
            pob = s.POB;
            currentPlace = s.CurrentPlace;
            father = s.Father;
            mother = s.Mother;
            memberFamily = s.Member;

            phone = s.Phone;
            filePic = s.Photo;

            changeAt = DateTime.Now;
        }
    }
}
