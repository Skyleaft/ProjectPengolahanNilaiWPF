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
    /// Interaction logic for c_inputnilai.xaml
    /// </summary>
    public partial class c_inputnilai : UserControl
    {
        private string nip,kdsekolah;
        koneksi k = new koneksi();
        
        public c_inputnilai(string nipusr)
        {
            InitializeComponent();
            showdatasiswa();
            showdatanilai();
            showmapel();
            ceksekolah();
            kodeotomatis();
            nip = nipusr;
        }
        
        private void ceksekolah()
        {
            k.sql = "select *from data_sekolah";
            k.setdt();
            kdsekolah = k.dt.Rows[0][0].ToString();
        }

        public void showdatasiswa()
        {
            k.sql = "select nis,nama,jenis_kelamin,nama_kelas from siswa left join kelas on siswa.kd_kelas = kelas.kd_kelas";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
        }

        private void showdatanilai()
        {
            k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel";
            k.setdt();
            dg_nilai.ItemsSource = k.dt.DefaultView;
        }

        private void showmapel()
        {
            k.sql = "select * from mapel";
            k.setdt();
            cmb_mapel.ItemsSource = k.dt.DefaultView;
            cmb_mapel.DisplayMemberPath = "nama_mapel";
            cmb_mapel.SelectedValuePath = "kd_mapel";
        }

        private void bersih()
        {
            txt_kdnilai.Text = "";
            txt_kkm.Text = "";
            txt_kls.Text = "";
            txt_nama.Text = "";
            txt_nilai.Text = "";
            txt_nis.Text = "";
            txt_grade.Text = "";
            cmb_jenis.Text = "";
            cmb_mapel.Text = "";
            dp_tglul.Text = "";
            rb_praktek.IsChecked = false;
            rb_teori.IsChecked = false;
            dg_nilai.UnselectAll();
            dg_sisw.UnselectAll();
        }

        private void bersihsiswa()
        {
            txt_kdnilai.Text = "";
            txt_kkm.Text = "";
            txt_kls.Text = "";
            txt_nama.Text = "";
            txt_nilai.Text = "";
            txt_nis.Text = "";
            txt_grade.Text = "";
            cmb_jenis.Text = "";
            cmb_mapel.Text = "";
            dp_tglul.Text = "";
            rb_praktek.IsChecked = false;
            rb_teori.IsChecked = false;
            dg_nilai.UnselectAll();
            kodeotomatis();
        }

        private void bersihnilai()
        {
            txt_kdnilai.Text = "";
            txt_nilai.Text = "";
            txt_grade.Text = "";
            dg_nilai.UnselectAll();
        }

        private void kodeotomatis()
        {
            k.sql = "select *from nilai order by kd_nilai asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "NL-0001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "NL-000" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "NL-00" + tambah;
                }
                else if (tambah < 1000)
                {
                    baru = "NL-0" + tambah;
                }
                else
                {
                    baru = "NL-" + tambah;
                }
            }
            txt_kdnilai.Text = baru;
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            DateTime tglujian = dp_tglul.SelectedDate.Value;
            String tipe = "";
            if (rb_teori.IsChecked == true)
            {
                tipe = "Teori";
            }
            else
            {
                tipe = "Praktek";
            }
            try
            {

                k.sql = "update nilai set tanggal = '" + tglujian.ToString("MM-dd-yyyy") + "',kd_mapel = '" + cmb_mapel.SelectedValue + "',jenis = '" + cmb_jenis.Text + "',tipe_ujian = '" + tipe + "',nilai='" + txt_nilai.Text + "',grade='" + txt_grade.Text + "' where kd_nilai = '" + txt_kdnilai.Text + "'";
                k.setdt();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Ubah" }
            };
            DialogHost.Show(sampleMessageDialog, "MainDialog");
            bersih();
            showdatanilai();
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Yakin Hapus Data?, Kode Nilai : " + txt_kdnilai.Text;
        }
        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from nilai where kd_nilai = '" + txt_kdnilai.Text + "'";
            k.setdt();
            //MessageBox.Show("test hapus " + txt_nip.Text);

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Nilai Berhasil Di Hapus" }
            };
            DialogHost.Show(sampleMessageDialog, "SecondDialog");
            bersih();
            kodeotomatis();
            showdatanilai();
        }

        private void dg_nilai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_nilai.SelectedIndex >= 0)
            {
                dg_sisw.UnselectAll();
                DataRowView dataRow = (DataRowView)dg_nilai.SelectedItem;
                string index = dataRow.Row[2].ToString();
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade,kelas.nama_kelas " +
                    "from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel left join kelas on siswa.kd_kelas = kelas.kd_kelas where nilai.kd_nilai = '"+index+"'";
                k.setdt();

                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nis.Text = baris[0].ToString();
                    txt_kdnilai.Text = baris[2].ToString();
                    txt_nama.Text = baris[1].ToString();
                    cmb_mapel.Text = baris[3].ToString();
                    dp_tglul.Text = baris[4].ToString();
                    cmb_jenis.Text = baris[5].ToString();
                    if(baris[6].ToString() == "Teori")
                    {
                        rb_teori.IsChecked = true;
                    }
                    else
                    {
                        rb_praktek.IsChecked = true;
                    }
                    txt_nilai.Text= baris[7].ToString();
                    txt_grade.Text = baris[8].ToString();
                    txt_kls.Text = baris[9].ToString();
                }
                btn_simpannilai.IsEnabled = false;
                btn_hapus.IsEnabled = true;
                btn_ubah.IsEnabled = true;
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
                
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nis.Text = baris[1].ToString();
                    txt_nama.Text = baris[2].ToString();
                    
                    txt_kls.Text = baris[11].ToString();
                }
                btn_simpannilai.IsEnabled = true;
                btn_hapus.IsEnabled = false;
                btn_ubah.IsEnabled = false;
            }
        }

        private void txt_nilai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void txt_nilai_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_nilai.Text != "")
            {
                int val = 0;
                bool res = Int32.TryParse(txt_nilai.Text, out val);
                if (res == true && val > -1 && val < 101)
                {
                    // add record
                    int nilai = int.Parse(txt_nilai.Text);
                    if (nilai <= 50)
                    {
                        txt_grade.Text = "D";
                    }
                    else if (nilai <= 75)
                    {
                        txt_grade.Text = "C";
                    }
                    else if (nilai <= 85)
                    {
                        txt_grade.Text = "B";
                    }
                    else if (nilai <= 100)
                    {
                        txt_grade.Text = "A";
                    }
                }
                else
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Harap Masukan nilai Dari 0 - 100 saja" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                    txt_nilai.Text = "";
                    txt_grade.Text = "";
                    return;
                }
            }
            else
            {
                txt_grade.Text = "";
            }
        }

        private void btn_simpannilai_Click(object sender, RoutedEventArgs e)
        {
            
            if (txt_nis.Text == "" || txt_nilai.Text == "" || dp_tglul.Text == "" || cmb_jenis.Text == "" 
                || cmb_mapel.Text == "" || rb_praktek.IsChecked == false && rb_teori.IsChecked == false)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data ada yang kosong!!!" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");
            }
            else
            {
                DateTime tglujian = dp_tglul.SelectedDate.Value;
                String tipe = "";
                if (rb_teori.IsChecked == true)
                {
                    tipe = "Teori";
                }
                else
                {
                    tipe = "Praktek";
                }
                try
                {
                    k.sql = "insert into nilai values('" + txt_kdnilai.Text + "','" + txt_nis.Text + "','" + nip + "','" + cmb_mapel.SelectedValue + "','" + tglujian.ToString("MM-dd-yyyy") + "','" + cmb_jenis.Text + "','" + tipe + "','" + txt_nilai.Text + "','" + txt_grade.Text + "','"+kdsekolah+"')";
                    k.setdt();
                    showdatanilai();
                    bersihnilai();
                    kodeotomatis();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btn_baru_Click(object sender, RoutedEventArgs e)
        {
            bersih();
            btn_simpannilai.IsEnabled = true;
            btn_hapus.IsEnabled = false;
            btn_ubah.IsEnabled = false;
            kodeotomatis();
        }

        private void txt_cari_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            k.sql = "select nis,nama,jenis_kelamin,nama_kelas from siswa inner join kelas on siswa.kd_kelas = kelas.kd_kelas where nis like'%" + txt_cari.Text + "%' or nama like'%" + txt_cari.Text + "%' or jenis_kelamin like'%" + txt_cari.Text + "%'";
            k.setdt();
            dg_sisw.ItemsSource = k.dt.DefaultView;
        }

        private void cmb_mapel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmb_mapel.SelectedIndex!= -1)
            {
                k.sql = "select kkm from mapel where kd_mapel='" + cmb_mapel.SelectedValue + "'";
                k.setdt();
                txt_kkm.Text = k.dt.Rows[0][0].ToString();
            }
            
        }

        
    }
}
