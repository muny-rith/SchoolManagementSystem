using Org.BouncyCastle.Asn1.Cms;

namespace SchoolManagementSystem
{
    public class Class : IPrototype<Class>,ISelectable
    {
        static public string sep = "___";

        private int classID;
        private string? name = "";
        private int languageID;
        private decimal price;
        private string other;

        
        public int ClassID { get { return classID; } set { classID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int LanguageID { get { return languageID; } set { languageID = value; } }
        public Language Language { get; set; } = new Language();
        public decimal Price { get { return price; } set { price = value; } }
        public string Other { get { return other; } set { other = value; } }    

        public bool IsSelected { get; set; }
        public object Tag { get; set; }

        public List<ClassTeacher> ClassTeachers { get; set; } = new List<ClassTeacher>();
        #region method
        public string getInfoForFile { get { return classID + sep + name +sep + languageID + sep + price; } }


        public Class clone()
        {
            return new Class()
            {
                classID = this.ClassID,
                name = this.Name,
                languageID = this.LanguageID,
                price = this.Price,
                Tag = this.Tag
            };

        }
        #endregion
        //public int gradeClass { get; set; }
    }
}