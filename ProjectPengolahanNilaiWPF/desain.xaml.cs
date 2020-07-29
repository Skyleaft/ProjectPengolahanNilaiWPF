using MaterialDesignColors;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectPengolahanNilaiWPF
{
    /// <summary>
    /// Interaction logic for desain.xaml
    /// </summary>
    public partial class desain : Window
    {
        public desain()
        {
            InitializeComponent();
            
        }


        private async void btn_test_ClickAsync(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "username atau password salah" }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
            
        }

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("red");

        }
    }
}
