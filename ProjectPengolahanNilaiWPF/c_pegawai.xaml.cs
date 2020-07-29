using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for c_pegawai.xaml
    /// </summary>
    public partial class c_pegawai : UserControl
    {
        public c_pegawai()
        {
            InitializeComponent();
            showdata();
        }
        koneksi k = new koneksi();
        private String alamat_foto;

        public void showdata()
        {
            k.sql = "select nip,nama,jenis_kelamin,alamat,tgl_lahir,agama,jabatan from pegawai";
            k.setdt();
            dg_peg.ItemsSource = k.dt.DefaultView;
            
        }

        public void bersih()
        {
            txt_alamat.Text = "";
            txt_nama.Text = "";
            txt_nip.Text = "";
            txt_nohp.Text = "";
            txt_tempatlahir.Text = "";
            cmb_agama.Text = "";
            cmb_jabatan.Text = "";
            txt_password.Password = "";
            txt_username.Text = "";
            rb_laki.IsChecked = false;
            rb_perempuan.IsChecked = false;
            dp_tgllahir.Text = "";
            img_foto.Source = null;
            btn_ubah.IsEnabled = false;
            btn_hapus.IsEnabled = false;
            dg_peg.UnselectAll();
        }

        private void refresh_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            bersih();
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            k.sql = "select nip,nama,jenis_kelamin,jabatan,tgl_lahir,alamat,agama from pegawai where nip like'%" + txt_cari.Text + "%' or nama like'%" + txt_cari.Text + "%' or jenis_kelamin like'%" + txt_cari.Text + "%' or jabatan like'%" + txt_cari.Text + "%' or agama like'%" + txt_cari.Text + "%' or tgl_lahir like'%" + txt_cari.Text + "%' or alamat like'%" + txt_cari.Text+"%'";
            k.setdt();
            dg_peg.ItemsSource = k.dt.DefaultView;
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
                k.sql = "select *from akun where nip='" + txt_nip.Text + "'";
                k.setdt();
                if (k.dt.Rows.Count == 0)
                {
                    k.sql = "insert into akun values('"+txt_username.Text+"','"+txt_password.Password+"','"+txt_nip.Text+"','"+cmb_jabatan.Text+"')";
                    k.setdt();
                }
                else
                {
                    k.sql = "update akun set username='"+txt_username.Text+"', password='"+txt_password.Password+"',hak_akses='"+cmb_jabatan.Text+"' where nip='"+txt_nip.Text+"'";
                    k.setdt();
                }

                k.sql = "update pegawai set nama = '" + txt_nama.Text + "',jenis_kelamin = '" + jk + "',tgl_lahir = '" + tgllahir.ToString("MM-dd-yyyy") + "',alamat='" + txt_alamat.Text + "',tempat_lahir='" + txt_tempatlahir.Text + "',agama='" + cmb_agama.Text + "',jabatan='" + cmb_jabatan.Text + "',no_hp='" + txt_nohp.Text + "',foto=bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar where nip = '" + txt_nip.Text + "'";
                k.setdt();

                

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                k.sql = "update pegawai set nama = '" + txt_nama.Text + "',jenis_kelamin = '" + jk + "',tgl_lahir = '" + tgllahir.ToString("MM-dd-yyyy") + "',alamat='" + txt_alamat.Text + "',tempat_lahir='" + txt_tempatlahir.Text + "',agama='" + cmb_agama.Text + "',jabatan='" + cmb_jabatan.Text + "',no_hp='" + txt_nohp.Text + "' where nip = '" + txt_nip.Text + "'";
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

        private void txt_nip_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void txt_nohp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }


        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            //var sampleMessageYesNo = new SampleMessageYesNo
            //{
            //    Message = { Text = "Yakin Hapus Data Nip : "+txt_nip.Text }
            //};
            //sampleMessageYesNo.anip = txt_nip.Text;
            //DialogHost.Show(sampleMessageYesNo, "MainDialog");
            //DialogHost.Show(this);
            Message.Text = "Yakin Hapus Data Nip : " + txt_nip.Text;

        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from pegawai where nip = '" + txt_nip.Text + "'";
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


        private void btn_tambahpeg_Click(object sender, RoutedEventArgs e)
        {
            var pluspeg = new plus_pegawai();
            DialogHost.Show(pluspeg, "MainDialog", ClosingEventHandler);

        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bersih();
            showdata();
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

        private void dg_peg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_peg.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_peg.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from pegawai left join akun on pegawai.nip = akun.nip where pegawai.nip = '" + index + "'";
                k.setdt();
                Byte[] tmpfoto = new Byte[0];
                String tgllahir;
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nip.Text = baris[0].ToString();
                    txt_nama.Text = baris[1].ToString();
                    if (baris[2].ToString() == "Laki - Laki")
                    {
                        rb_laki.IsChecked = true;
                    }
                    else
                    {
                        rb_perempuan.IsChecked = true;
                    }
                    tgllahir = baris[3].ToString();
                    DateTime dt = Convert.ToDateTime(tgllahir);
                    dp_tgllahir.SelectedDate = dt;
                    txt_alamat.Text = baris[4].ToString();
                    txt_tempatlahir.Text = baris[5].ToString();
                    cmb_agama.Text = baris[6].ToString();
                    cmb_jabatan.Text = baris[7].ToString();


                    txt_nohp.Text = baris[8].ToString();

                    tmpfoto = (Byte[])baris[9];
                    img_foto.Source = ByteImageConverter.ByteToImage(tmpfoto);


                    txt_username.Text = baris[10].ToString();
                    txt_password.Password = baris[11].ToString();
                }
                btn_ubah.IsEnabled = true;
                btn_hapus.IsEnabled = true;
            }
        }
    }
}
