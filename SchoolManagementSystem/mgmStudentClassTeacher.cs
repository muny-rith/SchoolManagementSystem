using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public partial class mgmStudentClassTeacher
    {


        DatabaseConnection conn = DatabaseConnection.Instance;
        public List<StudentClassTeacher> studentClassTeachers = new List<StudentClassTeacher>();
        string query = $@"
                SELECT 
                    sct.StudentClassTeacherID,
                    sct.StudentID,
                    sct.ClassTeacherID,
                    sct.StartDate,
                    sct.EndDate,

                    s.NameEng,
                    s.NameKh,
                    s.Sex,
                    s.Phone,
                    s.Photo AS StudentPhoto,
                    s.POB,
                    s.DOB,

                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.LanguageID,
                    c.Price,

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex AS TeacherSex,
                    t.Phone AS TeacherPhone

                FROM {Constants.tbStudentClassTeacher} sct
                INNER JOIN {Constants.tbStudent} s ON s.StudentID = sct.StudentID
                INNER JOIN {Constants.tbClassTeacher} ct ON ct.ClassTeacherID = sct.ClassTeacherID
                INNER JOIN {Constants.tbClass} c ON c.ClassID = ct.ClassID
                INNER JOIN {Constants.tbTeacher} t ON t.TeacherID = ct.TeacherID
                WHERE sct.IsActive = 1
";
        //Dictionary<string, object?> parameters = new Dictionary<string, object?>();
        //Dictionary<string, object?> parameters = new Dictionary<string, object?>();
        public List<StudentClassTeacher> loadDataFromDB(SqlConnection con, string q = null, Dictionary<string, object?> p = null)
        {
            string query;
            List<StudentClassTeacher> list = new();

            if(q!= null)
            {
                query = q;
            }
            else
            {
                query = this.query;
            }
                // Ensure parameters is always available
                Dictionary<string, object?> parameters = p ?? new();
            // Append filters
            if (parameters.ContainsKey("@ClassID") && Convert.ToInt32(parameters["@ClassID"]) != 0)
                query += " AND ct.ClassID = @ClassID"; // only filter if ClassID != 0

            if (parameters.ContainsKey("@TeacherID") && Convert.ToInt32(parameters["@TeacherID"]) != 0)
                query += " AND ct.TeacherID = @TeacherID"; // only filter if TeacherID != 0

            if (parameters.ContainsKey("@ClassTeacherID") && Convert.ToInt32(parameters["@ClassTeacherID"]) != 0)
                query += " AND ct.ClassTeacherID = @ClassTeacherID"; // only filter if ClassTeacherID != 0

            if (parameters.ContainsKey("@SCTID") && Convert.ToInt32(parameters["@SCTID"]) != 0)
                query += " AND sct.StudentClassTeacherID = @SCTID"; // only filter if SCTID != 0

            // etc...
            // etc...

            // Append ORDER BY once, at the end

            using SqlCommand cmd = new SqlCommand(query, con);
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }
            //query += " ORDER BY sct.StudentClassTeacherID;";

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var sct = new StudentClassTeacher
                {
                    StudentClassTeacherID = reader.GetSafe<int>("StudentClassTeacherID"),
                    StudentID = reader.GetSafe<int>("StudentID"),
                    ClassTeacherID = reader.GetSafe<int>("ClassTeacherID"),
                    StartDate = reader.GetSafe<DateTime>("StartDate"),
                    EndDate = reader.GetSafe<DateTime?>("EndDate"),

                    Student = new Student
                    {
                        StudentID = reader.GetSafe<int>("StudentID"),
                        NameEng = reader.GetSafeString("NameEng"),
                        NameKh = reader.GetSafeString("NameKh"),
                        Sex = reader.GetSafeString("Sex"),
                        Phone = reader.GetSafeString("Phone"),
                        Photo = reader.GetSafeString("StudentPhoto"),
                        POB = reader.GetSafeString("POB"),
                        DOB = reader.GetSafe<DateTime>("DOB"),
                       
                    },

                    ClassTeacher = new ClassTeacher
                    {
                        ClassTeacherID = reader.GetSafe<int>("ClassTeacherID"),
                        ClassID = reader.GetSafe<int>("ClassID"),
                        TeacherID = reader.GetSafe<int>("TeacherID"),

                        Class = new Class
                        {
                            ClassID = reader.GetSafe<int>("ClassID"),
                            Name = reader.GetSafeString("ClassName"),
                            Price = reader.GetSafe<decimal>("Price"),
                            LanguageID = reader.GetSafe<int>("LanguageID"),
                        },

                        Teacher = new Teacher
                        {
                            TeacherID = reader.GetSafe<int>("TeacherID"),
                            NameEng = reader.GetSafeString("TeacherNameEng"),
                            NameKh = reader.GetSafeString("TeacherNameKh"),
                            Sex = reader.GetSafeString("TeacherSex"),
                            Phone = reader.GetSafeString("TeacherPhone")
                        }
                    }
                };
                list.Add(sct);

            }
            studentClassTeachers = list;
            return list;

            //public void LoadData(SqlConnection connection)
            //{
            //    string query = @"
            //                SELECT 
            //                    sct.StudentClassTeacherID,
            //                    sct.StudentID,
            //                    sct.ClassTeacherID,
            //                    sct.StartDate,
            //                    sct.EndDate,

            //                    -- Student info
            //                    s.NameEng,
            //                    s.NameKh,
            //                    s.Sex,
            //                    s.Phone,

            //                    -- Grade (optional via ClassTeacher.ClassID → Class → Grade)
            //                    c.Name AS ClassName,

            //                    -- ClassTeacher info
            //                    ct.ClassID,
            //                    ct.TeacherID,

            //                    -- Class info
            //                    c.Name AS ClassName,
            //                    c.LanguageID,

            //                    -- Teacher info
            //                    t.Name AS TeacherName

            //                FROM StudentClassTeacher sct
            //                INNER JOIN Student s ON s.StudentID = sct.StudentID
            //                INNER JOIN ClassTeacher ct ON ct.ClassTeacherID = sct.ClassTeacherID
            //                INNER JOIN Class c ON c.ClassID = ct.ClassID
            //                INNER JOIN Teacher t ON t.TeacherID = ct.TeacherID

            //                ";
            //    //WHERE sct.StudentClassTeacherID = @ID;


            //    using var cmd = new SqlCommand(query, connection);
            //    //cmd.Parameters.AddWithValue("@ID", sctID);

            //    using var reader = cmd.ExecuteReader();
            //    if (reader.Read())
            //    {
            //        // SCT info
            //        StudentClassTeacherID = (int)reader["StudentClassTeacherID"];
            //        StudentID = (int)reader["StudentID"];
            //        ClassTeacherID = (int)reader["ClassTeacherID"];
            //        StartDate = (DateTime)reader["StartDate"];
            //        EndDate = reader["EndDate"] as DateTime?;

            //        // Student
            //        Student = new Student
            //        {
            //            StudentID = StudentID,
            //            NameEng = reader["NameEng"].ToString(),
            //            NameKh = reader["NameKh"].ToString(),
            //            Sex = reader["Sex"].ToString(),
            //            Phone = reader["Phone"].ToString(),
            //            //GradeName = reader["GradeName"]?.ToString()
            //        };

            //        // ClassTeacher
            //        ClassTeacher = new ClassTeacher
            //        {
            //            ClassTeacherID = ClassTeacherID,
            //            ClassID = (int)reader["ClassID"],
            //            TeacherID = (int)reader["TeacherID"],
            //            Class = new Class
            //            {
            //                ClassID = (int)reader["ClassID"],
            //                Name = reader["ClassName"].ToString(),
            //                //LanguageID = reader["Cla"].ToString()
            //            },
            //            Teacher = new Teacher
            //            {
            //                TeacherID = (int)reader["TeacherID"],
            //                NameEng = reader["NameEng"].ToString(),
            //                NameKh = reader["NameKh"].ToString()
            //            }
            //        };
            //    }
            //}

        }

        public List<StudentClassTeacher> GetStudentsPaidForMonth(DateTime now, int classTeacherID)
        {
            DateTime monthStart = new DateTime(now.Year, now.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1); // End of month
            DateTime monthNew = new DateTime(now.Year, now.Month, 1).AddMonths(1);

            var list = new List<StudentClassTeacher>();

            using (conn.GetConnection())
            {

                string query = @"
                            ;WITH LastPayment AS
                            (
                                SELECT p.StudentClassTeacherID, p.PayDate, p.StartDate AS PaymentStartDate, p.EndDate AS PaymentEndDate,
                                       ROW_NUMBER() OVER (PARTITION BY p.StudentClassTeacherID ORDER BY p.PaymentID DESC) AS rn
                                FROM Payment p
                            )
                            SELECT sct.*, s.NameEng, s.NameKh, s.Sex AS StudentSex, 
                                   lp.PayDate, lp.PaymentStartDate, lp.PaymentEndDate
                            FROM StudentClassTeacher sct
                            LEFT JOIN LastPayment lp
                                ON lp.StudentClassTeacherID = sct.StudentClassTeacherID AND lp.rn = 1
                            INNER JOIN Student s ON s.StudentID = sct.StudentID
                            WHERE sct.ClassTeacherID = @ClassTeacherID
                            ORDER BY sct.StudentClassTeacherID;";
                using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ClassTeacherID", classTeacherID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            DateTime? paymentEndDate = reader["PaymentEndDate"] == DBNull.Value
                                                        ? null
                                                        : (DateTime?)reader["PaymentEndDate"];

                            DateTime? payDate = reader["PayDate"] == DBNull.Value
                                                    ? null
                                                    : (DateTime?)reader["PayDate"];
                            DateTime? paymentStartDate = reader["PaymentStartDate"] == DBNull.Value ? null : (DateTime?)reader["PaymentStartDate"];

                            var sct = new StudentClassTeacher
                            {
                                StudentClassTeacherID = (int)reader["StudentClassTeacherID"],
                                StudentID = (int)reader["StudentID"],
                                ClassTeacherID = (int)reader["ClassTeacherID"],
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = reader["EndDate"] as DateTime?,
                                IsActive = (bool)reader["IsActive"],
                                Student = new Student
                                {
                                    StudentID = (int)reader["StudentID"],
                                    NameEng = reader["NameEng"].ToString(),
                                    NameKh = reader["NameKh"].ToString(),
                                    Sex = reader["StudentSex"].ToString(),
                                },

                                // Check if the last payment covers this month
                                //IsPaid = (paymentEndDate >= monthNew) ? true : false,

                                IsPaid = paymentEndDate.HasValue && paymentEndDate.Value >= monthNew,

                                Tag = payDate

                            };

                            list.Add(sct);
                        }
                    }
                }
            }
            studentClassTeachers = list.Where(e => e.IsPaid == true || e.IsActive == true).ToList();
            return list;
        }

    }
}
