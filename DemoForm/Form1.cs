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

namespace DemoForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string con_db = "Data Source=I-VIJAYD\\ENTERPRISE2014;Initial Catalog=SampleDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(con_db);

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string con_db = "Data Source=I-VIJAYD\\ENTERPRISE2014;Initial Catalog=SampleDB;Integrated Security=True";
            string ID = txtID.Text;
            string Name = txtName.Text;

           
            string query = "insert into SampleDATA values ( " + ID + ", \'" + Name + "\' )";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                 int res =  cmd.ExecuteNonQuery();
                MessageBox.Show("Record added successfully!! " + res.ToString() );
            }
            catch (SqlException abc)
            {
                MessageBox.Show(abc.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            string ID = txtSerachID.Text;
            string query = "select ID,Name from SampleDATA where ID = " + ID.ToString();
            string[] newData = new string[2];
           
            

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        { if (reader.FieldCount != -1) {

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    newData[i] = reader.GetValue(i).ToString();

                                }
                            }

                            else
                            {
                                MessageBox.Show("No record found!!");

                            }
                        }
                    }
                }
                 
            }
            catch (SqlException abc)
            {
                MessageBox.Show(abc.ToString());
            }
            finally
            {
                con.Close();
            }

            lb_ID.Show();
            lb_name.Show();

            lb_ID.Text = "ID : " + newData[0];
            lb_name.Text = "Name : " + newData[1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lb_ID.Hide();
            lb_name.Hide();
            //MessageBox.Show(sender.ToString());
        }
    }
}
