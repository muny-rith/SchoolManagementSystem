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
    public partial class formInputNumber : Form
    {
        public int number { get; set; }
        public formInputNumber(string name)
        {
            InitializeComponent();
            lbName.Text = name;
            btnOk.Click += (s, e) => save();
            txtNumber.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; // Block non-numeric input
                }
            };

            txtNumber.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnOk.PerformClick();
                }
            };
        }

        void save()
        {
            number = int.TryParse(txtNumber.Text,out int n)? n : 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
