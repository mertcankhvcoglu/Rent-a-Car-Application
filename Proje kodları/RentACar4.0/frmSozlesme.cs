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
    public partial class frmSozlesme : Form
    {
        public frmSozlesme()
        {
            InitializeComponent();
        }

        Arac_Kiralama arac = new Arac_Kiralama();
        private void frmSozlesme_Load(object sender, EventArgs e)
        {
            Bos_Araclar();
            Yenile();
        }

        private void Bos_Araclar()
        {
            string sorgu2 = "select *from araç where durumu = 'BOŞ'";
            arac.Bos_Araclar(comboAraclar, sorgu2);
        }

        private void Yenile()
        {
            string sorgu3 = "select *from sözleşme";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView1.DataSource = arac.listele(adtr2, sorgu3);
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboAraclar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu2 = "select *from araç where plaka like '" + comboAraclar.SelectedItem + "'";
            arac.CombodanGetir(comboAraclar, txtMarka, txtSeri, txtYil, txtRenk, sorgu2);
        }

        private void comboKiraSekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu2 = "select *from araç where plaka like '" + comboAraclar.SelectedItem + "'";
            arac.Ucret_Hesapla(comboKiraSekli, txtKiraUcreti, sorgu2);

        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            TimeSpan gun = DateTime.Parse(dateDonus.Text) - DateTime.Parse(dateCikis.Text); //gün farkını hesaplıyoruz
            int gun2 = gun.Days;
            txtGun.Text = gun2.ToString();
            txtTutar.Text = (gun2 * int.Parse(txtKiraUcreti.Text)).ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Temizle()
        {
            dateCikis.Text = DateTime.Now.ToShortDateString();
            dateDonus.Text = DateTime.Now.ToShortDateString();
            comboKiraSekli.Text = "";
            txtKiraUcreti.Text = "";
            txtGun.Text = "";
            txtTutar.Text = "";
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu2 = "insert into sözleşme(tc, adsoyad, telefon, ehliyetno, e_tarih, e_yer, plaka, marka, seri, yil, renk, kirasekli, kiraucreti, gun, tutar, cikistarih, donustarih) values(@tc, @adsoyad, @telefon, @ehliyetno, @e_tarih, @e_yer, @plaka, @marka, @seri, @yil, @renk, @kirasekli, @kiraucreti, @gun, @tutar, @cikistarih, @donustarih)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTc.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelNo.Text);
            komut2.Parameters.AddWithValue("@ehliyetno", txtE_No.Text);
            komut2.Parameters.AddWithValue("@e_tarih", txtE_Yer.Text);
            komut2.Parameters.AddWithValue("@e_yer", txtE_Yer.Text);
            komut2.Parameters.AddWithValue("@plaka", comboAraclar.Text);
            komut2.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut2.Parameters.AddWithValue("@seri", txtSeri.Text);
            komut2.Parameters.AddWithValue("@yil", txtYil.Text);
            komut2.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut2.Parameters.AddWithValue("@kirasekli", comboKiraSekli.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(txtKiraUcreti.Text));
            komut2.Parameters.AddWithValue("@gun", int.Parse(txtGun.Text));
            komut2.Parameters.AddWithValue("@tutar", int.Parse(txtTutar.Text));
            komut2.Parameters.AddWithValue("@cikistarih", dateCikis.Text);
            komut2.Parameters.AddWithValue("@donustarih", dateDonus.Text);

            arac.ekle_sil_guncelle(komut2, sorgu2);

            string sorgu3 = "update araç set durumu = 'DOLU' where plaka = '" + comboAraclar.Text + "'";
            SqlCommand komut3 = new SqlCommand(sorgu3);

            arac.ekle_sil_guncelle(komut3, sorgu3);
            comboAraclar.Items.Clear();
            Bos_Araclar();
            Yenile();

            foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
            foreach (Control item in groupBox2.Controls) if (item is TextBox) item.Text = "";
            comboAraclar.Text = "";
            Temizle();

            MessageBox.Show("Sözleşme başarıyla eklendi.");
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            if (txtTcAra.Text == "") foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
            string sorgu2 = "select *from Musteri where tc like '" + txtTcAra.Text + "'";
            arac.TC_Ara(txtTcAra, txtTc, txtAdSoyad, txtTelNo, sorgu2);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu2 = "update sözleşme set tc=@tc, adsoyad=@adsoyad, telefon=@telefon, ehliyetno=@ehliyetno, e_tarih=@e_tarih, e_yer=@e_yer, marka=@marka, seri=@seri, yil=@yil, renk=@renk, kirasekli=@kirasekli, kiraucreti=@kiraucreti, gun=@gun, tutar=@tutar, cikistarih=@cikistarih, donustarih=@donustarih where plaka = @plaka";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTc.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelNo.Text);
            komut2.Parameters.AddWithValue("@ehliyetno", txtE_No.Text);
            komut2.Parameters.AddWithValue("@e_tarih", txtE_Tarih.Text);
            komut2.Parameters.AddWithValue("@e_yer", txtE_Yer.Text);
            komut2.Parameters.AddWithValue("@plaka", comboAraclar.Text);
            komut2.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut2.Parameters.AddWithValue("@seri", txtSeri.Text);
            komut2.Parameters.AddWithValue("@yil", txtYil.Text);
            komut2.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut2.Parameters.AddWithValue("@kirasekli", comboKiraSekli.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(txtKiraUcreti.Text));
            komut2.Parameters.AddWithValue("@gun", int.Parse(txtGun.Text));
            komut2.Parameters.AddWithValue("@tutar", int.Parse(txtTutar.Text));
            komut2.Parameters.AddWithValue("@cikistarih", dateCikis.Text);
            komut2.Parameters.AddWithValue("@donustarih", dateDonus.Text);

            arac.ekle_sil_guncelle(komut2, sorgu2);
            
            comboAraclar.Items.Clear();
            Bos_Araclar();
            Yenile();

            foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
            foreach (Control item in groupBox2.Controls) if (item is TextBox) item.Text = "";
            comboAraclar.Text = "";
            Temizle();

            MessageBox.Show("Sözleşme başarıyla güncellendi.");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            txtTc.Text = satir.Cells[0].Value.ToString();
            txtAdSoyad.Text = satir.Cells[1].Value.ToString();
            txtTelNo.Text = satir.Cells[2].Value.ToString();
            txtE_No.Text = satir.Cells[3].Value.ToString();
            txtE_Tarih.Text = satir.Cells[4].Value.ToString();
            txtE_Yer.Text = satir.Cells[5].Value.ToString();
            comboAraclar.Text = satir.Cells[6].Value.ToString();
            txtMarka.Text = satir.Cells[7].Value.ToString();
            txtSeri.Text = satir.Cells[8].Value.ToString();
            txtYil.Text = satir.Cells[9].Value.ToString();
            txtRenk.Text = satir.Cells[10].Value.ToString();
            comboKiraSekli.Text = satir.Cells[11].Value.ToString();
            txtKiraUcreti.Text = satir.Cells[12].Value.ToString();
            txtGun.Text = satir.Cells[13].Value.ToString();
            txtTutar.Text = satir.Cells[14].Value.ToString();
            dateCikis.Text = satir.Cells[15].Value.ToString();
            dateDonus.Text = satir.Cells[16].Value.ToString();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            //gün farkı hesabı
            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString()); 
            DateTime donus = DateTime.Parse(satir.Cells["donustarih"].Value.ToString());
            int ucret = int.Parse(satir.Cells["kiraucreti"].Value.ToString());
            TimeSpan gunfarki = bugun - donus;
            int gunfarki2 = gunfarki.Days;
            int ucretfarki;

            ucretfarki = gunfarki2 * ucret;
            txtEkstra.Text = ucretfarki.ToString();
        }

        private void btnAracTeslim_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtEkstra.Text) >= 0 || int.Parse(txtEkstra.Text) < 0)
            {
                DataGridViewRow satir = dataGridView1.CurrentRow;
                DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());
                int ucret = int.Parse(satir.Cells["kiraucreti"].Value.ToString());
                int tutar = int.Parse(satir.Cells["tutar"].Value.ToString());

                DateTime cikis = DateTime.Parse(satir.Cells["cikistarih"].Value.ToString());
                TimeSpan gun = bugun - cikis;

                int gun2 = gun.Days;
                int toplamtutar = gun2 * ucret;

                string sorgu1 = "delete from sözleşme where plaka = '"+ satir.Cells["plaka"].Value.ToString() +"'";
                SqlCommand komut = new SqlCommand();
                arac.ekle_sil_guncelle(komut, sorgu1);

                string sorgu2 = "update araç set durumu = 'BOŞ' where plaka = '"+ satir.Cells["plaka"].Value.ToString() +"'";
                SqlCommand komut3 = new SqlCommand();
                arac.ekle_sil_guncelle(komut3, sorgu2);

                string sorgu3 = "insert into satış(tc, adsoyad, plaka, marka, seri, yil, renk, gun, fiyat, tutar, tarih1, tarih2) values(@tc, @adsoyad, @plaka, @marka, @seri, @yil, @renk, @gun, @fiyat, @tutar, @tarih1, @tarih2)";
                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.AddWithValue("@tc", satir.Cells["tc"].Value.ToString());
                komut2.Parameters.AddWithValue("@adsoyad", satir.Cells["adsoyad"].Value.ToString());
                
                komut2.Parameters.AddWithValue("@plaka", satir.Cells["plaka"].Value.ToString());
                komut2.Parameters.AddWithValue("@marka", satir.Cells["marka"].Value.ToString());
                komut2.Parameters.AddWithValue("@seri", satir.Cells["seri"].Value.ToString());
                komut2.Parameters.AddWithValue("@yil", satir.Cells["yil"].Value.ToString());
                komut2.Parameters.AddWithValue("@renk", satir.Cells["renk"].Value.ToString());                
                komut2.Parameters.AddWithValue("@gun", gun2);
                komut2.Parameters.AddWithValue("@fiyat", ucret);
                komut2.Parameters.AddWithValue("@tutar", toplamtutar);
                komut2.Parameters.AddWithValue("@tarih1", satir.Cells["cikistarih"].Value.ToString());
                komut2.Parameters.AddWithValue("@tarih2", DateTime.Now.ToShortDateString());

                arac.ekle_sil_guncelle(komut2, sorgu3);

                MessageBox.Show("Araç teslim edildi.");

                comboAraclar.Text = "";
                comboAraclar.Items.Clear();
                Bos_Araclar();
                Yenile();

                foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
                foreach (Control item in groupBox2.Controls) if (item is TextBox) item.Text = "";
                comboAraclar.Text = "";
                Temizle();

                txtEkstra.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen seçim yapınız.", "Uyarı");
            }
        }
    }
}
