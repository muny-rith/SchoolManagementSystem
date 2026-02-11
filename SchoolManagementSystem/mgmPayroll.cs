using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class mgmPayroll
    {
        public List<Payroll> payrollTeachers = new List<Payroll>();

        public List<Payroll> loadDataFromDB(SqlConnection conn, string q = null, Dictionary<string, object> para = null)
        {
            payrollTeachers.Clear();
            // Use custom query if provided
            string query = string.IsNullOrEmpty(q) ? @"
                SELECT pt.TeacherID, 
                       pt.TeacherID, 
                       pt.BaseSalary,
                       pt.Rate,
                       t.NameEng,
                       t.NameKh
                FROM Payroll pt
                INNER JOIN Teacher t 
                    ON pt.TeacherID = t.TeacherID
            " : q;

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add parameters if any
                if (para != null)
                {
                    foreach (var p in para)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var payroll = new Payroll
                        {
                            TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                            Teacher = new Teacher
                            {
                                TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                                NameEng = reader.GetString(reader.GetOrdinal("NameEng")),
                                NameKh = reader.GetString(reader.GetOrdinal("NameKh"))
                            },
                            BaseSalary = reader.GetDecimal(reader.GetOrdinal("BaseSalary")),
                            Rate = reader.GetDecimal(reader.GetOrdinal("Rate"))
                        };

                        payrollTeachers.Add(payroll);
                    }
                }
            }

            // Populate BaseSalary and TotalSalary AFTER reader is closed to avoid MARS issues
            //foreach (var pay in payrollTeachers)
            //{
            //    pay.BaseSalary = (GetTeacherBaseSalary(conn, pay.TeacherID)*pay.Rate)/100;
            //    pay.TotalSalary = pay.BaseSalary - pay.Other; // Ensure pay.Other is initialized
            //}

            return payrollTeachers;
        }



    }
}
