using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class mgmPayment
    {
        DatabaseConnection con = DatabaseConnection.Instance;
        public event Action<List<Payment>> onModify; 

        public mgmPayment() { }
        private List<Payment> payments = new List<Payment>();
        public List<Payment> Payments { get { return payments; } set { payments = value; } }
        string query = $@"
                        SELECT 
                            p.PaymentID, p.StudentClassTeacherID, p.PayDate, p.Duration, p.StartDate, p.EndDate, p.Bill, p.Discount, p.DiscountValue, p.Amount,
                            s.StudentID, s.NameEng, s.NameKh,s.Sex , s.Photo AS StudentPhoto, 
                            ct.ClassTeacherID,
                            c.ClassID, c.Name AS ClassName ,c.Price
                            
                        FROM {Constants.tbPayment} p
                        JOIN {Constants.tbStudentClassTeacher} sct ON p.StudentClassTeacherID = sct.StudentClassTeacherID
                        JOIN {Constants.tbStudent} s ON sct.StudentID = s.StudentID
                        JOIN {Constants.tbClassTeacher} ct ON sct.ClassTeacherID = ct.ClassTeacherID
                        JOIN {Constants.tbClass} c ON ct.ClassID = c.ClassID
                        WHERE p.IsActive = 1
                        ORDER BY p.PaymentID DESC;
                    ";

        public void AddPayment(Payment payment)
        { 
            //payments.Add(payment);
            payments.Insert(0, payment);
            onModify?.Invoke(payments);
        }
        public void updatePayment(Payment payment)
        {
            for (int i = 0; i < payments.Count; i++)
            {
                if (payment.PaymentID == payments[i].PaymentID)
                {
                    payments[i] = payment;
                    break;
                }
            }
            onModify?.Invoke(payments);
        }
        public void DeletePayment(Payment payment) 
        {
            for (int i = 0; i < payments.Count; i++)
            {
                if (payment.PaymentID== payments[i].PaymentID)
                {
                    payments.RemoveAt(i);
                    break;
                }
            }
            onModify?.Invoke(payments);
        }
        //public List<Payment> loadDataFromFile(string fileName)
        //{
        //    List<string> lines = File.ReadAllLines(fileName).ToList();

        //    foreach (string line in lines)
        //    {
        //        string[] arr = line.Split(Payment.sep);
        //        if(arr.Length == (Payment.countEle+1))
        //        {
        //            Payment p = new Payment();
        //            p.PaymentID = arr[0];
        //            p.StudentID = arr[1];
        //            p.GradeID = arr[2];

        //            DateTime.TryParse(arr[3], out DateTime pd);
        //            p.PayDate = pd;
        //            int.TryParse(arr[4], out int n);
        //            p.Duration = n;
        //            DateTime.TryParse(arr[5], out DateTime sd);
        //            p.StartDate = sd;
        //            DateTime.TryParse(arr[6], out DateTime ed);
        //            p.EndDate = ed;

        //            decimal.TryParse(arr[7], out decimal b);
        //            p.Bill = b;
        //            decimal.TryParse(arr[8],out decimal o);
        //            p.Discount = o;
        //            decimal.TryParse(arr[9], out decimal a);
        //            p.Amount = a;
        //            payments.Add(p);
        //        }
        //     }
        //    return payments;
        //}



        public void loadDataFromDB(SqlConnection con, string q=null, Dictionary<string, object>? parameters = null)
        {
            if(!string.IsNullOrEmpty(q))query = q;
            payments.Clear();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
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
                        payments.Add(new Payment
                        {
                            PaymentID = Convert.ToInt32(reader["PaymentID"]),
                            StudentClassTeacherID = Convert.ToInt32(reader["StudentClassTeacherID"]),
                            PayDate = reader.GetDateTime(reader.GetOrdinal("PayDate")),
                            Duration = Convert.ToInt32(reader["Duration"]),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            Bill = Convert.ToDecimal(reader["Bill"]),
                            Discount = Convert.ToDecimal(reader["Discount"]),
                            DiscountValue = Convert.ToDecimal(reader["DiscountValue"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),

                            StudentClassTeacher = new StudentClassTeacher
                            {
                                StudentClassTeacherID = Convert.ToInt32(reader["StudentClassTeacherID"]),
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                Student = new Student
                                {
                                    StudentID = Convert.ToInt32(reader["StudentID"]),
                                    NameEng = reader["NameEng"].ToString(),
                                    NameKh = reader["NameKh"].ToString(),
                                    Sex = reader["Sex"].ToString(),
                                    Photo = reader["StudentPhoto"].ToString()
                                },
                                ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                                ClassTeacher = new ClassTeacher
                                {
                                    ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                                    ClassID = Convert.ToInt32(reader["ClassID"]),
                                    Class = new Class
                                    {
                                        ClassID = Convert.ToInt32(reader["ClassID"]),
                                        Name = reader["ClassName"].ToString(),
                                        Price = Convert.ToDecimal(reader["Price"])
                                    }
                                }
                            }
                        });
                    }
                }
            }
        }

        ////public List<Payment> findPaymetnsByStudent(Student student)
        //{
        //    List<Payment> pays = new List<Payment>();
        //    int i = 0;
        //    mgmPayment mgmPayment = new mgmPayment();
        //    mgmPayment.loadDataFromDB(DatabaseConnection.Instance.GetConnection());
        //    foreach (Payment p in mgmPayment.Payments)
        //    {
        //        if (string.Equals(student.StudentID?.Trim(),p.StudentID?.Trim(),StringComparison.OrdinalIgnoreCase))
        //        {
        //            pays.Add(p);
        //        }
        //    }
        //    return pays;
        //}
        //public static void updateFile(List<Payment> list ,string file)
        //{
        //    File.Delete(file);
        //    string[] lines = new string[list.Count];
        //    int i = 0;
        //    foreach(Payment p in list)
        //    {
        //        lines[i] = p.getInfoForFile;
        //        i++;
        //    }
        //    File.WriteAllLines(file,lines);
        //}
    }
}
