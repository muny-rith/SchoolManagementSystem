using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SchoolManagementSystem
{
    public partial class FormSelectClassTeacher : Form
    {
        public int ClassID { get; set; } = 0;
        public int TeacherID { get; set; } = 0;
        public int ClassTeacherID { get; set; } = 0;

        mgmClass mgmClass = new mgmClass();

        mgmTeacher MgmTeacher = new mgmTeacher();

        public event EventHandler onSet;
        DatabaseConnection conn = DatabaseConnection.Instance;
        //string querySelectTeacher = $"SELECT * FROM {Constants.tbTeacher} Where ";
        //string className = "Math"; // Or any class name you have



        string query;
        //$@"
        //    SELECT t.*
        //    FROM {Constants.tbTeacher} t
        //    JOIN {Constants.tbClassTeacher} ct ON t.TeacherID = ct.TeacherID
        //    JOIN {Constants.tbClass} c ON ct.ClassID = c.ClassID
        //    WHERE c.ClassID = @ClassID";


        Dictionary<string, object> parameters = new Dictionary<string, object>();



        public FormSelectClassTeacher(int stuID =0, int ctID = 0, int clsID=0, int teaID =0)
        {
            InitializeComponent();

            if (stuID != 0) //not update insert CT
            {

                mgmClass.loadDataFromDB(conn.GetConnection());

                cbSelectClass.Items.Clear();
                foreach (Class c in mgmClass.Classes)
                {
                    cbSelectClass.Items.Add(new Item { ID = c.ClassID, Name = c.Name });
                }

                cbSelectClass.SelectedIndex = 0;

                if (cbSelectClass.SelectedItem is Item selectedItem)
                {
                    int classID = selectedItem.ID;
                    parameters.Add("@ClassID", classID);
                }
                query = $@"
                    SELECT t.*
                    FROM {Constants.tbTeacher} t
                    JOIN {Constants.tbClassTeacher} ct ON t.TeacherID = ct.TeacherID
                    WHERE ct.ClassID = @ClassID
                    ";
                MgmTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);
                cbSelectTeacher.Items.Clear();

                foreach (Teacher t in MgmTeacher.teachers)
                {
                    cbSelectTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
                }

                if (cbSelectTeacher.Items.Count > 0)
                {
                    cbSelectTeacher.SelectedIndex = 0; // or whatever index is valid
                }
                cbSelectClass.SelectedIndexChanged += (s, e) => cbSelectClass_SelectedIndexChanged(query);

                //cbSelectTeacher.SelectedIndexChanged += cbSelectTeacher_SelectedIndexChanged;

            }
            else // insert & update classTeacher
            {
                if (ctID != 0) // update ClassTeacher unenable select class
                {
                    ClassTeacherID = ctID;
                    //cbSelectClass.Enabled = false;
                }
                mgmClass.loadDataFromDB(conn.GetConnection());
                MgmTeacher.loadDataFromDB(conn.GetConnection());
                cbSelectTeacher.Items.Clear();
                cbSelectClass.Items.Clear();
                cbSelectTeacher.DataSource = MgmTeacher.teachers
                    .Select(t => new Item { ID = t.TeacherID, Name = t.NameEng })
                    .ToList();
                cbSelectTeacher.DisplayMember = "Name";
                cbSelectTeacher.ValueMember = "ID";

                cbSelectClass.DataSource = mgmClass.Classes
                    .Select(c => new Item { ID = c.ClassID, Name = c.Name })
                    .ToList();
                cbSelectClass.DisplayMember = "Name";
                cbSelectClass.ValueMember = "ID";

                // Now SelectedValue works
                if (clsID != 0)
                    cbSelectClass.SelectedValue = clsID;
                if (teaID != 0)
                    cbSelectTeacher.SelectedValue = teaID;




            }
            btnOK.Click += (s, e) => doSet();
        }
        private void cbSelectClass_SelectedIndexChanged(string query)
        {
            if (cbSelectClass.SelectedIndex >= 0)
            {
                //cbSelectTeacher.SelectedIndexChanged -= cbSelectTeacher_SelectedIndexChanged;
                //int classID = Convert.ToInt32(cbSelectClass.SelectedValue);


                var parameters = new Dictionary<string, object>();
                //if (cbSelectClass.SelectedItem is Item selectedItem)
                //{
                //    int classID = selectedItem.ID;
                //    parameters.Add("@ClassID", classID);

                //}
                if (cbSelectClass.SelectedItem is Item item)
                    parameters.Add("@ClassID", item.ID);

                //parameters.Add("@ClassID", ((Item)cbSelectClass.SelectedItem).ID);


                MgmTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);
                cbSelectTeacher.Items.Clear();
                foreach (Teacher t in MgmTeacher.teachers)
                {
                    cbSelectTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
                }
                if(cbSelectTeacher.Items.Count > 0)cbSelectTeacher.SelectedIndex = 0;
                //cbSelectTeacher.SelectedIndexChanged += cbSelectTeacher_SelectedIndexChanged;

            }
        }
        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // This will be shown in the ComboBox
            }
        }

        private void doSet()
        {
            if(cbSelectClass.SelectedIndex != -1 && cbSelectTeacher.SelectedItem != null)
            {
                if (cbSelectClass.SelectedItem is Item selectedClass)
                {
                    ClassID = selectedClass.ID;

                }
                if (cbSelectTeacher.SelectedItem is Item selectedTeacher)
                {
                    TeacherID = selectedTeacher.ID;

                }
                //TeacherID = Convert.ToInt32(cbSelectTeacher.SelectedValue);
                this.DialogResult = DialogResult.OK;
                onSet?.Invoke(this, new EventArgs());
                this.Close();
            }
            else
            {
                MessageBox.Show("Pls try again!");
            }
        }
    }
}
