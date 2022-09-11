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
    public partial class Teachers : Form
    {
        public Teachers()
        {
            InitializeComponent();
            DisplayTeacher();
            Clear();
        }
        int Id;
        int index;
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
        private void DisplayTeacher()
        {

            Conn.Open();
            String cmd = "Select* from TeacherTable";
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
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox2.ResetText();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox2.Text == "") 
            {
                MessageBox.Show("Fill all the detail");
            }
            else
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand("insert into TeacherTable values(@Name,@Age,@Subject,@Phone,@Gender,@Address)", Conn);
           
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age",int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@Subject", textBox5.Text);
                cmd.Parameters.AddWithValue("@Phone",textBox3.Text);
                cmd.Parameters.AddWithValue("@Gender", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Teacher Added Successfully");
                Conn.Close();
                DisplayTeacher();
                Clear();


            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Update TeacherTable set Age=@Age,Subject=@Subject,Phone=@Phone,Gender=@Gender,Address=@Address where Id=@Id", Conn);
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@Age", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@Subject", textBox5.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
            cmd.Parameters.AddWithValue("@Gender", comboBox2.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Address", textBox4.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated Successfully!..");
            Conn.Close();
            DisplayTeacher();
            Clear();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Delete TeacherTable where Id=@Id", Conn);
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            Conn.Close();
            DisplayTeacher();
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

            Id = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            textBox5.Text = row.Cells[3].Value.ToString();
            textBox3.Text = row.Cells[4].Value.ToString();
            comboBox2.Text=row.Cells[5].Value.ToString();
            textBox4.Text = row.Cells[6].Value.ToString();
        }
    }
}
