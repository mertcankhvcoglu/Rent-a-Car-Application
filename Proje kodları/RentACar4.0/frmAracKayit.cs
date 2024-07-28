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
    public partial class frmAracKayit : Form
    {
        Arac_Kiralama arackiralama = new Arac_Kiralama(); //class'ı cagir
        public frmAracKayit()
        {
            InitializeComponent();
        }

        private void btnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e) //arackayit sayfasi iptal butonu
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e) //arackayit sayfasi kayit butonu
        {
            string cumle = "insert into araç(plaka, marka, seri, yil, renk, km, yakit, kiraucreti, resim, tarih, durumu) values(@plaka, @marka, @seri, @yil, @renk, @km, @yakit, @kiraucreti, @resim, @tarih, @durumu)";

            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@plaka",txtPlaka.Text);
            komut2.Parameters.AddWithValue("@marka",comboMarka.Text);
            komut2.Parameters.AddWithValue("@seri",comboSeri.Text);
            komut2.Parameters.AddWithValue("@yil",txtYil.Text);
            komut2.Parameters.AddWithValue("@renk",txtRenk.Text);
            komut2.Parameters.AddWithValue("@km",txtKm.Text);
            komut2.Parameters.AddWithValue("@yakit",comboYakit.Text);
            komut2.Parameters.AddWithValue("@kiraucreti",int.Parse(txtUcret.Text));
            komut2.Parameters.AddWithValue("@resim",pictureBox1.ImageLocation);
            komut2.Parameters.AddWithValue("@tarih",DateTime.Now.ToString()); // o günkü tarihi alan kod
            komut2.Parameters.AddWithValue("@durumu","BOŞ");

            arackiralama.ekle_sil_guncelle(komut2, cumle);
            comboSeri.Items.Clear();
            foreach (Control item in Controls) if (item is TextBox) item.Text = ""; //TextBox içlerini temizleme 
            foreach (Control item in Controls) if (item is ComboBox) item.Text = ""; //ComboBox içlerini temizleme
            pictureBox1.ImageLocation = ""; // fotograf kutusunun içini temizleme
            MessageBox.Show("Araç Kayıt İşlemi Başarılı.");

        }
    }
}
