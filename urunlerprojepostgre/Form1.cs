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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        NpgsqlConnection baglantı = new NpgsqlConnection("server=localHost; port=5432;Database=dburunler;user ID =postgres ; password=12345");

        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void Btnekle_Click(object sender, EventArgs e)
        {
            baglantı.Open();//bagklantıyı açıyoruz
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriid,kategoriad) values (@p1,@p2)", baglantı);
            komut1.Parameters.AddWithValue("@p1",int.Parse(TxtKategoriid.Text));
            komut1.Parameters.AddWithValue("@p2", TxtKategoriad.Text);
            komut1.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kategori Ekleme İşlemi Başarılı Bir Şekilde Gerçekleşti. ");

        }

        private void TxtKategoriid_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from kategoriler where kategoriid=@p1", baglantı);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriid.Text));
            komut2.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kategori silme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2", baglantı);
            komut3.Parameters.AddWithValue("@p1", TxtKategoriad.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(TxtKategoriid.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Kategori gücelleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglantı.Close();
        }
    }
}
