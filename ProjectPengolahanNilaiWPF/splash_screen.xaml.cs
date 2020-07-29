using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for splash_screen.xaml
    /// </summary>
    public partial class splash_screen : MetroWindow
    {

        DispatcherTimer timer = new DispatcherTimer();
        public splash_screen()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_splash;
            timer.Start();
        }
        koneksi k = new koneksi();
        int step = 0;
        void timer_splash(object sender, EventArgs e)
        {
            switch (step)
            {
                case 0:
                    
                    break;
                case 1:
                    timer.Interval = TimeSpan.FromMilliseconds(700);
                    txt_load.Text = "Initializing Modules...........";

                    break;
                case 2:
                    txt_load.Text = "Connecting to Database.......";
                    break;
                case 3:
                    try
                    {
                        k.open();
                        txt_load.Text = "Connection OK....";
                    }
                    catch (Exception ex)
                    {
                        timer.Stop();
                        if (System.Windows.Forms.MessageBox.Show("Koneksi Ke Database Gagal",
                           "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                        {
                            timer.Stop();
                            this.Close();

                        }
                    }
                    timer.Interval = TimeSpan.FromMilliseconds(1200);
                    break;
                case 4:
                    txt_load.Text = "Checking account..........";
                    break;
                case 5:
                    k.sql = "select *from pegawai inner join akun on akun.nip=pegawai.nip";
                    k.setdt();
                    int cekbaris = k.dt.Rows.Count;
                    if (cekbaris == 0)
                    {
                        register_app frreg = new register_app();
                        frreg.Show();
                        timer.Stop();
                        this.Close();
                        
                    }
                    txt_load.Text = "Finalizing........";
                    break;
                case 6:
                    timer.Stop();
                    form_login frlog = new form_login();
                    frlog.Show();
                    this.Close();
                    break;
            }
            step++;
        }

    }
}
