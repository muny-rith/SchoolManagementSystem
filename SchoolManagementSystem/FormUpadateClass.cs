using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Collections;

namespace SchoolManagementSystem
{
    public partial class FormUpadateClass : Form
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        public event Action onModify;
        string fileName = "dataGrade.txt";
        mgmClass mgmClass = null;
        mgmLanguage mgmLanguage = new mgmLanguage();
        Class cls = new Class();
        public Class gradeData { get => this.cls; }
        ~FormUpadateClass() { }
        public FormUpadateClass(Class cl)
        {
            InitializeComponent();
            mgmLanguage.loadDataFromDB(conn.GetConnection());

            cbTypeGrade.Items.Clear();
            foreach (var item in mgmLanguage.languages)
            {
                cbTypeGrade.Items.Add(new Item { ID = item.LanguageID, Name = item.Title });
            }

            if (cl != null) // update
            {
                loadDataFromMainForm(cl);
            }
            else // insert
            {
                string selectLastID = $@"SELECT TOP 1 ClassID
                FROM {Constants.tbClass}
                ORDER BY ClassID DESC;";
                using (SqlCommand cmd = new SqlCommand(selectLastID, conn.GetConnection()))
                {
                    int lastID = cmd.ExecuteScalar() is object val ? Convert.ToInt32(val) : 0;
                    txtIdGrade.Text = (lastID + 1).ToString("D3");
                }
                mgmClass = new mgmClass();
                mgmClass.loadDataFromDB(conn.GetConnection());
            }

            btnYes.Click += (s, e) => doClickSave();
            btnNo.Click += (s, e) => this.Close();
        }

        //private void doCustomeDateTime()
        //{
        //    dtStartTime.Format = DateTimePickerFormat.Custom;
        //    dtStartTime.CustomFormat = "H:mm"; // or "HH:mm"
        //    dtStartTime.ShowUpDown = true;
        //    dtEndTime.Format = DateTimePickerFormat.Custom;
        //    dtEndTime.CustomFormat = "H:mm"; // or "HH:mm"
        //    dtEndTime.ShowUpDown = true;
        //}


        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // This will be shown in the ComboBox
            }
        }

        private void doClickSave()
        {
            if (txtNameGrade.Text.IsNullOrEmpty() || txtPriceGrade.Text.IsNullOrEmpty() || cbTypeGrade.SelectedItem is null)
            {
                MessageBox.Show("Pls fill information!");
            }
            else
            {
                setdata();
                DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        public void showInfor()
        {
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnOk.Visible = true;
            btnOk.Click += (s, e) => this.Close();
        }
  
        #region load data and get load data
        private void setdata()
        {
            cls.ClassID = int.Parse(txtIdGrade.Text);
            cls.Name = txtNameGrade.Text;
            decimal.TryParse(txtPriceGrade.Text, out decimal price);
            cls.Price = price;
            cls.Other = txtOther.Text;


            if (cbTypeGrade.SelectedItem == null || string.IsNullOrWhiteSpace(cbTypeGrade.SelectedItem.ToString()))
            {
                if (cbTypeGrade.Items.Count > 0)
                {
                    cbTypeGrade.SelectedIndex = 0; // Select first item
                    //cls.LanguageID = cbTypeGrade.SelectedIndex;
                    if(cbTypeGrade.SelectedItem is Item item)
                    {
                        cls.LanguageID = item.ID;
                        cls.Language.LanguageID = item.ID;
                        cls.Language.Title = item.Name;
                    }
                }
                else
                {
                    MessageBox.Show("No Language available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if ComboBox is empty
                }
            }
            else
            {
                if(cbTypeGrade.SelectedItem is Item item)
                {
                    cls.LanguageID = item.ID;
                    cls.Language.LanguageID = item.ID;
                    cls.Language.Title = item.Name;
                }
            }

        }
        private void loadDataFromMainForm(Class gr)
        {
            txtIdGrade.Text = gr.ClassID.ToString();
            txtNameGrade.Text = gr.Name;
            cbTypeGrade.Text = gr.Language.Title.ToString();
            txtPriceGrade.Text = gr.Price.ToString();
        }
        #endregion

        #region option

        private void doReset(object? sender, EventArgs e)
        {
            //loadDataFromMainForm(gr);
        }
        private void doClear(object? sender, EventArgs e)
        {
            foreach (Control item in this.panelCover.Controls)
            {
                cbTypeGrade.SelectedItem = default;
                if (item is TextBox)
                {
                    ((TextBox)item).Text = null;
                }
            }
        }
        public void doReadOnly()
        {
            foreach (Control o in panelCover.Controls)
            {
                o.TabStop = false;
                cbTypeGrade.Enabled = false;
                if (o is TextBox)
                {
                    ((TextBox)o).ReadOnly = true;
                    ((TextBox)o).BorderStyle = BorderStyle.None;
                }
            }
        }
        #endregion
    }
}
