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
    public partial class Form7 : Form
    {

        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Products ; integrated security = true; ");*/
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Products ; integrated security = false; User ID = Park_Info; Password = Park_Info0823;");


        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "Entrez les modifications";
            richTextBox1.ForeColor = Color.Gray;
        }

        private void Afficher()
        {

            dataGridView1.Rows.Clear();
            string req = "select * from Produits order by Date_Time desc";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5), dr.GetValue(6), dr.GetValue(8));
            }
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from Produits where Site_Produit like '" + textBox1.Text + "' or Departement_Produit like '" + textBox1.Text + "' or Materiel_Produit like '" + textBox1.Text + "' or Destinatin_Produit like '" + textBox1.Text + "' or Code_Bar_Produit like '" + textBox1.Text + "'", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5), dr.GetValue(6), dr.GetValue(8));
                }
                cn.Close();
            }
            else { Afficher(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (richTextBox1.Text == "Entrez les modifications")
            {
                MessageBox.Show("Vous devez remplir la zone de texte de Détails");
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("update Produits set Details = Details + '"+ ' ' +'#' +' ' + richTextBox1.Text + ' '+'#'+' '+ "', Date_Time= GETDATE() where Code_Bar_Produit = '" +  textBox2.Text+"'", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                textBox2.Text = ""; richTextBox1.Clear(); Afficher();
            }

            
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text = "Entrez les modifications";
                richTextBox1.ForeColor = Color.Gray;
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Entrez les modifications")
            {
                richTextBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

    }
}
