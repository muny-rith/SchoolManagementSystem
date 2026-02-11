using Microsoft.Data.SqlClient;

namespace SchoolManagementSystem
{
    public class mgmStudent: IIterator<Student>
    {
        public event Action<List<Student>> onModify;
        string queryLoadData = $@"
                        SELECT 
                            s.StudentId,
                            s.NameEng,
                            s.NameKh,
                            s.Sex,
                            s.DOB,
                            s.POB,
                            s.CurrentPlace,
                            s.Father,
                            s.Mother,
                            s.Member,
                            s.Phone,
                            s.Photo
                        FROM {Constants.tbStudent} s
                        Where s.IsActive = 1;
                ";
        string queryLoadDataStop = $@"
                        SELECT 
                            s.StudentId,
                            s.NameEng,
                            s.NameKh,
                            s.Sex,
                            s.DOB,
                            s.POB,
                            s.CurrentPlace,
                            s.Father,
                            s.Mother,
                            s.Member,
                            s.Phone,
                            s.Photo
                        FROM {Constants.tbStudent} s
                        Where s.IsActive = 0;
                ";
        #region field
        List<Student> students = new List<Student>();
        private int index;
        //private string sep = Student.sep;
        #endregion

        #region property
        public List<Student> Students { get { return students; } set { students = value; } }
        #endregion

        #region method
        public void LoadDataStopFromDB(SqlConnection conn, string query = null)
        {
            if(query != null)
            {
                queryLoadDataStop = query;
            }
            loadDataFromDB(conn, queryLoadDataStop);

        }
        public void loadDataFromDB(SqlConnection conn,string query=null)
        {
            students.Clear();
            using (conn)
            {
                //conn.Open();
                if (query == null)
                {
                    query = $@"
                        SELECT 
                            s.StudentId,
                            s.NameEng,
                            s.NameKh,
                            s.Sex,
                            s.DOB,
                            s.POB,
                            s.CurrentPlace,
                            s.Father,
                            s.Mother,
                            s.Member,
                            s.Phone,
                            s.Photo
                        FROM {Constants.tbStudent} s
                        Where s.IsActive = 1;
                ";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = Convert.ToInt32(reader["StudentId"]),
                            NameEng = reader["NameEng"].ToString(),
                            NameKh = reader["NameKh"].ToString(),
                            Sex = reader["Sex"].ToString(),
                            DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                            POB = reader["POB"].ToString(),
                            CurrentPlace = reader["CurrentPlace"].ToString(),
                            Father = reader["Father"].ToString(),
                            Mother = reader["Mother"].ToString(),
                            Member = Convert.ToInt32(reader["Member"]),
                            Phone = reader["Phone"].ToString(),
                            Photo = reader["Photo"].ToString()
                        });

                    }
                }
            }

        }
        //public List<Student> loadDataFromFile(string filename)
        //{

        //    if (!File.Exists(filename))
        //    {
        //        File.Create(filename).Close(); // Create and close to release the handle
        //    }

        //    var stdIn = Console.In;
        //    Console.SetIn(new StreamReader(filename));
        //    string line = null;

        //    while ((line = Console.ReadLine()) != null)
        //    {

        //        string[] arr = line.Split(Student.sep);
        //        if (arr.Length == Student.countFeild)
        //        {
        //            Student student = new Student();
        //            student.StudentID = int.Parse(arr[0]);
        //            student.NameEng = arr[1];
        //            student.NameKh = arr[2];
        //            student.Sex = arr[3];

        //            DateTime.TryParse(arr[5], out DateTime DOB);
        //            student.DOB = DOB;

        //            student.POB = arr[6];

        //            student.CurrentPlace = arr[7];


        //            student.Father = arr[9];
        //            student.Mother = arr[10];

        //            //student.MemberFamily = 
        //            int.TryParse(arr[11], out int mem);
        //            student.Member = mem;

        //            DateTime.TryParse(arr[12], out DateTime start);
        //            student.StartDate = start;

        //            DateTime.TryParse(arr[13], out DateTime end);
        //            student.EndDate = end;

        //            student.Phone = arr[14];

        //            student.Photo = arr[15];

        //            students.Add(student);
        //        }
        //    }
        //    Console.In.Close();
        //    Console.SetIn(stdIn);
        //    return students;
        //}

        //public void addFile(string fileName,Student student)
        //{
        //    string newstudent = student.getInfoForFile;
        //    File.AppendAllText(fileName, newstudent + "\n");
        //    onModify?.Invoke(students);
        //}

        //public void updateFile(Student student, string filename)
        //{
        //    List<string> lines = new List<string>();
        //    foreach(Student s in students)
        //    {
        //        lines.Add(s.getInfoForFile);
        //    }
        //    File.Delete(filename);
        //    File.WriteAllLines(filename,lines);
        //    onModify.Invoke(students);
        //}
        //public void moveTempory(Student student)
        //{
        //    string fileTempory = "fileTempory.txt";
        //    if (!File.Exists(fileTempory))
        //    {
        //        File.Create(fileTempory);
        //    }
        //    File.AppendAllText(fileTempory,student.getInfoForFile+"\n");
        //}
        //public void deleteFile(Student student, string filename)
        //{
        //    List<string> lines = File.ReadAllLines(filename).ToList();
        //    List<string> newLine = new List<string> { };

        //    foreach (string l in lines)
        //    {
        //        string[] line = l.Split(Student.sep);
        //        if (string.Compare(student.StudentID, line[0]) == 0)
        //        {
        //            continue;
        //        }
        //        newLine.Add(l);

        //    }
        //    File.Delete(filename);
        //    File.WriteAllLines(filename, newLine);
        //    onModify.Invoke(Students);
        //}

        #region new method
        public void addStudent(Student student)
        {
            students.Add(student);
            onModify?.Invoke(students);
        }
        public void updateStudent(Student student)
        {
            
            for(int i = 0; i<students.Count ; i++)
            {
                if(student.StudentID== students[i].StudentID)
                {
                    students[i] = student;
                    break;
                }
            }
            onModify?.Invoke(students);
        }
        public void removeStudent(Student student)
        {
            for(int i = 0;i<students.Count ;i++)
            {
                if (student.StudentID == students[i].StudentID)
                {
                    students.Remove(students[i]);
                }
            }
            onModify?.Invoke(students);
        }
        // search in listStudent
        public List<Student> searchSudents(string name)
        {
            List<Student> studentsFound = new List<Student> ();
            string inputName = name.Replace(" ", "").Trim();

            foreach (Student stu in Students)
            {
                string stuNameEng = stu.NameEng.Replace(" ", "").Trim();
                string stuNameKh = stu.NameKh.Replace(" ", "").Trim();

                if (stuNameEng.Contains(inputName, StringComparison.OrdinalIgnoreCase) || stuNameKh.Contains(inputName,StringComparison.OrdinalIgnoreCase))
                {
                    studentsFound.Add(stu);
                }
            }

            return studentsFound;
        }

        public Student current()
        {
            return students[index];
        }
        public void next()
        {
            index++;
        }
        public bool hasNext()
        {
            return index< students.Count();
        }
        #endregion

        #endregion
    }
}
