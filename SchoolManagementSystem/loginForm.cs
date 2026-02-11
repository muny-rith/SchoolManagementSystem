using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SchoolManagementSystem
{
    public partial class loginForm : Krypton.Toolkit.KryptonForm
    {
        DatabaseConnection con = DatabaseConnection.Instance;
        public loginForm()
        {
            //MySqlConnection mySqlConnection;
            //string connectionString = "Server=128.199.113.218:3306;Database=test;Username=root;Password=734658";
            ////_connection = new SqlConnection(connectionString);
            //mySqlConnection = new MySqlConnection(connectionString);
            //mySqlConnection.Open();

            //mySqlConnection.Close();
            InitializeComponent();
            btnLogin.Click += doLogin;
            btnExit.Click += doExit;
        }

        private void doExit(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void doLogin(object? sender, EventArgs e)
        {
            //this.Close();
            string userName = txtUser.Text;
            string password = txtPassword.Text;


            if (!(userName == string.Empty || password == string.Empty))
            {
                string query = $"SELECT COUNT(*) FROM tbUser WHERE userName = @userName AND userPassword = @password";
                // What does @userName mean?
                // @userName is a placeholder.
                //You're telling SQL: "I’ll give you the value for this later."
                //Then, in C#, you assign a value to it like this:
                SqlCommand cmd = new SqlCommand(query, con.GetConnection());

                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);

                int count = (int)cmd.ExecuteScalar();// Use ExecuteScalar for SELECT COUNT(*)
                                                     //string count = cmd.ExecuteNonQuery();
                con.CloseConnection();

                if (count == 1)
                {
                    mainForm mainForm = new mainForm();
                    mainForm.Show();
                    this.Hide();

                }
                else
                {
                    //,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning
                    MessageBox.Show("Invalid userName or Password!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill User Name and Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
