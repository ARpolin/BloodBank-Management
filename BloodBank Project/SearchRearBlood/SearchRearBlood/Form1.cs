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

namespace SearchRearBlood
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter sda;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            Refresh();
            
        }
        private void blood_save_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=.;Initial Catalog=bloodbank;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("INSERT INTO blood(bloodgroup,date,bid)VALUES(@bloodgroup,@date,@bid)",con);
            cmd.Parameters.AddWithValue("@bloodgroup", blood_cmbo.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@date",dateTimePicker1.Value.ToString("mm/dd/yyyy"));
            cmd.Parameters.AddWithValue("@bid",txt_id.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Save Data");
            Refresh();
            LoadData();
        }
       public void LoadData()
        {
            con = new SqlConnection(@"Data Source=.;Initial Catalog=bloodbank;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("SELECT * from blood",con);
            try
            {
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbdataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbdataset);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Refresh() 
        {
            txt_id.Text = "";
            blood_cmbo.Text = "";
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=.;Initial Catalog=bloodbank;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM blood WHERE bloodgroup LIKE('"+txt_search.Text+"')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Starting st = new Starting();
            st.Show();
            this.Hide();
        }
        
    }
}
