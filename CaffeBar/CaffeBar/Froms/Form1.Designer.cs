using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;




namespace CaffeBar
{
    public partial class Form1 : Form
    {
      
        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("✅ Connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Connection failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}

