using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace SchoolManagementSystem
{
    public class mgmClass
    {
        #region event

        public event Action<List<Class>> onModify;

        #endregion
        string query = $@"
            SELECT c.ClassID, c.Name, c.LanguageID, c.Price, l.Title, c.Other 
            FROM {Constants.tbClass} c 
            
            JOIN {Constants.tbLanguage} l ON c.LanguageID = l.LanguageID 
            ORDER BY l.Title DESC, c.Price
        ";

        public mgmClass()
        {

        }

        #region field
        private List<Class> classes = new List<Class>();
        #endregion

        public List<Class> Classes { get { return classes; } set { classes = value; } }

        public List<Class> loadDataFromDB(SqlConnection conn, string q = null, Dictionary<string, object> parameters = null)
        {
            if (!q.IsNullOrEmpty())
            {
                query = q;
            }

            classes.Clear(); // Prevent duplicates on multiple calls

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add parameters if provided
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classes.Add(new Class
                        {
                            ClassID = reader["ClassID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClassID"]),
                            Name = reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString(),
                            LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                            Language = new Language
                            {
                                LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                                Title = reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString()
                            },
                            Price = reader["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Price"]),
                            Other = reader["Other"] == DBNull.Value ? string.Empty : reader["Other"].ToString()
                        });
                    }
                }
            }

            return classes;
        }


        #region method
        //public List<Class> loadDataFromFile(string filename)
        //{
        //    List<string> lines = new List<string>();
        //    lines = File.ReadAllLines(filename).ToList();
        //    foreach (string line in lines)
        //    {
        //        Class grade = new Class();
        //        string[] arr = line.Split(Class.sep);
        //        grade.GradeID = arr[0];
        //        grade.Name = arr[1];
        //        grade.Type = arr[2];
        //        //grade.Price = arr[3];
        //        float.TryParse(arr[3], out var pr);
        //        grade.Price = pr;
        //        //if()
        //        grades.Add(grade);
        //    }
        //    return grades;
        //}
        public void addGrade(Class grade)
        {
            classes.Add(grade);
            onModify?.Invoke(classes);
        }
        public void deleteGrade(Class grade)
        {
            for(int i = 0; i< classes.Count; i++)
            {
                if (grade.ClassID== classes[i].ClassID)
                {
                    classes.Remove(classes[i]);
                    break;
                }
            }
            onModify?.Invoke(classes);
        }
        public void updateGrade(Class grade)
        {
            for(int i = 0; i < classes.Count; i++)
            {
                if (grade.ClassID == classes[i].ClassID)
                {
                    classes[i] = grade;
                    break;
                }
            }
            onModify?.Invoke(classes);
        }

        public List<Class> searchGrades(string data)
        {
            List<Class> grades = new List<Class>();

            string inputData = data.Replace(" ", "");
            foreach(Class grade in Classes)
            {
                string gradeName = grade.Name.Replace(" ", "");
                if (gradeName.Contains(inputData, StringComparison.OrdinalIgnoreCase))
                {
                    grades.Add(grade);
                }
            }
            return grades;
        }
        //public 
        #endregion
        
    }
}