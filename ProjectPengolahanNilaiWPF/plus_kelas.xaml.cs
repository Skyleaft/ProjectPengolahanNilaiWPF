using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for plus_kelas.xaml
    /// </summary>
    public partial class plus_kelas : UserControl
    {
        public plus_kelas()
        {
            InitializeComponent();
            kodeotomatis();
        }
        koneksi k = new koneksi();

        private void kodeotomatis()
        {
            k.sql = "select *from kelas order by kd_kelas asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "KLS-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "KLS-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "KLS-0" + tambah;
                }
                else
                {
                    baru = "KLS-" + tambah;
                }
            }
            txt_kdkls.Text = baru;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_kdkls.Text == "" || txt_namakls.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialogKelas");
            }
            else
            {
                try
                {
                    k.sql = "insert into kelas select '" + txt_kdkls.Text + "','" + txt_namakls.Text + "'";
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
    }
}
