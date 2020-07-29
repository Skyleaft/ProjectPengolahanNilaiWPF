using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.MahApps;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.ComponentModel;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for form_login.xaml
    /// </summary>
    public partial class form_login : MetroWindow
    {
        public form_login()
        {
            InitializeComponent();
        }
        koneksi k = new koneksi();
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }

        private void keluar(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void sumputin()
        {
            this.Closing -= MetroWindow_Closing;
            this.Closing += keluar;
            Close();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String nip = "";
            String cekhak = "";
            k.sql = "select *from akun inner join pegawai on akun.nip=pegawai.nip where username='" + txt_username.Text + "' and password='" + txt_password.Password + "'";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            foreach (DataRow baris in k.dt.Rows)
            {
                nip = baris[2].ToString();
                cekhak = baris[3].ToString();
            }
            
            
            if (txt_username.Text == "" && txt_password.Password == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username dan Password Belum Di isi" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
            }
            else if (cekbaris == 0)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username atau Password Salah" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
                txt_username.Text = "";
                txt_password.Password = "";
            }
            else
            {
                k.sql = "select * from session order by session_id asc";
                k.setdt();
                int cek = k.dt.Rows.Count;
                String sesid, data;
                int tambah;
                DateTime dtime = DateTime.Now;

                if (cek == 0)
                {
                    sesid = dtime.ToString("ddHHmm") + "01";
                }
                else
                {
                    data = k.dt.Rows[cekbaris - 1][0].ToString();
                    tambah = Convert.ToInt32(data.Substring(data.Length - 2, 2)) + 1;
                    if (tambah < 10)
                    {
                        sesid = dtime.ToString("ddHHmm") + "0" + tambah;
                    }
                    else
                    {
                        sesid = dtime.ToString("ddHHmm") + tambah;
                    }
                }
                form_menu fm = new form_menu(nip, sesid);
                if (cekhak == "Admin")
                {
                    k.sql = "insert into session values('" + sesid + "','" + nip + "')";
                    k.setdt();
                    fm.Show();
                    sumputin();

                }
                else if (cekhak == "Staf")
                {
                    
                    k.sql = "insert into session values('" + sesid + "','" + nip + "')";
                    k.setdt();
                    fm.tile_pegawai.IsEnabled = false;
                    fm.tile_sekolah.IsEnabled = false;
                    fm.tile_inputnl.IsEnabled = false;
                    fm.Show();
                    sumputin();
                }
                else if(cekhak=="Guru")
                {
                    k.sql = "insert into session values('" + sesid + "','" + nip + "')";
                    k.setdt();
                    fm.tile_pegawai.IsEnabled = false;
                    fm.tile_siswa.IsEnabled = false;
                    fm.tile_sekolah.IsEnabled = false;
                    fm.tile_kelas.IsEnabled = false;
                    fm.tile_mapel.IsEnabled = false;
                    fm.tile_laporan.IsEnabled = false;
                    fm.Show();
                    sumputin();
                }
                else
                {
                    k.sql = "insert into session values('" + sesid + "','" + nip + "')";
                    k.setdt();
                    fm.Show();
                    sumputin();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            txt_username.Text = "";
            txt_password.Password = "";
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {          
            e.Cancel = true;
            var keluar = new SampleMessageYesNo
            {
                Message = { Text = "Yakin Keluar Aplikasi?" }
            };
            DialogHost.Show(keluar, "LoginDialog");
        }

    }
}
