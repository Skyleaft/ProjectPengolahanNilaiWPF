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
    /// Interaction logic for c_mapel.xaml
    /// </summary>
    public partial class c_mapel : UserControl
    {
        public c_mapel()
        {
            InitializeComponent();
            showdata();
        }
        koneksi k = new koneksi();

        public void showdata()
        {
            k.sql = "select * from mapel";
            k.setdt();
            dg_mapl.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_kdmapl.Text = "";
            txt_namamapl.Text = "";
            txt_kkm.Text = "";
            btn_ubah.IsEnabled = false;
            btn_hapus.IsEnabled = false;
            dg_mapl.UnselectAll();
        }

        private void dg_mapl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_mapl.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_mapl.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from mapel where kd_mapel = '" + index + "'";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_kdmapl.Text = baris[0].ToString();
                    txt_namamapl.Text = baris[1].ToString();
                    txt_kkm.Text = baris[2].ToString();
                }
                btn_ubah.IsEnabled = true;
                btn_hapus.IsEnabled = true;
            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from mapel where kd_mapel like'%" + txt_cari.Text + "%' or nama_mapel like'%" + txt_cari.Text + "%' or kkm like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_mapl.ItemsSource = k.dt.DefaultView;
        }

        private void btn_tambahmapl_Click(object sender, RoutedEventArgs e)
        {
            var plusmp = new plus_mapel();
            DialogHost.Show(plusmp, "MainDialog",ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bersih();
            showdata();
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                k.sql = "update mapel set nama_mapel = '" + txt_namamapl.Text + "',kkm = '" + txt_kkm.Text + "' where kd_mapel = '" + txt_kdmapl.Text + "'";
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
            Message.Text = "Yakin Hapus Data Kode Mapel : " + txt_kdmapl.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from mapel where kd_mapel = '" + txt_kdmapl.Text + "'";
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

        private void txt_kkm_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

    }
}
