using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System.Data;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for c_sekolah.xaml
    /// </summary>
    public partial class c_sekolah : UserControl
    {
        public c_sekolah()
        {
            InitializeComponent();
            showdata();
            cekkepalasklh();
        }

        koneksi k = new koneksi();
        private String alamat_foto;

        private void cekkepalasklh()
        {
            k.sql = "select nip,nama from pegawai where jabatan='Kepala Sekolah'";
            k.setdt();
            int cekdata = k.dt.Rows.Count;
            if (cekdata != 0)
            {
                txt_nip.Text = k.dt.Rows[0][0].ToString();
                txt_kepalasklh.Text = k.dt.Rows[0][1].ToString();
            }
        }
        
        private void showdata()
        {
            k.sql = "select *from data_sekolah";
            k.setdt();

            Byte[] tmpfoto = new Byte[0];

            foreach (DataRow baris in k.dt.Rows)
            {
                txt_kdsklh.Text = baris[0].ToString();
                txt_namasklh.Text = baris[1].ToString();
                txt_alamat.Text = baris[2].ToString();
                txt_kota.Text = baris[3].ToString();
                txt_kec.Text = baris[4].ToString();
                txt_kdpos.Text = baris[5].ToString();
                txt_kepalasklh.Text = baris[6].ToString();
                txt_nip.Text = baris[7].ToString();
                txt_notelp.Text = baris[8].ToString();
                tmpfoto = (Byte[])baris[9];
                img_foto.Source = ByteImageConverter.ByteToImage(tmpfoto);
            }
        }

        private void btn_simpan_Click (object sender, RoutedEventArgs e)
        {
            k.sql = "select * from data_sekolah";
            k.setdt();
            int cekdata = k.dt.Rows.Count;
            if (cekdata==0)
            {
                if(txt_kdsklh.Text=="" || txt_namasklh.Text=="" || txt_alamat.Text=="" || txt_kota.Text=="" || txt_kec.Text=="" || txt_kdpos.Text=="" || txt_kepalasklh.Text=="" || txt_nip.Text=="" || txt_notelp.Text == "")
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Lengkapi Data Dulu" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                }
                else if (img_foto.Source == null)
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Harap Masukan Foto" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                }
                else
                {
                    try
                    {
                        k.sql = "insert into data_sekolah select '" + txt_kdsklh.Text + "','" + txt_namasklh.Text + "','" + txt_alamat.Text + "','"+txt_kota.Text+"','"+txt_kec.Text+"','"+txt_kdpos.Text+"','" + txt_kepalasklh.Text + "','" + txt_nip.Text + "','" + txt_notelp.Text + "',bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar";
                        k.setdt();

                        var sampleMessageDialog = new SampleMessageDialog
                        {
                            Message = { Text = "Data Berhasil Tersimpan" }
                        };
                        DialogHost.Show(sampleMessageDialog, "MainDialog");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Gagal Didaftarkan " + ex);
                    }

                }
            }
            else
            {
                try
                {
                    k.sql = "update data_sekolah set kd_sekolah='" + txt_kdsklh.Text + "',nama_sekolah='" + txt_namasklh.Text + "',alamat='" + txt_alamat.Text + "',kota='"+txt_kota.Text+"',kecamatan='"+txt_kec.Text+"',kdpos='"+txt_kdpos.Text+"',kepala_sekolah='" + txt_kepalasklh.Text + "',nip='" + txt_nip.Text + "',no_telp='" + txt_notelp.Text + "',logo=bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar) where kd_sekolah='"+txt_kdsklh.Text+"'";
                    k.setdt();
                }
                catch (Exception ex)
                {
                    k.sql = "update data_sekolah set kd_sekolah='" + txt_kdsklh.Text + "',nama_sekolah='" + txt_namasklh.Text + "',alamat='" + txt_alamat.Text + "',kota='" + txt_kota.Text + "',kecamatan='" + txt_kec.Text + "',kdpos='" + txt_kdpos.Text + "',kepala_sekolah='" + txt_kepalasklh.Text + "',nip='" + txt_nip.Text + "',no_telp='" + txt_notelp.Text + "' where kd_sekolah='" + txt_kdsklh.Text + "'";
                    k.setdt();
                }

                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data Berhasil Tersimpan" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");
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

        private void txt_kdpos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }
    }
}
