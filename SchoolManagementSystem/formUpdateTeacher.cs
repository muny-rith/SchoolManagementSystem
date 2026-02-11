using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolManagementSystem
{
    public partial class formUpdateTeacher : Form
    {
        public Teacher teacherData = new Teacher();
        string defaultPic = "Resource/System/unknow.jpeg";
        public formUpdateTeacher(Teacher teaher = null)
        {
            InitializeComponent();
            btnOk.Visible = false;

            if (teaher == null) //insert
            {
                cbSex.SelectedIndex = 0;
                pic.ImageLocation = defaultPic;
            }
            else //update
            {
                loadDataFromMainForm(teaher);
            }
            btnYes.Click += (s, e) => doSave();
            btnCancel.Click += (s, e) => this.Close();

            btnSelectImage.Click += (s, e) => doShowChangeFile(s, e);

        }

        private void BtnSelectImage_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void doSave()
        {
            doSetData();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void doSetData()
        {
            teacherData.NameEng = txtNameEng.Text;
            teacherData.NameKh = txtNameKh.Text;
            if(cbSex.SelectedItem == null) cbSex.SelectedIndex = 0;
            teacherData.Sex = cbSex.SelectedItem.ToString();
            teacherData.Address = txtAddress.Text;
            teacherData.Phone = txtPhone.Text;
            if (pic.ImageLocation == null)
            {
                pic.ImageLocation = defaultPic;
            }
            teacherData.Photo = pic.ImageLocation;
        }

        private void loadDataFromMainForm(Teacher data)
        {
            teacherData.TeacherID = data.TeacherID;
            txtNameEng.Text = data.NameEng;
            txtNameKh.Text = data.NameKh;
            cbSex.SelectedItem = data.Sex.ToLower();
            txtAddress.Text = data.Address;
            txtPhone.Text = data.Phone;
            pic.ImageLocation = "Data/Teacher/" + data.Photo;
        }
        public void showInfo()
        {
            btnYes.Visible = false;
            btnCancel.Visible = false;
            btnOk.Visible = true;
            doReadonly();
        }

        private void doReadonly()
        {
            pic.MouseHover -= doShowBtnSelect;
            pic.DoubleClick -= doShowChangeFile;
            foreach(Control control in this.Controls)
            {
                control.TabStop = false;
                if(control is TextBox || control is ComboBox)
                {
                    control.Enabled = false;
                }
                //control.Enabled = false;
            }
            btnOk.TabStop = true;
            btnOk.Click += (s,e) => this.Close();
        }
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

        private void doShowChangeFile(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;) | *.jpg; *.jpeg; *.gif; *.bmp;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //obj.Image = new Bitmap(open.FileName);
                pic.ImageLocation = open.FileName;
                
            }
        }
    }
}
