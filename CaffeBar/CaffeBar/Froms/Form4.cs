using CaffeBar.DAL;
using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaffeBar.Froms
{
    public partial class Form4 : Form
    {
        private PuntoretDAL dal;
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=Caffe Bar DB;Trusted_Connection=True;";

        public Form4()
        {
            InitializeComponent();
            this.Load += Form4_Load;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            dal = new PuntoretDAL(connectionString);
            List<Puntoret> allPuntoret = dal.GetAll();

            comboBox1.DataSource = allPuntoret;
            comboBox1.DisplayMember = "Emri";
            comboBox1.ValueMember = "Id";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Puntoret selected)
            {
                
                textBox1.Text = selected.Id;
                textBox2.Text = selected.Emri;
                textBox3.Text = selected.ContactInfo;
                textBox4.Text = selected.DataPunsimit;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Puntoret puntoret = new Puntoret();
            puntoret.Id = textBox1.Text;
            puntoret.Emri = textBox2.Text;
            puntoret.ContactInfo = textBox3.Text;
            puntoret.DataPunsimit = textBox4.Text;

            dal.Update(puntoret);
            MessageBox.Show("Updated successfully");
        }
    }
}
