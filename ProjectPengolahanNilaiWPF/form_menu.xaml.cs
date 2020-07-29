using MahApps.Metro.Controls;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for form_menu.xaml
    /// </summary>
    public partial class form_menu : MetroWindow
    {
        koneksi k = new koneksi();
        private string sessionId;
        public form_menu(string nip,string _sessionid)
        {
            InitializeComponent();
            //load nama user
            //-----------------------------------
            k.sql = "select *from pegawai inner join akun on pegawai.nip = akun.nip where pegawai.nip='" + nip + "'";
            k.setdt();
            string cekhak = "";
            Byte[] tmpfoto = new Byte[0];
            foreach (DataRow baris in k.dt.Rows)
            {
                
                txt_namausr.Text = baris[1].ToString();
                txt_nipusr.Text = baris[0].ToString();
                txt_hakusr.Text = baris[13].ToString();
                tmpfoto = (Byte[])baris[9];
                fotousr.ImageSource = ByteImageConverter.ByteToImage(tmpfoto);
            }
            txt_namausr2.Text = txt_namausr.Text;
            txt_nipusr2.Text = txt_nipusr.Text;
            txt_hakusr2.Text = txt_hakusr.Text;
            fotousr2.ImageSource = fotousr.ImageSource;

            sessionId = _sessionid;

            //timer-------------
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
            
            timer.Interval = TimeSpan.FromMilliseconds(2500);
            timer.Tick += timer_refreshData;
            timer.Start();

            //---------

            //Digital Clock-------------
            DispatcherTimer clock = new DispatcherTimer(DispatcherPriority.Render);
            clock.Interval = TimeSpan.FromSeconds(1);
            clock.Tick += digitalClockTick;
            clock.Start();

            //---------


        }

        void digitalClockTick(object sender, EventArgs e)
        {
            digital_clock.Text=DateTime.Now.ToString("HH:mm:ss");
            digital_date.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Clock.Time = DateTime.Now;
            date.SelectedDate = DateTime.Now;
            digital_clock2.Text = DateTime.Now.ToString("HH:mm:ss");
            digital_date2.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        void timer_refreshData(object sender, EventArgs e)
        {
            k.sql = "select COUNT(nip) from pegawai";
            k.setdt();
            tile_jmlpeg.Count = k.dt.Rows[0][0].ToString();
            tile_jmlpeg2.Count = tile_jmlpeg.Count;

            k.sql = "select COUNT(nis) from siswa";
            k.setdt();
            tile_jmlsis.Count = k.dt.Rows[0][0].ToString();
            tile_jmlsis2.Count = tile_jmlsis.Count;

            k.sql = "select COUNT(kd_nilai) from nilai";
            k.setdt();
            tile_jmlnl.Count = k.dt.Rows[0][0].ToString();
            tile_jmlnl2.Count = tile_jmlnl.Count;

            k.sql = "select COUNT(session_id) from session";
            k.setdt();
            tile_online.Count = k.dt.Rows[0][0].ToString();
            tile_online2.Count = tile_online.Count;

            batteryPercent.Text = HardwareInfo.GetBaterylevel() + "%";
            batteryPercent2.Text = batteryPercent.Text;
            string cas = HardwareInfo.GetBaterycharge();
            if (cas == "2")
            {
                charge_icon.Kind = MahApps.Metro.IconPacks.PackIconModernKind.BatteryCharging;
                charge_icon2.Kind = charge_icon.Kind;
            }
            else
            {
                charge_icon.Kind = MahApps.Metro.IconPacks.PackIconModernKind.Battery0;
                charge_icon2.Kind = charge_icon.Kind;
            }
        }

       

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }

        private void btn_pegawai_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Data Pegawai";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.AccountMultiple;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var fp = new c_pegawai();
            main_content.Content = fp;
            
        }

        private void btn_slideback_Click(object sender, RoutedEventArgs e)
        {
            
            txt_title.Text = "Dasboard";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ViewDashboard;
            btn_slideback.Visibility = Visibility.Hidden;
            btn_slidedown.Visibility = Visibility.Hidden;
            Storyboard sb = this.FindResource("SlideBack") as Storyboard;
            sb.Begin();
        }

        private void test_btn_Click(object sender, RoutedEventArgs e)
        {
            //var fp = new UserControl1();
            //main_content.Content = fp;
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from session where session_id='" + sessionId + "'";
            k.setdt();
            form_login fl = new form_login();
            fl.Show();
            sumputin();
        }

        private void btn_siswa_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Data Siswa";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.AccountMultiple;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var fp = new c_siswa();
            main_content.Content = fp;
        }


        private void btn_mapel_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Data Mata Pelajaran";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Book;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var mp = new c_mapel();
            main_content.Content = mp;
        }

        private void btn_kelas_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Data Kelas";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChairSchool;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var kls = new c_kelas();
            main_content.Content = kls;
        }

        private void btn_inptnilai_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Input Nilai";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.BookOpen;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var inpt = new c_inputnilai(txt_nipusr.Text);
            main_content.Content = inpt;
        }

        private void tile_laporan_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Laporan";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.FileDocument;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var lprn = new c_laporan();
            main_content.Content = lprn;
        }

        private void tile_sekolah_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Data Sekolah";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.School;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var ds = new c_sekolah();
            main_content.Content = ds;
        }

        private void keluar(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void sumputin()
        {
            this.Closing -= window_Closing;
            this.Closing += keluar;
            Close();
        }

        private void btn_slidedown_Click(object sender, RoutedEventArgs e)
        {
            fy_header.IsOpen = true;
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var keluar = new SampleMessageYesNo
            {
                Message = { Text = "Yakin Keluar Aplikasi?" }
            };
            DialogHost.Show(keluar, "MainDialog");
        }

        private void btn_slideup_Click(object sender, RoutedEventArgs e)
        {
            fy_botom.IsOpen = true;
        }

        private void tile_rekap_Click(object sender, RoutedEventArgs e)
        {
            btn_slideback.Visibility = Visibility.Visible;
            btn_slidedown.Visibility = Visibility.Visible;
            txt_title.Text = "Rekap Nilai";
            icon_title.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.TooltipEdit;
            Storyboard sb = this.FindResource("SlideOpen") as Storyboard;
            sb.Begin();
            main_content.Content = null;
            var ds = new c_tampilN();
            main_content.Content = ds;
        }
    }
}
