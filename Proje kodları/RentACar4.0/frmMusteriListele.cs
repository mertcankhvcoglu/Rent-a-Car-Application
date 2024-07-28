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

namespace RentACar4._0
{
    public partial class frmMusteriListele : Form
    {
        Arac_Kiralama arackiralama = new Arac_Kiralama();
        public frmMusteriListele()
        {
            InitializeComponent();
        }

        private void frmMusteriListele_Load(object sender, EventArgs e)
        {
            YenileListele();
        }

        private void YenileListele()
        {
            string cumle = "select *from Musteri";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            //COLUMNLARDA e posta ve adresin sırası ters olacak. yani e posta 4, adres 3
            dataGridView1.DataSource = arackiralama.listele(adtr2, cumle);
            dataGridView1.Columns[0].HeaderText = "TC";
            dataGridView1.Columns[1].HeaderText = "AD SOYAD";
            dataGridView1.Columns[2].HeaderText = "TELEFON";
            dataGridView1.Columns[4].HeaderText = "E-POSTA";
            dataGridView1.Columns[3].HeaderText = "ADRES";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string cumle = "select *from Musteri where tc like '%"+textBox1.Text+"%' ";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            
            dataGridView1.DataSource = arackiralama.listele(adtr2, cumle);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            txtTcNo.Text = satir.Cells[0].Value.ToString(); //cells 0 yani 0. sütun
            txtAdSoyad.Text = satir.Cells[1].Value.ToString();
            txtTelNo.Text = satir.Cells[2].Value.ToString();
            txtMail.Text = satir.Cells[4].Value.ToString();
            txtAdres.Text = satir.Cells[3].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cumle = "update Musteri set adsoyad = @adsoyad, telefon = @telefon, mail = @mail, adres = @adres where tc = @tc";
            SqlCommand komut2 = new SqlCommand();

            komut2.Parameters.AddWithValue("@tc", txtTcNo.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelNo.Text);
            komut2.Parameters.AddWithValue("@mail", txtMail.Text);
            komut2.Parameters.AddWithValue("@adres", txtAdres.Text);
            arackiralama.ekle_sil_guncelle(komut2, cumle);
            foreach (Control item in Controls) if (item is TextBox) item.Text = "";     
            YenileListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;

            string cumle = "delete from Musteri where tc = '" + satir.Cells["tc"].Value.ToString() +"' ";
            SqlCommand komut2 = new SqlCommand();
            arackiralama.ekle_sil_guncelle(komut2, cumle);
            //foreach (Control item in Controls) if (item is TextBox) item.Text = "";
            YenileListele();
        }
    }
}
