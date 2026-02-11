using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace SchoolManagementSystem
{

    public partial class mgmSalary
    {
        #region test
        string query = @"
                SELECT
                    t.TeacherID,
                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    SUM(ClassPaidCount * c.Price) AS BaseSalary,
                    SUM(ClassPaidCount * c.Price) +  0 AS TotalSalary
                FROM Teacher t
                INNER JOIN ClassTeacher ct ON t.TeacherID = ct.TeacherID
                INNER JOIN Class c ON ct.ClassID = c.ClassID
                INNER JOIN (
                    -- Count of students who paid per class
                    SELECT sct.ClassTeacherID, COUNT(DISTINCT sct.StudentID) AS ClassPaidCount
                    FROM Payment p
                    INNER JOIN StudentClassTeacher sct ON p.StudentClassTeacherID = sct.StudentClassTeacherID
                    WHERE YEAR(p.PayDate) = @Year
                      AND MONTH(p.PayDate) = @Month
                    GROUP BY sct.ClassTeacherID
                ) pc ON pc.ClassTeacherID = ct.ClassTeacherID
                GROUP BY
                    t.TeacherID,
                    t.NameEng
                ORDER BY
                    t.NameEng;
                ";
        //public List<Salary> loadDataFromDB_SalaryMonth(SqlConnection conn,string q = null,Dictionary<string, object> para = null)
        //{
        //    var salaries = new List<Salary>();

        //    string query = q;

        //    using (var cmd = new SqlCommand(query, conn))
        //    {
        //        if (para != null)
        //        {
        //            foreach (var p in para)
        //            {
        //                cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
        //            }
        //        }

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var salary = new Salary
        //                {
        //                    ClassTeacherID = reader.GetInt32(reader.GetOrdinal("ClassTeacherID")),

        //                    // optional if you SELECT these in SQL
        //                    ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
        //                    ClassName = reader.IsDBNull(reader.GetOrdinal("ClassName"))
        //                                ? null
        //                                : reader.GetString(reader.GetOrdinal("ClassName")),

        //                    BaseSalary = reader.GetDecimal(reader.GetOrdinal("BaseSalary")),
        //                    TotalSalary = reader.GetDecimal(reader.GetOrdinal("TotalSalary"))
        //                };

        //                salaries.Add(salary);
        //            }
        //        }
        //    }

        //    return salaries;
        //}
        #endregion
        private List<Salary> salaries = new List<Salary>();
        public List<Salary> Salaries
        {
            get { return salaries; }
            set { salaries = value; }
        }

        // Query to load salaries filtered by year and month
        private string queryLoadData;

        // Default parameters
        private Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@Year", DateTime.Now.Year },
        { "@Month", DateTime.Now.Month }
    };

        public List<Salary> LoadData(SqlConnection conn, string query = null, Dictionary<string, object> para = null)
        {
            if (!query.IsNullOrEmpty())
            {
                queryLoadData = query; // use passed query
            }
            else
            {
                // default query
                queryLoadData = @$"
                SELECT 
                    s.SalaryID,
                    s.ClassTeacherID,
                    s.Year,
                    s.Month,
                    s.BaseSalary,
                    s.Gift,
                    s.Other,
                    s.Total,

                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.Price AS ClassPrice,

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex AS TeacherSex
                FROM Salary s
                INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
                INNER JOIN Class c ON ct.ClassID = c.ClassID
                INNER JOIN Teacher t ON ct.TeacherID = t.TeacherID
                WHERE s.Year = @Year AND s.Month = @Month
                
                ORDER BY s.SalaryID;
            ";
            }

            if (!para.IsNullOrEmpty())
            {
                parameters = para; // use passed parameters
            }


            var result = new List<Salary>();

            using (SqlCommand cmd = new SqlCommand(queryLoadData, conn))
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salary salary = new Salary
                        {
                            SalaryID = reader.GetInt32(reader.GetOrdinal("SalaryID")),
                            ClassTeacherID = reader.GetInt32(reader.GetOrdinal("ClassTeacherID")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            Month = (int)reader.GetByte(reader.GetOrdinal("Month")), // tinyint → int
                            BaseSalary = reader.GetDecimal(reader.GetOrdinal("BaseSalary")),
                            Gift = reader.GetDecimal(reader.GetOrdinal("Gift")),
                            Other = reader.GetDecimal(reader.GetOrdinal("Other")),
                            Total = reader.GetDecimal(reader.GetOrdinal("Total")),

                            ClassTeacher = new ClassTeacher()
                            {
                                ClassTeacherID = reader.GetInt32(reader.GetOrdinal("ClassTeacherID")),
                                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                                TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),

                                Class = new Class()
                                {
                                    ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                                    Name = reader.GetString(reader.GetOrdinal("ClassName")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("ClassPrice"))
                                },

                                Teacher = new Teacher()
                                {
                                    TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                                    NameEng = reader.GetString(reader.GetOrdinal("TeacherNameEng")),
                                    NameKh = reader.GetString(reader.GetOrdinal("TeacherNameKh")),
                                    Sex = reader.GetString(reader.GetOrdinal("TeacherSex"))
                                }
                            }
                        };

                        result.Add(salary);
                    }
                }
            }
            salaries = result;
            return result;
        }

        public List<TeacherSalarySummary> LoadGroupedByTeacher(SqlConnection conn, string q =null,Dictionary<string,object> para = null)
        {
            if (!q.IsNullOrEmpty())
            {
                query = q; // use passed query
            }
            else
            {
                query = @"
                SELECT 
                    ct.TeacherID,
                    t.NameEng AS TeacherNameEng,
                    SUM(s.BaseSalary) AS TotalBaseSalary,
                    SUM(s.Gift) AS TotalGift,
                    SUM(s.Other) AS TotalOther,
                    SUM(s.Total) AS TotalSalary,
                    Month,
                    Year
                FROM Salary s
                INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
                INNER JOIN Teacher t ON ct.TeacherID = t.TeacherID
                GROUP BY ct.TeacherID, t.NameEng, s.Year , s.Month

                ORDER BY ct.TeacherID;
            ";
            }
            if (!para.IsNullOrEmpty())
            {
                parameters = para; // use passed parameters
            }



            var result = new List<TeacherSalarySummary>();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //int monthInt = Convert.ToInt32(reader.GetByte(reader.GetOrdinal("Month")));

                        result.Add(new TeacherSalarySummary
                        {
                            TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                            TeacherNameEng = reader.GetString(reader.GetOrdinal("TeacherNameEng")),
                            TotalBaseSalary = reader.GetDecimal(reader.GetOrdinal("TotalBaseSalary")),
                            TotalGift = reader.GetDecimal(reader.GetOrdinal("TotalGift")),
                            TotalOther = reader.GetDecimal(reader.GetOrdinal("TotalOther")),
                            TotalSalary = reader.GetDecimal(reader.GetOrdinal("TotalSalary")),
                            Month = Convert.ToInt32(reader.GetByte(reader.GetOrdinal("Month"))),
                            Year = reader.GetInt32(reader.GetOrdinal("Year"))
                        });
                    }
                }



            }

            return result;
        }


    }
}
