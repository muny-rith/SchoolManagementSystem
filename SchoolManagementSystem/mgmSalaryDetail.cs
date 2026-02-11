
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal class mgmSalaryDetail
    {
        public List<SalaryDetail> salaryDetails { get; set; } = new List<SalaryDetail> { };
        public List<SalaryDetail> loadDataFromDB(SqlConnection conn, string q, Dictionary<string, object> para)
        {
            // Base query (default if q is empty)
            var result = new List<SalaryDetail>();

            string query = string.IsNullOrWhiteSpace(q) ? @"
                SELECT 
                    sd.SalaryDetailID,
                    sd.SalaryID,
                    sd.StudentID,
                    sd.IsPaid,
                    sd.Amount,
                    s.NameEng AS StudentName,
                    s.NameKh  AS StudentNameKh,
                    s.Sex
                FROM SalaryDetail sd
                INNER JOIN Student s ON sd.StudentID = s.StudentID
        
            " : q;

            // Dynamically add filters
            if (para != null)
            {
                if (para.ContainsKey("@SalaryID"))
                    query += " AND sd.SalaryID = @SalaryID";

                if (para.ContainsKey("@StudentID"))
                    query += " AND sd.StudentID = @StudentID";

                if (para.ContainsKey("@MinAmount"))
                    query += " AND sd.Amount >= @MinAmount";

                if (para.ContainsKey("@MaxAmount"))
                    query += " AND sd.Amount <= @MaxAmount";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
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
                        result.Add(new SalaryDetail
                        {
                            SalaryDetailID = reader.GetInt32(reader.GetOrdinal("SalaryDetailID")),
                            SalaryID = reader.GetInt32(reader.GetOrdinal("SalaryID")),
                            StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            IsPaid = reader.GetBoolean(reader.GetOrdinal("IsPaid")),
                            Student = new Student()
                            {
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                NameEng = reader.GetString(reader.GetOrdinal("StudentName")),
                                NameKh = reader.GetString(reader.GetOrdinal("StudentNameKh")),
                                Sex = reader.GetString(reader.GetOrdinal("Sex"))
                            }

                        });
                    }

                    // TODO: bind result to DataGridView or return it
                }
            }
            salaryDetails = result;

            return result;

        }

    }
}
