using Google.Protobuf.WellKnownTypes;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class FormEditGrade : KryptonForm
    {
        mgmClass mgmGrade = new mgmClass();
        public event EventHandler aplied;
        public string option { get; set; }
        //public string oldGrade { get; set; }
        public FormEditGrade(string oldGrade)
        {
            mgmGrade.loadDataFromDB(DatabaseConnection.Instance.GetConnection());

            InitializeComponent();
            lbOldGrade.Text = oldGrade;
            //this.Disposed += (s,e) => this.Close();
            this.Deactivate += (s, e) => this.Close();

            //lbOldGrade.Text = 
            ContextMenuStrip menuTypeGrade = new ContextMenuStrip();
            menuTypeGrade.Items.Add("Khmer", null, MenuItemTypeGrade_Click);
            menuTypeGrade.Items.Add("English", null, MenuItemTypeGrade_Click);
            btnFilter.Click += (s, ev) =>
            {
                menuTypeGrade.Show(btnFilter, new Point(0, btnFilter.Height + 5));
            };
            btnCancel.Click += (s, ev) => { this.Close(); };
            btnOk.Click += (s, ev) => { aplied.Invoke(s,EventArgs.Empty); this.Close(); };

        }
        //private void cbNewGrade_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedLanguage = cbNewGrade.SelectedItem.ToString();

        //    List<Grade> filteredGrades = mgmGrade.Grades
        //        .Where(g => g.Type != null && g.Type.Equals(selectedLanguage, StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //    MessageBox.Show(filteredGrades.FirstOrDefault().Name);

        //    // Clear old items
        //    cbNewGrade.Items.Clear();

        //    // Add new filtered items
        //    cbNewGrade.Items.AddRange(filteredGrades.Select(e => e.Name).ToArray());



        //}

        private void MenuItemTypeGrade_Click(object sender, EventArgs e)
        {

            ContextMenuStrip menuKhmer = new ContextMenuStrip();
            ContextMenuStrip menuEng = new ContextMenuStrip();
            ToolStripItem item = sender as ToolStripItem;

            // Clear previous items (optional)
            menuKhmer.Items.Clear();
            menuEng.Items.Clear();

            // Get Khmer grades
            var khmerGrades = mgmGrade.Classes
                .Where(g => !string.IsNullOrEmpty(g.Language.Title) && g.Language.Title.IndexOf("k", StringComparison.OrdinalIgnoreCase) >= 0);

            foreach (Class g in khmerGrades)
            {
                menuKhmer.Items.Add(g.Name, null, MenuItem_Click);
            }

            // Get English grades
            var engGrades = mgmGrade.Classes
                .Where(g => !string.IsNullOrEmpty(g.Language.Title) && g.Language.Title.IndexOf("en", StringComparison.OrdinalIgnoreCase) >= 0);

            foreach (Class g in engGrades)
            {
                menuEng.Items.Add(g.Name, null, MenuItem_Click);

            }

            // Example: show the correct menu depending on what was clicked
            if (item != null && item.Text == "Khmer")
            {
                menuKhmer.Show(btnFilter, new Point(0, btnFilter.Height + 5));

            }
            else if (item != null && item.Text == "English")
            {
                menuEng.Show(btnFilter, new Point(0, btnFilter.Height + 5));
            }

            void MenuItem_Click(object sender, EventArgs e)
            {
                var item = sender as ToolStripItem;
                option = item.Text;
                btnFilter.Text = option;
                //aplied.Invoke(this,e);

            }
        }
    }
}
