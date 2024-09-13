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

        }

        private void jegy_Checked(object sender, RoutedEventArgs e)
        {

            if (berlet.IsChecked == true)
            {
                jegyBerlet.Content = "Bérlet típusa";
            }
            else if (jegy.IsChecked == true)
            {
                jegyBerlet.Content = "Jegy típusa";
            }
            else
            {
                MessageBox.Show("Nem választottad ki, hogy milyen tipusú jegyed van!");
            }
            
        }

        private void gomb_Click(object sender, RoutedEventArgs e)
        {
            jegy_Checked(sender, e);
            if (megalloCb.SelectedItem== null)
            {
                MessageBox.Show("Nincs kiválasztva a megálló!");
            }



            //azonosító 
            int idSzam;
            bool sikeres = int.TryParse(IDTxt.Text, out idSzam);
            if (sikeres)
            {
                if (idSzam < 0)
                {
                    MessageBox.Show("kártya azonosítója nem pozitív egész szám!");
                }
            }
            else
            {
                
                MessageBox.Show("Nincs be írva az azonosító!");
            }
            IDTxt.MaxLength = 7;

            if (IDTxt.Text.Length < 7)
            {
                MessageBox.Show("Nem 7 karakterből áll az azonosító!");
            }
          
            

        }
        private void IdoTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ido =  IdoTxt.Text.Split(":") ;
            int ora = Convert.ToInt32( ido[0]);
            int perc = Convert.ToInt32(ido[1]);

            if (ora < 23 && ora > 0)
            {
                MessageBox.Show("ilyen idő nem létezik");
            }
            else if(perc < 59 )
            {

            }

        }

    }
}