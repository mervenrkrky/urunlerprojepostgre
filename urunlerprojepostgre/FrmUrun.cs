using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace urunlerprojepostgre
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "kategoriad";//combobox ın on planında çalışacak olan on planda kategor adi sececez arka planda by bize k.id secmis olacak
            comboBox1.ValueMember = "kategoriid";//combobox ın arka olanında çalısacak olan
            comboBox1.DataSource = dt;
            baglantı.Close();



        }
        NpgsqlConnection baglantı = new NpgsqlConnection("server=localHost; port=5432;Database=dburunler;user ID =postgres ; password=12345");


        private void BtnListele_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btnekle_Click(object sender, EventArgs e)
        {
            // TxtAd.Text = comboBox1.SelectedValue.ToString();

            baglantı.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urunler(urunid,urunad,stok,alısfiyat,satısfiyat,gorsel,kategori) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglantı);
            komut.Parameters.AddWithValue("@p1",int.Parse(Txtid.Text));
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3",int.Parse( numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p4",double.Parse (TxtAlısfiyat.Text));
            komut.Parameters.AddWithValue("@p5",double.Parse( TxtSatısfiyat.Text));
            komut.Parameters.AddWithValue("@p6", TxtGörsel.Text);
            komut.Parameters.AddWithValue("@p7",int.Parse (comboBox1.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Ürün kaydi başarılı bir şekilde gercekleşti","bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from urunler where urunid=@p1", baglantı);
            komut2.Parameters.AddWithValue("@p1", int.Parse(Txtid.Text));
            komut2.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("ürün silme işlemi başarılı bir şekilde gerçekleşti","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update urunler set urunad=@p1,stok=@p2,alısfiyat=@p3 where urunid=@p4",baglantı);
            komut3.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut3.Parameters.AddWithValue("@p2",int.Parse(numericUpDown1.Value.ToString()));
            komut3.Parameters.AddWithValue("@p3",double.Parse(TxtAlısfiyat.Text));
            komut3.Parameters.AddWithValue("@p4",int.Parse(Txtid.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Ürün gücelleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglantı.Close();
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("Select*from urunlerlistesi", baglantı);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglantı.Close();


        }
    }
}