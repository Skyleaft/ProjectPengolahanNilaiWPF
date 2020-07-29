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
    /// Interaction logic for c_laporan.xaml
    /// </summary>
    public partial class c_laporan : UserControl
    {
        public c_laporan()
        {
            InitializeComponent();
            showmapel();
            showkelas();
        }
        koneksi k = new koneksi();
        private string kueri="";

        private void showmapel()
        {
            k.sql = "select * from mapel";
            k.setdt();
            cmb_mapel.ItemsSource = k.dt.DefaultView;
            cmb_mapel.DisplayMemberPath = "nama_mapel";
            cmb_mapel.SelectedValuePath = "kd_mapel";
        }

        private void showkelas()
        {
            k.sql = "select * from kelas";
            k.setdt();
            cmb_kelas.ItemsSource = k.dt.DefaultView;
            cmb_kelas.DisplayMemberPath = "nama_kelas";
            cmb_kelas.SelectedValuePath = "kd_kelas";
        }

        private void btn_tampil_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_kelas.Text != "" && cmb_mapel.Text != "" && dp_tglul.Text != "")
            {
                DateTime tglujian = dp_tglul.SelectedDate.Value;
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where siswa.kd_kelas='" + cmb_kelas.SelectedValue + "' and mapel.kd_mapel='" + cmb_mapel.SelectedValue + "' and tanggal='" + tglujian.ToString("MM-dd-yyyy") + "'";
                k.setdt();
            }
            else if (cmb_kelas.Text != "" && cmb_mapel.Text != "")
            {
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where siswa.kd_kelas='" + cmb_kelas.SelectedValue + "' and mapel.kd_mapel='" + cmb_mapel.SelectedValue + "'";
                k.setdt();
                kueri = "SELECT ViewNilai.nis, ViewNilai.nip, ViewNilai.tanggal, ViewNilai.jenis, ViewNilai.tipe_ujian, ViewNilai.nilai, ViewNilai.grade, ViewNilai.nama, ViewNilai.jenis_kelamin, ViewNilai.nama_kelas, ViewNilai.nama_mapel, ViewNilai.kkm, ViewNilai.nama_sekolah, ViewNilai.kota, ViewNilai.kecamatan, ViewNilai.kdpos, ViewNilai.Expr1, ViewNilai.alamat, ViewNilai.logo FROM ProjectPengolahanNilai.dbo.ViewNilai ViewNilai where ViewNilai.kd_mapel = '" + cmb_mapel.SelectedValue + "' and ViewNilai.kd_kelas = '"+cmb_kelas.SelectedValue+"'";

            }
            else if (cmb_kelas.Text != "")
            {
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where siswa.kd_kelas='"+cmb_kelas.SelectedValue+"'";
                k.setdt();
                kueri = "SELECT ViewNilai.nis, ViewNilai.nip, ViewNilai.tanggal, ViewNilai.jenis, ViewNilai.tipe_ujian, ViewNilai.nilai, ViewNilai.grade, ViewNilai.nama, ViewNilai.jenis_kelamin, ViewNilai.nama_kelas, ViewNilai.nama_mapel, ViewNilai.kkm, ViewNilai.nama_sekolah, ViewNilai.kota, ViewNilai.kecamatan, ViewNilai.kdpos, ViewNilai.Expr1, ViewNilai.alamat, ViewNilai.logo FROM ProjectPengolahanNilai.dbo.ViewNilai ViewNilai where ViewNilai.kd_kelas = '" + cmb_kelas.SelectedValue + "'";
            }
            else if (cmb_mapel.Text !="")
            {
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where mapel.kd_mapel='" + cmb_mapel.SelectedValue + "'";
                k.setdt();
                kueri = "SELECT ViewNilai.nis, ViewNilai.nip, ViewNilai.tanggal, ViewNilai.jenis, ViewNilai.tipe_ujian, ViewNilai.nilai, ViewNilai.grade, ViewNilai.nama, ViewNilai.jenis_kelamin, ViewNilai.nama_kelas, ViewNilai.nama_mapel, ViewNilai.kkm, ViewNilai.nama_sekolah, ViewNilai.kota, ViewNilai.kecamatan, ViewNilai.kdpos, ViewNilai.Expr1, ViewNilai.alamat, ViewNilai.logo FROM ProjectPengolahanNilai.dbo.ViewNilai ViewNilai where ViewNilai.kd_mapel = '"+cmb_mapel.SelectedValue+"'";
            }
            else if (dp_tglul.Text != "")
            {
                DateTime tglujian = dp_tglul.SelectedDate.Value;
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel where tanggal='" + tglujian.ToString("MM-dd-yyyy") + "'";
                k.setdt();
                kueri = "SELECT ViewNilai.nis, ViewNilai.nip, ViewNilai.tanggal, ViewNilai.jenis, ViewNilai.tipe_ujian, ViewNilai.nilai, ViewNilai.grade, ViewNilai.nama, ViewNilai.jenis_kelamin, ViewNilai.nama_kelas, ViewNilai.nama_mapel, ViewNilai.kkm, ViewNilai.nama_sekolah, ViewNilai.kota, ViewNilai.kecamatan, ViewNilai.kdpos, ViewNilai.Expr1, ViewNilai.alamat, ViewNilai.logo FROM ProjectPengolahanNilai.dbo.ViewNilai ViewNilai where ViewNilai.tanggal = '" + tglujian.ToString("MM-dd-yyyy") + "'";
            }
            else
            {
                k.sql = "select siswa.nis,nama,kd_nilai,nama_mapel,tanggal,jenis,tipe_ujian,nilai,grade from nilai inner join siswa on nilai.nis = siswa.nis inner join mapel on nilai.kd_mapel = mapel.kd_mapel";
                k.setdt();
            }
            dg_cetaknilai.ItemsSource = k.dt.DefaultView;
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            dg_cetaknilai.ItemsSource = null;
            cmb_kelas.Text = "";
            cmb_mapel.Text = "";
            dp_tglul.Text = "";
            kueri = "";
        }

        private void btn_cetak_Click(object sender, RoutedEventArgs e)
        {
            if (kueri != "")
            {
                k.sql = kueri;
                k.setdt();

                var ctk = new CetakNilai();
                ctk.SetDataSource((DataTable)k.dt);

                var wctk = new w_Cetak();
                wctk.ReportViewer1.ViewerCore.ReportSource = ctk;
                wctk.ShowDialog();
            }
            else
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Nilai Belum Ditampilkan" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");
            }
            
        }
    }
}
