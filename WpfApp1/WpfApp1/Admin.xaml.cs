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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void add_newUser_Click(object sender, RoutedEventArgs e)
        {
            AdminAddForm adminAddForm = new AdminAddForm();
            adminAddForm.ShowDialog();
        }

        private void showAllOrders(object sender, RoutedEventArgs e)
        {

        }

        private void SetSmena_Click(object sender, RoutedEventArgs e)
        {
            AdminSetSmena admin = new AdminSetSmena();
            admin.ShowDialog();
        }

        private void FireWorker_Click(object sender, RoutedEventArgs e)
        {
            AdminFireSotr adminFireSotr = new AdminFireSotr();
            adminFireSotr.ShowDialog();
        }
    }
}
