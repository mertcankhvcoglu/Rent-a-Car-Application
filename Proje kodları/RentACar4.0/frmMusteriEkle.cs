using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // sonradan

namespace RentACar4._0
{
    public partial class frmMusteriEkle : Form
    {
        Arac_Kiralama arac_kiralama = new Arac_Kiralama();
        public frmMusteriEkle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cumle = "insert into Musteri(tc,adsoyad,telefon,mail,adres) values(@tc,@adsoyad,@telefon,@mail,@adres)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTcNo.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelNo.Text);
            komut2.Parameters.AddWithValue("@mail", txtMail.Text);
            komut2.Parameters.AddWithValue("@adres", txtAdres.Text);
            arac_kiralama.ekle_sil_guncelle(komut2, cumle);
            foreach (Control item in Controls) if (item is TextBox) item.Text = "";
            MessageBox.Show("Kayıt İşlemi Başarılı.");
        }

    }
}
