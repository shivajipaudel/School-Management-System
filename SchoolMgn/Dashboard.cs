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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Hide();
        }

        private void CountStudent()
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from StTable",Conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StNum.Text= dt.Rows[0][0].ToString();
            Conn.Close();
        }
        private void CountTeacher()
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from TeacherTable", Conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            NmT.Text = dt.Rows[0][0].ToString();
            Conn.Close();
        }
        private void CountEvent()
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from EventTable", Conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            NEv.Text = dt.Rows[0][0].ToString();
            Conn.Close();
        }
        private void SumFee()
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(Amount) from FeesTable", Conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           fee.Text = dt.Rows[0][0].ToString();
            Conn.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            CountStudent();
            CountTeacher();
            CountEvent();
            SumFee();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Hide();
        }

        private void StNum_Click(object sender, EventArgs e)
        {

        }
    }
}
