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
    /// Interaction logic for pilih_nsis.xaml
    /// </summary>
    public partial class pilih_nsis : UserControl
    {
        public pilih_nsis()
        {
            InitializeComponent();
            showdata();
        }
        koneksi k = new koneksi();
        public String nis { get; set; }

        public void showdata()
        {
            k.sql = "select nis,nama,jenis_kelamin from siswa";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;

        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            if (dg_sisw.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_sisw.SelectedItem;
                string index = dataRow.Row[0].ToString();
                nis = index;

                btn_batal.Command.Execute(null);
            }
            else
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Anda Belum Memilih Siswa Yang ada Di Tabel!" }
                };
                DialogHost.Show(sampleMessageDialog, "MsgDialogPil2");
            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select nis,nama,jenis_kelamin from siswa where nis like'%" + txt_cari.Text + "%' or nama like'%" + txt_cari.Text + "%' or jenis_kelamin like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
        }
    }
}
