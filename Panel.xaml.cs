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
using System.Windows.Shapes;

namespace SQL
{
    /// <summary>
    /// Logika interakcji dla klasy Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        User czytelnik;
        public Panel(User user)
        {
            czytelnik = user;
            InitializeComponent();
            LocalDBEntitiesAuto SqlKsiegi = new LocalDBEntitiesAuto(); 
            var logKsiegi = from d in SqlKsiegi.Samochod
                            where d.Status.Trim() == "NaStanie"

                            select d;

            this.ListaKsiag.ItemsSource = logKsiegi.ToList();


            LocalDBEntitiesAuto Sqlw = new LocalDBEntitiesAuto();
            var logw = from d in Sqlw.Wypozyczenie

                       select d;

            this.listaw.ItemsSource = logw.ToList();
        }
        private void wyp_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListaKsiag.SelectedIndex >= 0)
            {
                if (this.ListaKsiag.SelectedItems.Count >= 0)
                {

                    LocalDBEntitiesAuto Sqlwyp = new LocalDBEntitiesAuto();
                    DateTime thisDay = DateTime.Today;
                    Samochod z = (Samochod)this.ListaKsiag.SelectedItems[0];

                    Wypozyczenie Objectsql = new Wypozyczenie { Data_Wyd = thisDay, Termin_Od = thisDay.AddDays(7), id_osoby = czytelnik.Id, id_samochodu = z.Id_Samochodu };

                    Sqlwyp.Wypozyczenie.Add(Objectsql);
                    Sqlwyp.SaveChangesAsync();

                    LocalDBEntitiesAuto Sqlwypk = new LocalDBEntitiesAuto(); 

                    var r = from d in Sqlwypk.Samochod
                            where d.Id_Samochodu == Objectsql.id_samochodu
                            select d;
                    Samochod k = r.SingleOrDefault();

                    k.Status = "Brak"; //zmiana statusu na brak, nie wyswietli sie na liscie do wypozyczenia

                    Sqlwypk.SaveChanges();


                }
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //usuniecie z listy wypozyczonych samochodow
        private void Odaj_Click(object sender, RoutedEventArgs e)
        {
            if (this.listaw.SelectedIndex >= 0)
            {
                if (this.listaw.SelectedItems.Count >= 0)
                {
                    Wypozyczenie z = (Wypozyczenie)this.listaw.SelectedItems[0];
                    LocalDBEntitiesAuto Sqlodd = new LocalDBEntitiesAuto();
                    var r = from d in Sqlodd.Wypozyczenie
                            where d.Id == z.Id
                            select d;
                    Wypozyczenie w = r.SingleOrDefault();
                    Sqlodd.Wypozyczenie.Remove(w);
                    Sqlodd.SaveChangesAsync();

                    LocalDBEntitiesAuto Sqlwypk = new LocalDBEntitiesAuto();

                    var rt = from d in Sqlwypk.Samochod
                             where d.Id_Samochodu == z.id_samochodu
                             select d;
                    Samochod k = rt.SingleOrDefault();

                    k.Status = "NaStanie";

                    Sqlwypk.SaveChanges();
                }
            }
        }
        private void load_Click(object sender, RoutedEventArgs e)
        {

            LocalDBEntitiesAuto SqlKsiegi = new LocalDBEntitiesAuto();
            var logKsiegi = from d in SqlKsiegi.Samochod
                            where d.Status.Trim() == "NaStanie"

                            select d;

            this.ListaKsiag.ItemsSource = logKsiegi.ToList();


            LocalDBEntitiesAuto Sqlw = new LocalDBEntitiesAuto();
            var logw = from d in Sqlw.Wypozyczenie

                       select d;

            this.listaw.ItemsSource = logw.ToList();
        }


    }
}
