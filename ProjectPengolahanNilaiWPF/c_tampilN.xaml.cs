using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for c_tampilN.xaml
    /// </summary>
    public partial class c_tampilN : UserControl
    {
        public c_tampilN()
        {
            InitializeComponent();
        }
        koneksi k = new koneksi();
        string nis, selectednis;

        private void btn_tampilin_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "select *from nilai inner join mapel on mapel.kd_mapel = nilai.kd_mapel where nis='"+nis+"'";
            k.setdt();
            dg_nilai.ItemsSource = k.dt.DefaultView;
        }

        private void dg_nilai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_nilai.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_nilai.SelectedItem;
                string index = dataRow.Row[0].ToString();
                k.sql = "select nilai.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where nilai.kd_nilai = '" + index + "'";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nis.Text = baris[0].ToString();
                    txt_kdnilai.Text = baris[2].ToString();
                    txt_nama.Text = baris[1].ToString();
                    cmb_mapel.Text = baris[3].ToString();
                    dp_tglul.Text = baris[4].ToString();
                    cmb_jenis.Text = baris[5].ToString();
                    if (baris[6].ToString() == "Teori")
                    {
                        rb_teori.IsChecked = true;
                    }
                    else
                    {
                        rb_praktek.IsChecked = true;
                    }
                    txt_nilai.Text = baris[7].ToString();
                    txt_grade.Text = baris[8].ToString();
                }
                btn_hapus.IsEnabled = true;
                btn_ubah.IsEnabled = true;
            }
        }

        private void txt_nilai_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txt_nilai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private async void btn_pilsis_ClickAsync(object sender, RoutedEventArgs e)
        {
            var pilsis = new pilih_nsis();

            var result = await DialogHost.Show(pilsis, "MainDialog");
            nis = pilsis.nis;
            k.sql = "select nis,nama,jenis_kelamin,kelas.nama_kelas from siswa inner join kelas on siswa.kd_kelas = kelas.kd_kelas where siswa.nis='" + pilsis.nis + "'";
            k.setdt();
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_nis.Text = baris[0].ToString();
                txt_nama.Text = baris[1].ToString();
                if (baris[2].ToString() == "Laki - Laki")
                {
                    rb_laki.IsChecked = true;
                }
                else
                {
                    rb_perempuan.IsChecked = true;
                }
            }
        }
    }
}
