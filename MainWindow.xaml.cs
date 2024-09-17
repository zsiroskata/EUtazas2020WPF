using System.Diagnostics.Eventing.Reader;
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
            berletAdatok.Visibility = Visibility.Collapsed;
            jegyAdatok.Visibility = Visibility.Collapsed;

        }

        private void jegy_Checked(object sender, RoutedEventArgs e)
        {
            if (berlet.IsChecked == true)
            {
                berletAdatok.Visibility = Visibility.Visible;
               jegyAdatok.Visibility = Visibility.Collapsed;
                jegyBerlet.Content = "Bérlet típusa";
            }
            else if (jegy.IsChecked == true)
            {
                jegyAdatok.Visibility = Visibility.Visible;
                berletAdatok.Visibility = Visibility.Collapsed;
                jegyBerlet.Content = "Jegy típusa";
            }
            else
            {
                MessageBox.Show("Nem választottad ki, hogy milyen tipusú jegyed van!");
            }
            
        }


        private void gomb_Click(object sender, RoutedEventArgs e)
        {
            int hiba = 0;
            jegy_Checked(sender, e);
            if (megalloCb.SelectedItem== null)
            {
                MessageBox.Show("Nincs kiválasztva a megálló!");
                hiba++;
            }

            //azonosító 
            int idSzam;
            bool sikeres = int.TryParse(IDTxt.Text, out idSzam);
            if (sikeres)
            {
                if (idSzam < 0)
                {
                    MessageBox.Show("kártya azonosítója nem pozitív egész szám!");
                    hiba++;
                }
            }
            else
            {
                MessageBox.Show("Nincs be írva az azonosító!");
                hiba++;
            }

            IDTxt.MaxLength = 7;
            if (IDTxt.Text.Length < 7)
            {
                MessageBox.Show("Nem 7 karakterből áll az azonosító!");
                hiba++;
            }

            // I D Ő
            if (string.IsNullOrWhiteSpace(IdoTxt.Text))
            {
                MessageBox.Show("Nem adott meg időt!");
                hiba++;
            }
            if (IdoTxt.Text.Length < 5)
            {
                MessageBox.Show("Helytelen időpont formátum.");
                hiba++;
            }

            // D Á T U M
            if (berletNaptar.SelectedDate == null && berlet.IsChecked == true)
            {
                MessageBox.Show("Nem adta meg a bérlet érvényességi idejét!");
                hiba++;
            }
        }
        private void IdoTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdoTxt.Text.Length ==5)
            {
                if (IdoTxt.Text.Contains(":"))
                {
                    var ido = IdoTxt.Text.Split(':');

                    if (ido.Length == 2 && int.TryParse(ido[0], out int ora) && int.TryParse(ido[1], out int perc))
                    {
                        if ((ora >= 0 && ora <= 23) && (perc >= 0 && perc <= 59))
                        {
                            MessageBox.Show("Helyes időpont.");
                        }
                        else
                        {
                            MessageBox.Show("Helytelen időpont: az időnek 00:00 és 23:59 között kell lennie.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Helytelen időpont formátum. Az időt óó:pp formátumban adja meg!");
                    }
                }
                else
                {
                    MessageBox.Show("Helytelen időpont formátum. Az időt óó:pp formátumban adja meg!");
                }
            }
            else if (IdoTxt.Text.Length > 5)
            {
                    MessageBox.Show("Helytelen időpont formátum.");

            }
        }
        private void jegySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            jegyLabel.Content = $"{(int)jegySlider.Value} db";
        }

    }
}