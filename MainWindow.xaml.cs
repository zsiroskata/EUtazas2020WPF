using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EUtazas2020
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Utazas> utazasok = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new(
                path: @"..\..\..\src\utasadat.txt",
                encoding: Encoding.UTF8
                );
            while (!sr.EndOfStream)
            {
                utazasok.Add(new Utazas(sr.ReadLine()));
            }
            sr.Close();

            foreach (var item in utazasok)
            {
                if (!megalloCb.Items.Contains(item.Sorszam))
                {
                    megalloCb.Items.Add(item.Sorszam);
                }
            }

            foreach (var item in utazasok)
            {
                if (!jegyCb.Items.Contains(item.JegyTipus))
                {
                    jegyCb.Items.Add(item.JegyTipus);
                }
            }

        }

        private void IDTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            IDTxt.MaxLength = 7;
        }

        private void jegy_Checked(object sender, RoutedEventArgs e)
        {

            if (berlet.IsChecked == true)
            {
                jegyBerlet.Content = "Bérlet";
            }
            else
            {
                jegyBerlet.Content = "Jegy";
            }
        }
    }
}