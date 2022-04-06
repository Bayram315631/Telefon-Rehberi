using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Telefon_Rehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=data.accdb ");
            da = new OleDbDataAdapter("SElect *from Kisiler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Kisiler");
            dataGridView1.DataSource = ds.Tables["Kisiler"];
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
           
           cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into ogrenci (ad,soyad,tel) values ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtTelefon.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Kisiler Set ad=@Ad,soyad=@Soyad,Telefon=@tel Where Id=@id";
            cmd = new OleDbCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@tel", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE Kisiler Where Id=@id";
            cmd = new OleDbCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            //DataView dv = .DefaultView;
           // dv.RowFilter = "Ad Like '" + txtAra.Text + "%'";
            //dataGridView1.DataSource = dv;
        }

    }
}
    