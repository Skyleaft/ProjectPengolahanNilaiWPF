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
    /// Interaction logic for plus_siswa.xaml
    /// </summary>
    public partial class plus_siswa : UserControl
    {
        public plus_siswa()
        {
            InitializeComponent();
            kodeotomatis();
        }
        koneksi k = new koneksi();
        string alamat_foto;

        private void kodeotomatis()
        {
            k.sql = "select *from siswa order by nis asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            String thn = DateTime.Now.ToString("yy");
            int thn2 = int.Parse(thn) + 1;

            if (cekbaris == 0)
            {
                baru = thn + thn2 + "001";
            }
            else
            {
                String str = k.dt.Rows[cekbaris - 1][1].ToString();
                tambah = Convert.ToInt32(str.Substring(str.Length - 3)) + 1;
                if (tambah < 10)
                {
                    baru = thn + thn2 + "00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = thn + thn2 + "0" + tambah;
                }
                else
                {
                    baru = thn + thn2 + tambah;
                }
            }
            txt_nis.Text = baru;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nisn.Text == "" || txt_nis.Text == "" || txt_nama.Text == "" ||  txt_alamat.Text == "" || txt_tempatlahir.Text == "" || dp_tgllahir.Text == "" || cmb_agama.Text == "" )
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialog2");
            }
            else if (img_foto.Source == null)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Harap Masukan Foto" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialog2");
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
                    k.sql = "insert into siswa select '" + txt_nisn.Text + "','" + txt_nis.Text + "','" + txt_nama.Text + "','" + jk + "','" + tglahir.ToString("MM-dd-yyyy") + "','" + txt_tempatlahir.Text + "','" + txt_alamat.Text + "','" + cmb_agama.Text + "','',bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar";
                    k.setdt();

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

        private void txt_nisn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }
    }
}
