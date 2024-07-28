using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;  //table kullanmak icin eklendi
using System.Data.SqlClient; //sql kullanmak icin eklendi
using System.Windows.Forms; // sonradan tanımlandı

//sınıf kullanmak işlemleri daha hızlı ve kolay yapar. Karışıklığı önler.
// "Data'dan önce @ koyarak hata düzeltildi.

namespace RentACar4._0
{
    internal class Arac_Kiralama
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E5PVFO9\MSSQLSERVERMETO;Initial Catalog=Arac_Kiralama;Integrated Security=True");
        DataTable tablo;

        public void ekle_sil_guncelle(SqlCommand komut, string sorgu)
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public DataTable listele(SqlDataAdapter adtr, string sorgu)
        {
            tablo = new DataTable();
            adtr = new SqlDataAdapter(sorgu, baglanti);
            adtr.Fill(tablo);
            baglanti.Close();               

            return tablo;
            //ekle guncelle islemlerinden sonra kayıtlar güncellenmiş olarak gelecektir.
        }

        public void Bos_Araclar(ComboBox combo, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combo.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }
        public void TC_Ara(TextBox tcara, TextBox tc, TextBox adsoyad,TextBox telefon, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                tc.Text = read["tc"].ToString();
                adsoyad.Text = read["adsoyad"].ToString();
                telefon.Text = read["telefon"].ToString();                
            }
            baglanti.Close();
        }
        public void Ucret_Hesapla(ComboBox comboKiraSekli, TextBox ucret, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboKiraSekli.SelectedIndex == 0) ucret.Text = (int.Parse(read["kiraucreti"].ToString()) * 1).ToString(); //günlük kiralamada indirim yok.
                if (comboKiraSekli.SelectedIndex == 1) ucret.Text = (int.Parse(read["kiraucreti"].ToString()) * 0.90).ToString(); // haftalık kiralamada yüzde 10 indirim
                if (comboKiraSekli.SelectedIndex == 2) ucret.Text = (int.Parse(read["kiraucreti"].ToString()) * 0.80).ToString(); // aylık kiralamada yüzde 20 indirim
            
            }
            baglanti.Close();
        }
        public void CombodanGetir(ComboBox araclar, TextBox marka, TextBox seri, TextBox yil, TextBox renk, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                marka.Text = read["marka"].ToString();
                seri.Text = read["seri"].ToString();
                yil.Text = read["yil"].ToString();
                renk.Text = read["renk"].ToString();
            }
            baglanti.Close();
        }

        public void satisHesapla(Label lbl)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satış", baglanti);
            lbl.Text = "Toplam Tutar= " + komut.ExecuteScalar() + "TL";
            baglanti.Close();
        }
    }
}
