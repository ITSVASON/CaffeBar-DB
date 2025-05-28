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
    public partial class Form2 : Form
    {
        private PuntoretDAL dal;
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=Caffe Bar DB;Trusted_Connection=True;";
        public Form2()
        {
            InitializeComponent();
            dal = new PuntoretDAL(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Puntoret puntoret = new Puntoret();
            puntoret.Id = textBox1.Text;
            puntoret.Emri = textBox2.Text;
            puntoret.ContactInfo = textBox3.Text;
            puntoret.DataPunsimit = textBox4.Text;

            dal.Insert(puntoret);
            MessageBox.Show("Added successfully");


        }

    }
}
