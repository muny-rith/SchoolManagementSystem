using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{

    public partial class mgmClassTeacher
    {
        string query = $@"
                SELECT 
                    ct.ClassTeacherID,
                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.Price,
                    c.Other,
                    
                    l.LanguageID,
                    l.Title,  -- Added language name

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex

                FROM {Constants.tbClassTeacher} ct
                INNER JOIN {Constants.tbClass} c 
                    ON ct.ClassID = c.ClassID
                INNER JOIN {Constants.tbTeacher} t 
                    ON ct.TeacherID = t.TeacherID
                INNER JOIN {Constants.tbLanguage} l   -- Extra join
                    ON c.LanguageID = l.LanguageID
                ORDER BY l.Title DESC, c.Price;
            ";
        public List<ClassTeacher> classTeachers = new List<ClassTeacher>();

        public List<ClassTeacher> loadDataFromDB(SqlConnection conn, string q = null, Dictionary<string, object> para = null)
        {
            classTeachers.Clear();

            string cmdText = q ?? query; // Use provided query or default class-level query

            using SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Add parameters if any
            if (para != null)
            {
                foreach (var p in para)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                }
            }

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                #region old
                //ClassTeacher ct = new ClassTeacher
                //{
                //    ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                //    ClassID = Convert.ToInt32(reader["ClassID"]),
                //    TeacherID = Convert.ToInt32(reader["TeacherID"]),
                //    Class = new Class
                //    {
                //        ClassID = Convert.ToInt32(reader["ClassID"]),
                //        Name = reader["ClassName"].ToString(),
                //        Price = reader["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Price"]),
                //        Other = reader["Other"].ToString(),
                //        LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                //        Language = new Language
                //        {
                //            LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                //            Title = reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString()
                //        }
                //    },
                //    Teacher = new Teacher
                //    {
                //        TeacherID = Convert.ToInt32(reader["TeacherID"]),
                //        NameEng = reader["TeacherNameEng"].ToString(),
                //        NameKh = reader["TeacherNameKh"].ToString(),
                //        Sex = reader["Sex"].ToString(),
                //        Payroll = new Payroll
                //        {
                //            TeacherID = Convert.ToInt32(reader["TeacherID"]),
                //            BaseSalary = reader["BaseSalary"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["BaseSalary"]),
                //            Rate = reader["Rate"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Rate"])
                //        }
                //    }
                #endregion
                ClassTeacher ct = new ClassTeacher
                {
                    ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                    ClassID = Convert.ToInt32(reader["ClassID"]),
                    TeacherID = Convert.ToInt32(reader["TeacherID"]),
                    Class = new Class
                    {
                        ClassID = Convert.ToInt32(reader["ClassID"]),
                        Name = reader["ClassName"].ToString(),
                        Price = reader["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Price"]),
                        Other = reader["Other"].ToString(),
                        LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                        Language = new Language
                        {
                            LanguageID = reader["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LanguageID"]),
                            Title = reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString()
                        }
                    },
                    Teacher = new Teacher
                    {
                        TeacherID = Convert.ToInt32(reader["TeacherID"]),
                        NameEng = reader["TeacherNameEng"].ToString(),
                        NameKh = reader["TeacherNameKh"].ToString(),
                        Sex = reader["Sex"].ToString(),
                        Payroll = new Payroll
                        {
                            TeacherID = Convert.ToInt32(reader["TeacherID"]),
                            BaseSalary = reader.HasColumn("BaseSalary") && reader["BaseSalary"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["BaseSalary"])
                                        : 0m,
                            Rate = reader.HasColumn("Rate") && reader["Rate"] != DBNull.Value
                                        ? Convert.ToInt32(reader["Rate"])
                                        : 0
                        }
                    }
                };
                classTeachers.Add(ct);
            }

            return classTeachers;
        }


    }
}
