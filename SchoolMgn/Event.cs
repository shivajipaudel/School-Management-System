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
    public partial class Event : Form
    {
        public Event()
        {
            InitializeComponent();
            DisplayEvent();
        }
        int EId;
        int index;
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void DisplayEvent()
        {
            Conn.Open();
            String cmd = "Select* from EventTable";
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
            dateTimePicker1.ResetText();
            textBox3.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || dateTimePicker1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Fill all the detail");
            }
            else
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand("insert into EventTable values(@EDes,@EDate,@EDuration)", Conn);
            
                cmd.Parameters.AddWithValue("@EDes", textBox1.Text);
                cmd.Parameters.AddWithValue("@EDate", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@EDuration", textBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Event Added Successfully");
                Conn.Close();
                DisplayEvent();
                Clear();



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Update EventTable set EDes=@EDes,EDate=@EDate,EDuration=@EDuration Where EId=@EId ", Conn);
            cmd.Parameters.AddWithValue("@EID", this.EId);
            cmd.Parameters.AddWithValue("@EDes", textBox1.Text);
            cmd.Parameters.AddWithValue("@EDate", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@EDuration", textBox3.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Event Updated Successfully");
            Conn.Close();
            DisplayEvent();
            Clear();

        }

        //Auto fill data
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

            EId = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Text = row.Cells[2].Value.ToString();
            textBox3.Text=row.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Delete EventTable where EId=@EId", Conn);
            cmd.Parameters.AddWithValue("@EId", this.EId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Event Deleted");
            Conn.Close();
            DisplayEvent();
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Hide();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
