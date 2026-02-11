using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class mgmTeacher
    {
        string query = $"select * from {Constants.tbTeacher} WHERE IsActive = 1 ORDER BY NameEng";

        public List<Teacher> teachers = new List<Teacher>();
        public void loadDataFromDB(SqlConnection conn, string query = null, Dictionary<string, object> parameters = null)
        {
            if (!string.IsNullOrEmpty(query))
            {
                this.query = query;
            }

            teachers.Clear(); // Clear old data before loading new

            using (SqlCommand cmd = new SqlCommand(this.query, conn))
            {
                // Add parameters if any
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
                        teachers.Add(new Teacher
                        {
                            TeacherID = Convert.ToInt32(reader["TeacherID"]),
                            NameEng = reader["NameEng"].ToString(),
                            NameKh = reader["NameKh"].ToString(),
                            Sex = reader["Sex"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Photo = reader["Photo"].ToString()
                        });
                    }
                }
            }
        }

    }
}
