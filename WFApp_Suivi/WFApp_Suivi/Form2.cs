﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WFApp_Suivi
{
    public partial class Form2 : Form
    {
        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Products ; integrated security = true; ");*/
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Products ; integrated security = false; User ID = Park_Info; Password = Park_Info0823;");


        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Afficher();
        }

        private void Afficher()
        {

            dataGridView1.Rows.Clear();
            string req = "select * from Sites order by Date_Time desc";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2));
            }
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Code de Site ou Désignation est vide !! Entrez une Valeur!");
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("insert into Sites values('" + textBox1.Text + "','" + textBox2.Text + "',getdate())", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                textBox1.Clear(); textBox2.Clear(); textBox1.Focus();
                Afficher();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Afficher();
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from Sites where Code_Site Like '" + textBox1.Text + "'", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2));
                }
                cn.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
