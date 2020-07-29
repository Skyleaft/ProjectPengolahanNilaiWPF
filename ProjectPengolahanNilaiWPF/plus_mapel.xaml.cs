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
    /// Interaction logic for plus_mapel.xaml
    /// </summary>
    public partial class plus_mapel : UserControl
    {
        public plus_mapel()
        {
            InitializeComponent();
            kodeotomatis();
        }
        koneksi k = new koneksi();

        private void kodeotomatis()
        {
            k.sql = "select *from mapel order by kd_mapel asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "MP-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "MP-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "MP-0" + tambah;
                }
                else
                {
                    baru = "MP-" + tambah;
                }
            }
            txt_kdmapel.Text = baru;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            
            if (txt_kdmapel.Text == "" || txt_namamapel.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "PMapelDialog");
            }
            else if (txt_kkm.Text!="")
            {
                int val = int.Parse(txt_kkm.Text);
                if(val>59 && val < 101)
                {
                    try
                    {
                        k.sql = "insert into mapel select '" + txt_kdmapel.Text + "','" + txt_namamapel.Text + "','" + txt_kkm.Text + "'";
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
                else
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Batas kkm 60/Nilai melebihi batas" }
                    };
                    DialogHost.Show(sampleMessageDialog, "PMapelDialog");
                    return;
                }
            }
        }

        private void txt_kkm_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        
    }
}
