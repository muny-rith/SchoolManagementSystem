using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class Language
    {
        //private int languageID;
        private string title;

        private int languageID;

        public int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }

        //public int LanguageID { get { return languageID; } set { LanguageID = value; } }
        public string Title { get { return title; } set { title = value; } }

    }
}
