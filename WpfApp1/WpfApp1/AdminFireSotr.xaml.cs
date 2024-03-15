using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AdminFireSotr.xaml
    /// </summary>
    public partial class AdminFireSotr : Window
    {
        public AdminFireSotr()
        {
            InitializeComponent();
        }
        user149_dbEntities db = new user149_dbEntities();
        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(search_txt.Text.Length > 0)
            {
                var search = db.User_KAFE.Where(w=>w.Familiya.Contains(search_txt.Text)|| w.Familiya == search_txt.Text || w.Imya.Contains(search_txt.Text) || w.Imya == search_txt.Text || w.Otchestvo.Contains(search_txt.Text) || w.Otchestvo == search_txt.Text);
                sotr_datagrid.ItemsSource = search.ToList();

            }
            else { sotr_datagrid.ItemsSource = db.User_KAFE.ToList(); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sotr_datagrid.ItemsSource = db.User_KAFE.ToList();
        }

        private void fire_but_Click(object sender, RoutedEventArgs e)
        {
            if(id_fire_txt.Text.Length > 0)
            {
                int num = Convert.ToInt32(id_fire_txt.Text);
                var fireSotr = db.User_KAFE.Where(w=>w.id_user == num).SingleOrDefault();
                if( fireSotr.Role != 1) { 
                fireSotr.id_status = 1;
                }
                else { MessageBox.Show("Сотрудник уже уволен"); return; }
                db.SaveChanges();
                sotr_datagrid.ItemsSource = db.User_KAFE.ToList();
                MessageBox.Show("Сотрудник уволен");
            }
            else { MessageBox.Show("Поле ID пустое"); }
           
        }

        private void id_fire_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            
        }
    }
}
