using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class mgmLanguage
    {
        public List<Language> languages = new List<Language>();
        string query = $@"SELECT * FROM {Constants.tbLanguage}";
        public List<Language> loadDataFromDB(SqlConnection conn,string q = null)
        {
            if(!q.IsNullOrEmpty())
            {
                query = q;
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    languages.Add(new Language
                    {
                        LanguageID = Convert.ToInt32(reader["LanguageID"]),
                        Title = Convert.ToString(reader["Title"])
                    });
                }
            }
            return languages;
        }
    }
}
