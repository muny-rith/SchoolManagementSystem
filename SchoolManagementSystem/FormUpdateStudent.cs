using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1;
using static System.ComponentModel.Design.ObjectSelectorEditor;


namespace SchoolManagementSystem
{
    public partial class FormUpdateStudent : Form
    {
        public event Action<FormUpadateClass> onModify;
        Student studentData = new Student();
        StudentMemento studentMemento = null;
        StudentHistoryChange studentsChange = new StudentHistoryChange();
        mgmStudent students = new mgmStudent();
        mgmPayment payments = new mgmPayment();
        mgmClass classes = new mgmClass();
        public Student StudentData { get => studentData; }
        public DateTime StartDate { get; set; }


        DatabaseConnection con = DatabaseConnection.Instance;

        string defaultPic = "Resource/System/unknow.jpeg";
        
        ~FormUpdateStudent() { }
        public FormUpdateStudent(Student stu)
        {
            InitializeComponent();
            //check grade and add to combo box
            classes.loadDataFromDB(con.GetConnection());
            if (classes.Classes.Count() == 0)
            {
                MessageBox.Show("Pls input Grade first!!");
                this.DialogResult = DialogResult.No;

                this.Close();
            }
            else
            {
                if (stu != null) //update
                {
                    labelStartDate.Visible = false;
                    dtStartDate.Visible = false;
                    loadDataFromMainForm(stu);
                }
                else //insert
                {
                    cbPOB.DataSource = Enum.GetValues(typeof(Provinces));
                    cbSex.SelectedIndex = 0;
                    cbPOB.SelectedIndex = 0;
                    picStudent.ImageLocation = defaultPic;
                }

            }
            btnYes.Click += doClickSave;
            btnNo.Click += (s, e) => this.Close();

        }

        public void showInfor()
        {
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnOk.Visible = true;
            btnOk.Click += (s, e) => this.Close();
        }

        private void doClickSave(object? sender, EventArgs e)
        {
            if( txtNameEng.Text.Any() && txtNameKh.Text.Any() && cbSex.SelectedItem != null && dtStartDate.Value != null)
            {
                setData();
                StartDate = dtStartDate.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fiil information!");
            }

        }
        #region method Data
        private void setData()
        {
            studentData.NameEng = txtNameEng.Text;
            studentData.NameKh = txtNameKh.Text;
            if (cbSex.SelectedItem == null)
            {
                studentData.Sex = "male";
            }
            else
            {
                studentData.Sex = cbSex.SelectedItem.ToString();
            }

            studentData.DOB = dtimeDOB.Value;
            studentData.POB = cbPOB.Text;

            studentData.CurrentPlace = txtCurrentPlace.Text;
            studentData.Father = txtFather.Text;
            studentData.Mother = txtMother.Text;

            //student.MemberFamily = txtMember.Text;
            if (int.TryParse(txtMember.Text, out int value) is false)
            {
                //MessageBox.Show("Member must be number !!");
                studentData.Member = 0;
            }
            else
            {
                studentData.Member = value;

            }

            studentData.Phone = txtPhone.Text;


            if (picStudent.ImageLocation == null || picStudent.ImageLocation == "")
            {
                picStudent.ImageLocation = defaultPic;

            }

            studentData.Photo = picStudent.ImageLocation;


        }
        private void doChangeFilePic(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;) | *.jpg; *.jpeg; *.gif; *.bmp;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //obj.Image = new Bitmap(open.FileName);
                picStudent.ImageLocation = open.FileName;
            }
            //var file = dlg.OpenFile().Position.ToString();
            //MessageBox.Show(file);
        }
        private void loadDataFromMainForm(Student s)
        {
            cbPOB.DataSource = Enum.GetValues(typeof(Provinces));

            // Try to find matching enum
            if (Enum.TryParse<Provinces>(s.POB?.Replace(" ", ""), true, out Provinces enumValue))
            {
                cbPOB.SelectedItem = enumValue;
            }
            else
            {
                // For old data, just set the Text property directly
                cbPOB.SelectedIndex = -1; // Deselect
                cbPOB.Text = s.POB; // Shows "P.P" but user can still select from dropdown
            }


            studentData.StudentID = s.StudentID;
            txtNameEng.Text = s.NameEng;
            txtNameKh.Text = s.NameKh;
            cbSex.SelectedItem = s.Sex;

            dtimeDOB.Value = s.DOB;

            //MessageBox.Show(s.POB);
            //cbPOB.Text = s.POB;

            txtCurrentPlace.Text = s.CurrentPlace;
            //cbStatus.SelectedItem = s.Status;
            txtFather.Text = s.Father;
            txtMother.Text = s.Mother;
            txtMember.Text = s.Member.ToString();


            txtPhone.Text = s.Phone;
            if (s.Photo == string.Empty)
            {
                picStudent.ImageLocation = defaultPic;
            }
            else
            {
                picStudent.ImageLocation = "Data/Student/"+s.Photo;
            }
        }
        #endregion



        #region option
        public void doReadOnly()
        {
            picStudent.DoubleClick -= doChangeFilePic;
            picStudent.MouseHover -= doShowBtnSelect;
            picStudent.Cursor = Cursors.Arrow;
            cbSex.Enabled = false;
            cbStatus.Enabled = false;
            //cbClass.Enabled = false;
            foreach (Control obj in this.panel1.Controls)
            {
                obj.TabStop = false;
                //obj.TabIndex = -1;
                if (obj is TextBox)
                {
                    ((TextBox)obj).ReadOnly = true;
                    ((TextBox)obj).BorderStyle = BorderStyle.None;
                }
                if (obj is DateTimePicker)
                {
                    ((DateTimePicker)obj).Enabled = false;
                }
            }
        }

        private void doClear(object? sender, EventArgs e)
        {
            picStudent.ImageLocation = defaultPic;
            (cbSex.SelectedItem) = default;
            cbStatus.SelectedItem = default;
            foreach (Control obj in this.panel1.Controls)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).Text = string.Empty;
                }
            }
        }

        private void doReset(object? sender, EventArgs e)
        {
            if (studentsChange.GetMemento() != null)
            {
                studentData.restoreState(studentsChange.GetMemento());
            }

            loadDataFromMainForm(studentData);
        }
        #endregion

        #region effect

        private void doShowBtnSelect(object sender, EventArgs e)
        {
            btnSelectImage.Visible = true;
        }

        private void doCloseBtnSelect(object sender, EventArgs e)
        {
            btnSelectImage.Visible = false;
        }

        private void doShowEffect(object sender, EventArgs e)
        {
            var obj = sender as Button;
            obj.BackColor = Color.LightCyan;
            obj.FlatAppearance.BorderColor = Color.LightSkyBlue;
        }

        private void doCloseEffect(object sender, EventArgs e)
        {
            var obj = sender as Button;
            obj.BackColor = Color.Transparent;
            obj.FlatAppearance.BorderColor = Color.Silver;
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}