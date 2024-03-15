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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminSetSmena.xaml
    /// </summary>
    public partial class AdminSetSmena : Window
    {
        public AdminSetSmena()
        {
            InitializeComponent();
        }
        user149_dbEntities db = new user149_dbEntities();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sotr_datagrid.ItemsSource = db.Sotrudniki_Smena_KAFE.ToList();
        }

        private void set_smena_Click(object sender, RoutedEventArgs e)
        {
            set_smenaForm set_SmenaForm = new set_smenaForm();
            set_SmenaForm.ShowDialog();
        }
    }
}
