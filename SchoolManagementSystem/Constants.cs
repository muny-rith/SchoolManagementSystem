//using ComponentFactory.Krypton.Toolkit;

namespace SchoolManagementSystem
{
    public static class Constants
    {
        public static Color firstColor = Color.FromArgb(223, 241, 250);
        public static Color secondColor = ColorTranslator.FromHtml("#C4EBF7");
        public static Color thirdColor = ColorTranslator.FromHtml("#E5A865");
        public static Color color4 = Color.FromArgb(252, 232, 141);
        public static Color color5 = ColorTranslator.FromHtml("#F9F9FB");

        #region file
        public const string StudentFile = "Data/data.txt";
        public const string GradeFile = "Data/dataGrade.txt";
        public const string PaymentFile = "Data/dataPayment.txt";
        public const string ResultMonthlyFile = "Data/dataResultMonthly.txt";
        #endregion

        #region DB
        public const string DB = "dbSMS.mdf";
        public const string tbStudent = "Student";
        public const string tbTeacher = "Teacher";
        public const string tbLanguage = "Language";
        public const string tbClass  = "Class";
        public const string tbPayment = "Payment";
        public const string tbClassTeacher = "ClassTeacher";
        public const string tbStudentClassTeacher = "StudentClassTeacher";
        #endregion
    }
}
