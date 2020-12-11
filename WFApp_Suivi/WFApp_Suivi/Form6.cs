using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace WFApp_Suivi
{
    public partial class Form6 : Form
    {
        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Products ; integrated security = true; ");*/
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Products ; integrated security = false; User ID = Park_Info; Password = Park_Info0823;");


        PrintDocument document = new PrintDocument();
        PrintDialog dialog = new PrintDialog();

        public Form6()
        {
            InitializeComponent();
            document.PrintPage += new PrintPageEventHandler(document_PrintPage);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            Afficher();
        }

        void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image newImage = Image.FromFile("\\\\192.168.1.156\\CIBEL_Logo\\CIBEL_Logo.jpg");
            string txt1 = "Please do not remove this";

            /*e.Graphics.DrawImage(newImage, ix, iy, width, height);*/

            e.Graphics.DrawImage(newImage, 25, 15, 100, 100);

            e.Graphics.DrawString(textBox1.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, 190, 15);

            e.Graphics.DrawImage(pictureBox1.Image, 140, 37);

            e.Graphics.DrawString(txt1, new Font("Arial", 08, FontStyle.Regular), Brushes.Black, 250, 90);

        }

        private void Afficher()
        {

            dataGridView1.Rows.Clear();
            string req = "select Site_Produit, Departement_Produit, Materiel_Produit, FullName, Code_Bar_Produit, Date_Entree_Produit from Produits, Receveur where Produits.Destinatin_Produit = Receveur.Code_Receveur order by Produits.Date_Time desc";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
            }
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = barcode.Draw(textBox1.Text, 50);
            button3.Enabled = true;
                                             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dialog.Document = document;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                document.Print();
            }
            button3.Enabled = false; textBox1.Clear(); pictureBox1.Image = null;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text !="")
            { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }
    }
    }
   
