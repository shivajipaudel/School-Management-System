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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            DisplayFees();
        }
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void DisplayFees()
        {
            Conn.Open();
            String cmd = "Select* from FeesTable";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Conn.Close();
        }

        private void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.ResetText();
            textBox3.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
         if(textBox1.Text=="" || textBox2.Text==""|| dateTimePicker1.Text==""|| textBox3.Text=="")
            {
                MessageBox.Show("Fill all the details");
            }
            else
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand("insert into FeesTable values(@StName,@EnNo,@Date,@Amount)", Conn);
                cmd.Parameters.AddWithValue("@StName", textBox1.Text);
                cmd.Parameters.AddWithValue("@EnNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@Amount", int.Parse(textBox3.Text));
                cmd.ExecuteNonQuery();
               
                MessageBox.Show("Fees Paid Successfully");
                Conn.Close();
                DisplayFees();
                Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
