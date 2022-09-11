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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //Database Connection
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            Conn.Open();
           SqlCommand cmd = new SqlCommand("select*from RegisterTable where Email='"+textBox1.Text+"' and Password='"+textBox2.Text+"'  ",Conn);
           SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read() == true)
            {
                MainMenu mn = new MainMenu();
                mn.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalide Email or Password");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            Conn.Close();

        }
        private void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        //Call Clear funcation
        private void label5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //Exit to this application
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       //Redirect to the Registration page 
        private void label7_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            rg.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }
        
        //Show password
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
