using MaterialDesignColors.WpfExample.Domain;
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
    /// Interaction logic for c_kelas.xaml
    /// </summary>
    public partial class c_kelas : UserControl
    {
        public c_kelas()
        {
            InitializeComponent();
            showdata();
        }
        koneksi k = new koneksi();
        string kdkelas, nis, selectednis;
        public void showdata()
        {
            k.sql = "select * from kelas";
            k.setdt();
            dg_kls.ItemsSource = k.dt.DefaultView;

        }
        private void showsiswa()
        {
            k.sql = "select nis,nama,jenis_kelamin from siswa where kd_kelas = '" + kdkelas + "'";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
            int jmlsiswa = k.dt.Rows.Count;
            txt_jmlsis.Text = jmlsiswa.ToString();
        }

        public void bersih()
        {
            txt_kdkls.Text = "";
            txt_namakls.Text = "";
            txt_nis.Text = "";
            txt_nama.Text = "";
            rb_laki.IsChecked = false;
            rb_perempuan.IsChecked = false;
            btn_ubah.IsEnabled = false;
            btn_hapus.IsEnabled = false;
            dg_kls.UnselectAll();
            dg_sisw.ItemsSource = null;
            gb_masukan.Visibility = Visibility.Hidden;
            txt_jmlsis.Text = "";
        }

        private void bersih2()
        {
            txt_nis.Text = "";
            txt_nama.Text = "";
            rb_laki.IsChecked = false;
            rb_perempuan.IsChecked = false;
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                k.sql = "update kelas set nama_kelas = '" + txt_namakls.Text + " where kd_kelas = '" + txt_kdkls.Text + "'";
                k.setdt();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //MessageBox.Show("Data Berhasil Di Ubah", "Pemberitahuan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Ubah" }
            };
            DialogHost.Show(sampleMessageDialog, "MainDialog");
            bersih();
            showdata();
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Yakin Hapus Data Kode Kelas : " + txt_kdkls.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from kelas where kd_kelas = '" + txt_kdkls.Text + "'";
            k.setdt();
            //MessageBox.Show("test hapus " + txt_nip.Text);

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.Show(sampleMessageDialog, "SecondDialog");
            bersih();
            showdata();
        }

        private void btn_tambahkls_Click(object sender, RoutedEventArgs e)
        {
            var pluskls = new plus_kelas();
            DialogHost.Show(pluskls, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bersih();
            showdata();
        }

        private void dg_kls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_kls.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_kls.SelectedItem;
                string index = dataRow.Row[0].ToString();
                kdkelas = index;
                k.sql = "select *from kelas where kd_kelas = '" + index + "'";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_kdkls.Text = baris[0].ToString();
                    txt_namakls.Text = baris[1].ToString();
                }
                btn_ubah.IsEnabled = true;
                btn_hapus.IsEnabled = true;

                k.sql = "select nis,nama,jenis_kelamin from siswa where kd_kelas = '" + index + "'";
                k.setdt();
                dg_sisw.ItemsSource = k.dt.DefaultView;
                int jmlsiswa = k.dt.Rows.Count;
                txt_jmlsis.Text = jmlsiswa.ToString();

                bersih2();
                gb_masukan.Visibility = Visibility.Visible;
                btn_hapussiswa.Visibility = Visibility.Collapsed;
                btn_masukansis.Visibility = Visibility.Visible;
            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from kelas where kd_kelas like'%" + txt_cari.Text + "%' or nama_kelas like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_kls.ItemsSource = k.dt.DefaultView;
        }

        private async void btn_pilsis_ClickAsync(object sender, RoutedEventArgs e)
        {
            var pilsis = new pilih_siswa();
            
            var result = await DialogHost.Show(pilsis, "MainDialog", AmbilDataSiswa);
            nis = pilsis.nis;
            k.sql = "select nis,nama,jenis_kelamin from siswa where nis='"+pilsis.nis+"'";
            k.setdt();
            foreach(DataRow baris in k.dt.Rows)
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

        private void AmbilDataSiswa(object sender, DialogClosingEventArgs eventArgs)
        {


        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            bersih();

        }

        private void btn_masukansis_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "update siswa set kd_kelas='"+kdkelas+"' where nis='"+nis+"'";
            k.setdt();
            showsiswa();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Siswa berhasil dimasukan ke Kelas : "+kdkelas }
            };
            DialogHost.Show(sampleMessageDialog, "SecondDialog");
            bersih2();
        }

        private void txt_carisiswa_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select nis,nama,jenis_kelamin from siswa where nis like'%" + txt_cari.Text + "%' or nama like'%" + txt_cari.Text + "%' or jenis_kelamin like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
        }

        private void dg_sisw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_sisw.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_sisw.SelectedItem;
                string index = dataRow.Row[0].ToString();
                selectednis = index;
                k.sql = "select nis,nama,jenis_kelamin from siswa where nis='" + index + "'";
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
                btn_hapussiswa.Visibility = Visibility.Visible;
                btn_masukansis.Visibility = Visibility.Collapsed;

            }
        }

        private void btn_hapussiswa_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "update siswa set kd_kelas='' where nis='"+selectednis+"'";
            k.setdt();
            showsiswa();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Dikeluarkan dari kelas : " + kdkelas }
            };
            DialogHost.Show(sampleMessageDialog, "SecondDialog");
            bersih2();
            btn_hapussiswa.Visibility = Visibility.Collapsed;
            btn_masukansis.Visibility = Visibility.Visible;

        }
    }
}
