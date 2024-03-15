using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for set_smenaForm.xaml
    /// </summary>
    public partial class set_smenaForm : Window
    {
        public set_smenaForm()
        {
            InitializeComponent();
        }
        user149_dbEntities db = new user149_dbEntities();
        private void set_but_Click(object sender, RoutedEventArgs e)
        {
            Sotrudniki_Smena_KAFE sotrudniki_Smena_KAFE = new Sotrudniki_Smena_KAFE();

            DateTime selectedDate;
            if (DateTime.TryParseExact(smena_combo.Text, "M.d.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
            {
                // Успешное преобразование - обновление текста в ComboBox с нужным форматом
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");
                
                // Продолжаем сравнение с базой данных или выполнение других операций
                var selectSmena = db.Smena_KAFE.Select(w => w.id_smeni).FirstOrDefault();
                sotrudniki_Smena_KAFE.id = 11;

                //var selectSmena = db.Smena_KAFE.Where(w => w.Data == selectedDate).Select(w => w.id_smeni);
                if (selectSmena != null)
                {
                    sotrudniki_Smena_KAFE.id_smeni = Convert.ToInt32(selectSmena);
                }
                else { MessageBox.Show("Выберите дату смены"); }
            }
            else { MessageBox.Show("неверный формат даты");return; }
            
            var selectUser = db.User_KAFE.Where(w => w.id_user == Convert.ToInt32(id_fire_txt.Text));
            if(selectUser != null) { 
            sotrudniki_Smena_KAFE.id_user =Convert.ToInt32( id_fire_txt.Text);
            }
            else { MessageBox.Show("Такого сотрудника не существует"); }
            db.Sotrudniki_Smena_KAFE.Add(sotrudniki_Smena_KAFE);
            db.SaveChanges();
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_txt.Text.Length > 0)
            {
                var search = db.User_KAFE.Where(w => w.Familiya.Contains(search_txt.Text) || w.Familiya == search_txt.Text);
                no_smena.ItemsSource = search.ToList();

            }
            else { no_smena.ItemsSource = db.User_KAFE.ToList(); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            var item1 = db.Smena_KAFE.Select(w => w.Data).ToList();
            foreach (var item in item1)
            {
                smena_combo.Items.Add(item);
            }
            no_smena.ItemsSource = db.User_KAFE.ToList();
        }

        private void id_fire_txt_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
