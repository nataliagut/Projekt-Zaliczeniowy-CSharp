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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQL
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Loguj_Click(object sender, RoutedEventArgs e)
        {
            LocalDBEntitiesAuto SQL = new LocalDBEntitiesAuto();
            var log = from d in SQL.User 
                select d;
            foreach(var item in log)
            {
                if (item.Login.Trim() == TextLogin.Text && item.Haslo.Trim() == TextHaslo.Text)
                {
                    Panel panel = new Panel(item);
                    panel.Show();

                    this.Close();
                }
            }
        }
    }
}
