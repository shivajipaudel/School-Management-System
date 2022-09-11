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
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
            DisplayStudent(); //Call the display function
        }
        int Id;
        int index;
        //Database connection 
        SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SchoolMgn\SchoolMgn\StudentDb.mdf;Integrated Security=True");
       
      
        //Fetch the data from database
        private void DisplayStudent()
        {
            Conn.Open();
            String cmd = "Select* from StTable";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd,Conn);
            SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Conn.Close();
        }

        //Clear text
        private void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.ResetText();
            textBox3.Clear();
            comboBox2.ResetText();
        }
       //Insert a data 
        private void button1_Click(object sender, EventArgs e)
        {
        if(textBox1.Text=="" || textBox2.Text=="" || comboBox1.Text=="" || textBox3.Text==""|| comboBox2.Text=="")
            {
                MessageBox.Show("Fill all the Detail");
            }
            else
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand("insert into StTable values(@Name,@Age,@Class,@EnNO,@Gender)",Conn);
           
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age", textBox2.Text);
                cmd.Parameters.AddWithValue("@Class", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EnNO", textBox3.Text);
                cmd.Parameters.AddWithValue("@Gender", comboBox2.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully");
               
                Conn.Close();
                DisplayStudent();

            }
        }

       //Update data
        private void button2_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Update StTable set Name=@Name,Age=@Age,Class=@Class,Gender=@Gender where Id=@Id", Conn);
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@Age", textBox2.Text);
            cmd.Parameters.AddWithValue("@Class", comboBox1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EnNO", textBox3.Text);
            cmd.Parameters.AddWithValue("@Gender", comboBox2.SelectedItem.ToString());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated");
            Conn.Close();
            DisplayStudent();
            Clear();
        }

       //Remove data
        private void button3_Click(object sender, EventArgs e)
        {
            Conn.Open();
            SqlCommand cmd = new SqlCommand("Delete StTable where Id=@Id", Conn);
       
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            Conn.Close();
            DisplayStudent();
            Clear();
        }

       //Back to MainMenu
        private void button4_Click(object sender, EventArgs e)
        {
            
           MainMenu obj = new MainMenu();
           obj.Show();
            this.Hide();
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Onclick any row of dataGridView this Auto fill the text in the Form 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

            Id = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            comboBox1.Text = row.Cells[3].Value.ToString();
            textBox3.Text = row.Cells[4].Value.ToString();
            comboBox2.Text = row.Cells[5].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
    }
}
