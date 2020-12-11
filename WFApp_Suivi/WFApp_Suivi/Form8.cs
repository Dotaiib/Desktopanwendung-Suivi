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
    public partial class Form8 : Form
    {
        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Products ; integrated security = true; ");*/
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Products ; integrated security = false; User ID = Park_Info; Password = Park_Info0823;");



        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

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
            else { textBox1.Focus(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("insert into Produits_Sortie select ID, Site_Produit, Departement_Produit, Materiel_Produit, Destinatin_Produit, Code_Bar_Produit, Date_Entree_Produit, Date_Sortie_Produit='"+dateTimePicker1.Text+"', Details, getdate() from Produits where Code_Bar_Produit = '"+textBox2.Text+"'", cn);
            DialogResult result = MessageBox.Show("Voulez-vous supprimer le produit " + textBox2.Text + "?", "Confirmer la suppression du produit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Supprimé");
                dataGridView1.Rows.Clear();
                textBox1.Clear(); textBox2.Clear();
                textBox1.Focus();
                button2.Enabled = false;
            }
            cn.Close();
            
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text!= "")
            { button2.Enabled = true; }
        }
    }
}
