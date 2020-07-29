using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for plus_pegawai.xaml
    /// </summary>
    public partial class plus_pegawai : UserControl
    {
        public plus_pegawai()
        {
            InitializeComponent();
        }
        koneksi k = new koneksi();
        private String alamat_foto = "";

        private void txt_nohp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void txt_nip_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nip.Text == "" || txt_nama.Text == "" || txt_nohp.Text == "" || txt_alamat.Text == "" || txt_tempatlahir.Text == "" || dp_tgllahir.Text=="" || cmb_agama.Text == "" || cmb_jabatan.Text=="")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialog");
            }
            else if (img_foto.Source == null)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Harap Masukan Foto" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialog");
            }
            else
            {
                try
                {
                    DateTime tglahir = dp_tgllahir.SelectedDate.Value;
                    String jk = "";
                    if (rb_laki.IsChecked == true)
                    {
                        jk = "Laki - Laki";
                    }
                    else
                    {
                        jk = "Perempuan";
                    }
                    k.sql = "insert into pegawai select '" + txt_nip.Text + "','" + txt_nama.Text + "','" + jk + "','" + tglahir.ToString("MM-dd-yyyy") + "','" + txt_alamat.Text + "','" + txt_tempatlahir.Text + "','" + cmb_agama.Text + "','" + cmb_jabatan.Text + "','" + txt_nohp.Text + "',bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar";
                    k.setdt();
                    if(txt_username.Text!="" && txt_password.Password != "")
                    {
                        k.sql = "insert into akun values('" + txt_username.Text + "','" + txt_password.Password + "','" + txt_nip.Text + "','" + cmb_jabatan.Text + "')";
                        k.setdt();
                    }
                    

                    DialogHost.CloseDialogCommand.Execute(null, this);
                    
                    

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "SecondDialog");
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
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

        private void btn_batal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
