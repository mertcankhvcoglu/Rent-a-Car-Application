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
    public partial class frmAracListele : Form
    {
        Arac_Kiralama arackiralama = new Arac_Kiralama();
        public frmAracListele()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            txtPlaka.Text = satir.Cells["plaka"].Value.ToString();
            comboMarka.Text = satir.Cells["marka"].Value.ToString();
            comboSeri.Text = satir.Cells["seri"].Value.ToString();
            txtYil.Text = satir.Cells["yil"].Value.ToString();
            txtRenk.Text = satir.Cells["renk"].Value.ToString();
            txtKm.Text = satir.Cells["km"].Value.ToString();
            comboYakit.Text = satir.Cells["yakit"].Value.ToString();
            txtUcret.Text = satir.Cells["kiraucreti"].Value.ToString();            
            pictureBox2.ImageLocation = satir.Cells["resim"].Value.ToString();

        }

        private void frmAracListele_Load(object sender, EventArgs e)
        {
            YenileAraclarListesi();

            comboAraclar.SelectedIndex = 0;

        }

        private void YenileAraclarListesi()
        {
            string cumle = "select *from araç";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView1.DataSource = arackiralama.listele(adtr2, cumle);
        }

        private void btnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog1.FileName;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cumle = "update araç set marka = @marka, seri = @seri, yil = @yil, renk = @renk, km = @km, yakit = @yakit, kiraucreti = @kiraucreti, resim = @resim, tarih = @tarih where plaka = @plaka"; //id olarak plaka kullanıyoruz. plaka değiştirilemez.
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut2.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut2.Parameters.AddWithValue("@seri", comboSeri.Text);
            komut2.Parameters.AddWithValue("@yil", txtYil.Text);
            komut2.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut2.Parameters.AddWithValue("@km", txtKm.Text);
            komut2.Parameters.AddWithValue("@yakit", comboYakit.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(txtUcret.Text));
            komut2.Parameters.AddWithValue("@resim", pictureBox2.ImageLocation);
            komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

            arackiralama.ekle_sil_guncelle(komut2, cumle);
            comboSeri.Items.Clear();
            foreach (Control item in Controls) if (item is TextBox) item.Text = ""; //TextBox içlerini temizleme 
            foreach (Control item in Controls) if (item is ComboBox) item.Text = ""; //ComboBox içlerini temizleme
            pictureBox2.ImageLocation = "";
            YenileAraclarListesi();
            MessageBox.Show("Güncelleme İşlemi Başarılı.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            string cumle = "delete from araç where plaka = '" + satir.Cells["plaka"].Value.ToString() + "'";
            SqlCommand komut2 = new SqlCommand();
            arackiralama.ekle_sil_guncelle(komut2, cumle);
            foreach (Control item in Controls) if (item is TextBox) item.Text = ""; //TextBox içlerini temizleme 
            foreach (Control item in Controls) if (item is ComboBox) item.Text = ""; //ComboBox içlerini temizleme
            pictureBox2.ImageLocation = ""; 
            YenileAraclarListesi();
            MessageBox.Show("Silme İşlemi Başarılı.");
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboSeri.Items.Clear(); //önce 2. listeyi temizlesin
                if (comboMarka.SelectedIndex == 0) //0. satırda Honda bulunuyor.
                {
                    comboSeri.Items.Add("Civic");
                    comboSeri.Items.Add("Jazz");
                    comboSeri.Items.Add("HR-V");
                }
                else if (comboMarka.SelectedIndex == 1) //1. Satırda Toyota bulunuyor.
                {
                    comboSeri.Items.Add("Corolla");
                    comboSeri.Items.Add("Yaris");
                    comboSeri.Items.Add("C-HR");
                }
                else if (comboMarka.SelectedIndex == 2) //2. Satırda Volkswagen bulunuyor.
                {
                    comboSeri.Items.Add("Passat");
                    comboSeri.Items.Add("Golf");
                    comboSeri.Items.Add("Tiguan");
                }
                else if (comboMarka.SelectedIndex == 3) //3. Satırda Ford bulunuyor.
                {
                    comboSeri.Items.Add("Focus");
                    comboSeri.Items.Add("Fiesta");
                    comboSeri.Items.Add("Puma");
                }
                else if (comboMarka.SelectedIndex == 4) //4. Satırda Fiat bulunuyor.
                {
                    comboSeri.Items.Add("Egea");
                    comboSeri.Items.Add("500");
                    comboSeri.Items.Add("Doblo");
                }
                else if (comboMarka.SelectedIndex == 5) //5. Satırda BMW bulunuyor.
                {
                    comboSeri.Items.Add("i3");
                    comboSeri.Items.Add("3 Serisi");
                    comboSeri.Items.Add("X3");
                }
                else if (comboMarka.SelectedIndex == 6) //6. Satırda Mercedes bulunuyor.
                {
                    comboSeri.Items.Add("C Serisi");
                    comboSeri.Items.Add("A Serisi");
                    comboSeri.Items.Add("EQ");
                }
                else if (comboMarka.SelectedIndex == 7) //4. Satırda Opel bulunuyor.
                {
                    comboSeri.Items.Add("Astra");
                    comboSeri.Items.Add("Corsa");
                    comboSeri.Items.Add("Mokka");
                }
                else if (comboMarka.SelectedIndex == 8) //4. Satırda Renault bulunuyor.
                {
                    comboSeri.Items.Add("Megane");
                    comboSeri.Items.Add("Clio");
                    comboSeri.Items.Add("Kadjar");
                }

            }
            catch
            {
                ;
            }
        }

        private void comboAraclar_SelectedIndexChanged(object sender, EventArgs e) 
        {
            try
            {
                if (comboAraclar.SelectedIndex == 0) //Tüm araçları Listeler
                {
                    YenileAraclarListesi();
                }
                if (comboAraclar.SelectedIndex == 1) //Boş araçları listeler
                {
                    string cumle = "select *from araç where durumu = 'BOŞ'";
                    SqlDataAdapter adtr2 = new SqlDataAdapter();
                    dataGridView1.DataSource = arackiralama.listele(adtr2, cumle);
                }
                if (comboAraclar.SelectedIndex == 2) //Dolu araçları listeler
                {
                    string cumle = "select *from araç where durumu = 'DOLU'";
                    SqlDataAdapter adtr2 = new SqlDataAdapter();
                    dataGridView1.DataSource = arackiralama.listele(adtr2, cumle);
                }

            }
            catch
            {

                ;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
