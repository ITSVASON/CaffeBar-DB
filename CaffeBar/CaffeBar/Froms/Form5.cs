using CaffeBar.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CaffeBar.Froms
{
    public partial class Form5 : Form
    {
        private PuntoretDAL dal;
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=Caffe Bar DB;Trusted_Connection=True;";
        private Dictionary<string, string[]> allowedTables = new Dictionary<string, string[]>
        {
            { "puntoret", new[] { "id" } },
            { "p_oraret", new[] { "id" } },
            { "produkte", new[] { "id" } },
            { "shitjet", new[] { "id" } },
            { "shitje_artikuj", new[] { "id" } },
            { "ardhurat_ditore", new[] { "id" } },
            { "hargjimet", new[] { "id" } }
        };
        public Form5()
        {
            InitializeComponent();
            dal = new PuntoretDAL(connectionString);
            this.Load += Form5_Load;

        }
        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (var table in allowedTables.Keys)
            {
                comboBox2.Items.Add(table);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a table.");
                return;
            }

            string selectedTable = comboBox2.SelectedItem.ToString();
            string idToDelete = textBox1.Text;

            try
            {
                dal.DeleteById(selectedTable, "id", idToDelete);
                MessageBox.Show("Deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


   
    }
}
