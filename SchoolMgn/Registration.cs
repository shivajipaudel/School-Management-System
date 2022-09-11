using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SchoolMgn
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text=="" || textBox3.Text==""||textBox4.Text=="")
            {
                MessageBox.Show("Fill all the detail carefully!");
            }
            else if(textBox2.Text==textBox4.Text)
            {
                Conn.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into RegisterTable values(@FName,@Email,@Password,@CPassword)",Conn);
                sqlCommand.Parameters.AddWithValue("@FName", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("@Email", textBox3.Text);
                sqlCommand.Parameters.AddWithValue("@Password", textBox2.Text);
                sqlCommand.Parameters.AddWithValue("@CPassword", textBox4.Text);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Register Successfully!");
                Conn.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }
            
        //Clear input data
        private void label5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        
        //Redirect to login page
        private void label7_Click(object sender, EventArgs e)
        {
            Login ln = new Login();
            ln.Show();
            this.Hide();
        }
    }
}
