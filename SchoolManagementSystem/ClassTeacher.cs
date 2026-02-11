using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class ClassTeacher :  ISelectable
    {
        public int ClassTeacherID { get; set; }
        public int ClassID { get; set; }
        public Class Class { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public bool IsSelected { get; set; }
        public object Tag { get; set; }

        public decimal Gift { get; set; } = 0;
        public decimal Other { get; set; } = 0;


        public List<StudentClassTeacher> StudentClassTeachers { get; set; } = new List<StudentClassTeacher>();
        public List<StudentClassTeacher> GetSCTs(SqlConnection conn, int? classId = null, int? teacherId = null)
        {
            var scts = new List<StudentClassTeacher>();
            string query = @"
                    SELECT sct.StudentClassTeacherID,
                           sct.StudentID,
                           sct.ClassTeacherID,
                           sct.StartDate,
                           sct.EndDate,
                           ct.ClassID,
                           ct.TeacherID
                    FROM StudentClassTeacher sct
                    INNER JOIN ClassTeacher ct ON sct.ClassTeacherID = ct.ClassTeacherID
                    WHERE sct.IsActive = 1
                        AND (@ClassID IS NULL OR ct.ClassID = @ClassID)
                        AND (@TeacherID IS NULL OR ct.TeacherID = @TeacherID)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClassID", (object)classId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TeacherID", (object)teacherId ?? DBNull.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        scts.Add(new StudentClassTeacher
                        {
                            StudentClassTeacherID = (int)reader["StudentClassTeacherID"],
                            StudentID = (int)reader["StudentID"],
                            ClassTeacherID = (int)reader["ClassTeacherID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = reader["EndDate"] as DateTime?,
                        });
                    }
                }
            }
            return scts;
        }


    }

}
