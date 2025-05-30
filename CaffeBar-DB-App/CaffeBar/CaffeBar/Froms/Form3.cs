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
    public partial class Form3 : Form
    {
        private PuntoretDAL dal;
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=CaffeBar DB;Trusted_Connection=True;";
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
        public Form3()
        {
            InitializeComponent();
            dal = new PuntoretDAL(connectionString);
            this.Load += Form3_Load;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var table in allowedTables.Keys)
            {
                comboBox1.Items.Add(table);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            string selectedTable = comboBox1.SelectedItem.ToString();

            if (!allowedTables.ContainsKey(selectedTable))
            {
                MessageBox.Show("Invalid table selected.");
                return;
            }

            try
            {
                DataTable data = dal.GetAllFromTable(selectedTable);

                
                var columnNames = data.Columns.Cast<DataColumn>().Select(col => col.ColumnName);
                listBox1.Items.Add(string.Join(" | ", columnNames));

                
                foreach (DataRow row in data.Rows)
                {
                    var values = row.ItemArray.Select(val => val?.ToString() ?? "");
                    listBox1.Items.Add(string.Join(" | ", values));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

    }
}
