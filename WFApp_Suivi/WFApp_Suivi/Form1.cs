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

namespace WFApp_Suivi
{
    public partial class Form1 : Form
    {

        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Products ; integrated security = true; ");*/
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Products ; integrated security = false; User ID = Park_Info; Password = Park_Info0823;");

        public static Form1 form;
        public Form1()
        {
            form = this;
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            CMB1(); CMB2(); CMB3(); CMB4();
            Afficher();

            comboBox1.Focus();
            richTextBox1.Text = "Veuillez entrer tous les détails sur le produit que vous enregistrez";
            richTextBox1.ForeColor = Color.Gray;
        }

        public void CMB1()
        {
            cn.Open();
            string Sql = "select Code_Site from sites";
            SqlCommand cmd = new SqlCommand(Sql, cn);
            SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            { comboBox1.Items.Add(DR[0]); }
            cn.Close();
            textBox5.Clear();
        }

        private void CMB2()
        {
            cn.Open();
            string Sql = "select Code_Departement from Departements";
            SqlCommand cmd = new SqlCommand(Sql, cn);
            SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            { comboBox2.Items.Add(DR[0]); }
            cn.Close();
            textBox5.Clear();
        }

        private void CMB3()
        {
            cn.Open();
            string Sql = "select Code_Materiel from Materiel";
            SqlCommand cmd = new SqlCommand(Sql, cn);
            SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            { comboBox3.Items.Add(DR[0]); }
            cn.Close();
            textBox5.Clear();
        }

        private void CMB4()
        {
            cn.Open();
            string Sql = "select Code_Receveur from Receveur";
            SqlCommand cmd = new SqlCommand(Sql, cn);
            SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            { comboBox4.Items.Add(DR[0]); }
            cn.Close();
            textBox5.Clear();
        }

        private void Afficher()
        {

            dataGridView1.Rows.Clear();
            string req = "select top 5 Site_Produit, Departement_Produit, Materiel_Produit, FullName, Code_Bar_Produit, Date_Entree_Produit from Produits, Receveur where Produits.Destinatin_Produit = Receveur.Code_Receveur order by Produits.Date_Time desc";

            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
            }
            cn.Close();
        }


        private void CodeBar()
        {
            if (textBox1.Text == "")
            {
                if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "")
                {
                    textBox5.Text = "** " + comboBox1.Text + " D" + comboBox2.Text + " P" + comboBox3.Text + " R" + comboBox4.Text + " **";
                    button1.Enabled = true;
                }
            }
            else
            {
                if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "")
                {
                    textBox5.Text = "** " + comboBox1.Text + " D" + comboBox2.Text + " P" + comboBox3.Text + " R" + comboBox4.Text + " @" + textBox1.Text+" **";
                    button1.Enabled = true;
                }
            }

            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Veuillez entrer tous les détails sur le produit que vous enregistrez")
            {
                MessageBox.Show("Vous devez remplir la zone de texte de Détails");
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("insert into Produits values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Text + "','','" + richTextBox1.Text + "',getdate())", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                comboBox1.SelectedIndex = -1; comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1; comboBox4.SelectedIndex = -1;
                dateTimePicker1.ResetText(); textBox1.Clear();  textBox5.Clear(); richTextBox1.Text = "Veuillez entrer tous les détails sur le produit que vous enregistrez";
                Afficher();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           textBox5.Text = textBox5.Text.ToUpper(); 
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            comboBox1.Items.Clear();
            CMB1();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            comboBox2.Items.Clear();
            CMB2();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
            comboBox3.Items.Clear();
            CMB3();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
            comboBox4.Items.Clear();
            CMB4();
        }

        private void menu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            this.Hide();
            f6.Show();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Veuillez entrer tous les détails sur le produit que vous enregistrez")
            {
                richTextBox1.Text = "";
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text = "Veuillez entrer tous les détails sur le produit que vous enregistrez";
                richTextBox1.ForeColor = Color.Gray;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeBar();
        }

        private void menu2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            this.Hide();
            f7.Show();
        }

        private void menu3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            this.Hide();
            f8.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeBar();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeBar();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeBar();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CodeBar();
        }
    }
}
