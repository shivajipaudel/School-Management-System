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
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
            DisplayAttendance();
            
        }
        int Id;
        int index;
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        
        private void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.ResetText();
            comboBox1.ResetText();
        }
        private void DisplayAttendance()
        {
            Conn.Open();
            String cmd = "Select* from AttendanceTable";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Fill all data");
            }
            else
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand("insert into AttendanceTable values(@StName,@StRoll,@Date,@StStatus)",Conn);
                cmd.Parameters.AddWithValue("@StName", textBox1.Text);
                cmd.Parameters.AddWithValue("@StRoll", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@StStatus", comboBox1.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
               
                MessageBox.Show("Data added");
                Conn.Close();
                DisplayAttendance();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Update AttendanceTable set StName=@StName,StRoll=@StRoll,Date=@Date,StStatus=@StStatus where Id=@Id",Conn);
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.Parameters.AddWithValue("@StName", textBox1.Text);
            cmd.Parameters.AddWithValue("@StRoll", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@StStatus", comboBox1.SelectedItem.ToString());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Data Updated");
            Conn.Close();
            DisplayAttendance();
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Delete AttendanceTable where Id=@Id", Conn);

            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            Conn.Close();
            DisplayAttendance();
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

            Id = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            comboBox1.Text = row.Cells[4].Value.ToString();
        }
    }
}
