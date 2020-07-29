using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for c_siswa.xaml
    /// </summary>
    public partial class c_siswa : UserControl
    {
        public c_siswa()
        {
            InitializeComponent();
            showdata();
            //kodeotomatis();
        }

        koneksi k = new koneksi();
        private String alamat_foto;

        public void showdata()
        {
            k.sql = "select nis,nama,jenis_kelamin,alamat,tgl_lahir,agama from siswa";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_alamat.Text = "";
            txt_nama.Text = "";
            txt_nis.Text = "";
            txt_nisn.Text = "";
            txt_tempatlahir.Text = "";
            cmb_agama.Text = "";
            rb_laki.IsChecked = false;
            rb_perempuan.IsChecked = false;
            dp_tgllahir.Text = "";
            img_foto.Source = null;
            btn_ubah.IsEnabled = false;
            btn_hapus.IsEnabled = false;
            dg_sisw.UnselectAll();
        }

        

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            DateTime tgllahir = dp_tgllahir.SelectedDate.Value;
            String jk = "";
            if (rb_laki.IsChecked == true)
            {
                jk = "Laki - Laki";
            }
            else
            {
                jk = "Perempuan";
            }
            try
            {

                k.sql = "update siswa set nisn = '" + txt_nisn.Text + "',nama = '" + txt_nama.Text + "',jenis_kelamin = '" + jk + "',tgl_lahir = '" + tgllahir.ToString("MM-dd-yyyy") + "',alamat='" + txt_alamat.Text + "',tempat_lahir='" + txt_tempatlahir.Text + "',agama='" + cmb_agama.Text + "',foto=bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar where nis = '" + txt_nis.Text + "'";
                k.setdt();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                k.sql = "update siswa set nisn = '" + txt_nisn.Text + "',nama = '" + txt_nama.Text + "',jenis_kelamin = '" + jk + "',tgl_lahir = '" + tgllahir.ToString("MM-dd-yyyy") + "',alamat='" + txt_alamat.Text + "',tempat_lahir='" + txt_tempatlahir.Text + "',agama='" + cmb_agama.Text + "' where nis = '" + txt_nis.Text + "'";
                k.setdt();
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
            Message.Text = "Yakin Hapus Data NIS : " + txt_nis.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from siswa where nis = '" + txt_nis.Text + "'";
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

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select nis,nama,jenis_kelamin,tgl_lahir,alamat,agama from siswa where nis like'%" + txt_cari.Text + "%' or nama like'%" + txt_cari.Text + "%' or jenis_kelamin like'%" + txt_cari.Text + "%' or agama like'%" + txt_cari.Text + "%' or tgl_lahir like'%" + txt_cari.Text + "%' or alamat like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
        }


        private void btn_ambil_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Masukan Foto";
            op.Filter = "Semua Gambar|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                alamat_foto = op.FileName;
                img_foto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void dg_sisw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_sisw.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_sisw.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from siswa left join kelas on siswa.kd_kelas = kelas.kd_kelas where siswa.nis = '" + index + "'";
                k.setdt();
                Byte[] tmpfoto = new Byte[0];
                String tgllahir;
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nisn.Text = baris[0].ToString();
                    txt_nis.Text = baris[1].ToString();
                    txt_nama.Text = baris[2].ToString();
                    if (baris[3].ToString() == "Laki - Laki")
                    {
                        rb_laki.IsChecked = true;
                    }
                    else
                    {
                        rb_perempuan.IsChecked = true;
                    }
                    tgllahir = baris[4].ToString();
                    DateTime dt = Convert.ToDateTime(tgllahir);
                    dp_tgllahir.SelectedDate = dt;
                    txt_tempatlahir.Text = baris[5].ToString();
                    txt_alamat.Text = baris[6].ToString();
                    cmb_agama.Text = baris[7].ToString();

                    tmpfoto = (Byte[])baris[9];
                    img_foto.Source = ByteImageConverter.ByteToImage(tmpfoto);
                }
                btn_ubah.IsEnabled = true;
                btn_hapus.IsEnabled = true;
            }
        }

        private void btn_tambahsiswa_Click(object sender, RoutedEventArgs e)
        {
            var plussw = new plus_siswa();
            DialogHost.Show(plussw, "MainDialog" ,ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bersih();
            showdata();
        }

        private void txt_nisn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

    }
}
