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
    /// Interaction logic for AdminAddForm.xaml
    /// </summary>
    public partial class AdminAddForm : Window
    {
        public AdminAddForm()
        {
            InitializeComponent();
        }
        user149_dbEntities db = new user149_dbEntities();
        private void addUser_but_Click(object sender, RoutedEventArgs e)
        {
            if (Fam_txt.Text != "" && Imya_txt.Text != "" && otch_txt.Text != "" && role_cmbx.Text != "" && tele_txt.Text != "" && log_txt.Text != "" && pass_txt.Text != "" && status_cmbx.Text != "")
            {
                User_KAFE user_KAFE = new User_KAFE();
                user_KAFE.Familiya = Fam_txt.Text;
                user_KAFE.Imya = Imya_txt.Text;
                user_KAFE.Otchestvo = otch_txt.Text;
                var checkRole = db.Role_KAFE.Where(w => w.Nazvanie == role_cmbx.Text).Select(w => w.id_role).FirstOrDefault();
                user_KAFE.Role = checkRole;
                user_KAFE.Telephone = tele_txt.Text;
                user_KAFE.Login = log_txt.Text;
                user_KAFE.Password= pass_txt.Text;
                var checkStatus = db.Status_KAFE.Where(w => w.Status == status_cmbx.Text).Select(w => w.id_status).FirstOrDefault();
                user_KAFE.id_status = checkStatus;
                db.User_KAFE.Add(user_KAFE);
                db.SaveChanges();
                MessageBox.Show("Новый пользователь добавлен", "Успех");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var itemO = db.Status_KAFE.Select(w => w.Status).ToList();
            status_cmbx.ItemsSource = itemO;
            status_cmbx.Name = "Status";
            var item1 = db.Role_KAFE.Select(w => w.Nazvanie).ToList();
            role_cmbx.ItemsSource = item1;
            role_cmbx.Name = "Nazvanie";
        }
    }
}
