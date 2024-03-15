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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateCaptcha();
            SPanelSymbols.Visibility = Visibility.Hidden; CanvasNoise.Visibility = Visibility.Hidden; captcha_txt.Visibility = Visibility.Hidden;

        }
        user149_dbEntities db = new user149_dbEntities();
        private int activationCount = 0;
        Random _random = new Random();
        public Button ButtonLogin { get { return enter_but; } }
        DispatcherTimer _timerEn = new DispatcherTimer();
        string captcha_txt_HIDDEN = "";
        int WrongCount;
        private void enter_but_Click(object sender, RoutedEventArgs e)
        {
            if (WrongCount > 0)
            {
                if(WrongCount == 3)
                {
                    HandleReopening();
                    
                    MessageBox.Show("Вход заблокирован");
                }
                var allRecMod = db.User_KAFE.ToList();
                var allRecAdm = db.Admin_KAFE.ToList();


                var userMod = allRecMod.SingleOrDefault(w => w.Login == log_textbox.Text && w.Password == pass_textbox.Password);
                var userAdm = allRecAdm.SingleOrDefault(w => w.Login == log_textbox.Text && w.Password == pass_textbox.Password);
                if (log_textbox.Text != "" && pass_textbox.Password != "")
                {
                    if (userMod?.Role == 1 && userAdm?.Login == null)
                    {
                        if (userMod?.Login == log_textbox.Text && userMod?.Password == pass_textbox.Password)
                        {
                            if (captcha_txt.Text == captcha_txt_HIDDEN)
                            {
                                MessageBox.Show("Вы успешно вошли как Официант");
                                Oficiant oficiant = new Oficiant();
                                this.Close();
                                oficiant.Show();
                                return;
                            }
                            else { MessageBox.Show("Неправильная капча"); WrongCount++; UpdateCaptcha(); }

                        }
                        else { MessageBox.Show("Неправильные данные"); WrongCount++; UpdateCaptcha(); }
                    }
                    else if ((userMod?.Role == 2 && userAdm?.Login == null))
                    {
                        if (userMod?.Login == log_textbox.Text && userMod?.Password == pass_textbox.Password)
                        {
                            if (captcha_txt.Text == captcha_txt_HIDDEN)
                            {
                                MessageBox.Show("Вы успешно вошли как Повар");
                                Povar povar = new Povar();
                                this.Close();
                                povar.Show();
                                return;
                            }
                            else { MessageBox.Show("Неправильная капча"); WrongCount++; UpdateCaptcha(); }
                        }
                        else { MessageBox.Show("Неправильные данные"); WrongCount++; UpdateCaptcha(); }
                    }
                    else if (userMod?.Role == null && userAdm?.Login != null)
                    {
                        if (userAdm?.Login == log_textbox.Text && userAdm?.Password == pass_textbox.Password)
                        {
                            if (captcha_txt.Text == captcha_txt_HIDDEN)
                            {
                                MessageBox.Show("Вы успешно вошли как Админ");
                                Admin admin = new Admin();
                                this.Close();
                                admin.Show();
                                return;
                            }
                            else { MessageBox.Show("Неправильная капча"); WrongCount++; UpdateCaptcha(); }
                        }
                        else { MessageBox.Show("Неправильные данные"); WrongCount++; UpdateCaptcha(); }

                    }
                    else { if (userMod == null && userAdm == null) { MessageBox.Show("Неправильные данные"); WrongCount++; Showelem(); } else MessageBox.Show("Логин повторяется"); WrongCount++; }
                }
                else
                {
                    MessageBox.Show("Заполните поля");
                    WrongCount++;
                }
            }
            else { SuccesAuth(); }
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void HandleReopening()
        {
            if (enter_but.IsEnabled == true)
            {
                _timerEn.Tick += new EventHandler(WaitingEventEn);
                _timerEn.Interval = new TimeSpan(0, 0, 6);
                _timerEn.Start();
                enter_but.IsEnabled = false;
            }
        }
        void SuccesAuth()
        {
            

                var allRecMod = db.User_KAFE.ToList();
                var allRecAdm = db.Admin_KAFE.ToList();


                var userMod = allRecMod.SingleOrDefault(w => w.Login == log_textbox.Text && w.Password == pass_textbox.Password);
                var userAdm = allRecAdm.SingleOrDefault(w => w.Login == log_textbox.Text && w.Password == pass_textbox.Password);
                if (log_textbox.Text != "" && pass_textbox.Password != "")
                {
                    if ((userMod?.Role == 1 && userAdm?.Login == null))
                    {
                        if (userMod?.Login == log_textbox.Text && userMod?.Password == pass_textbox.Password)
                        {
                            
                                MessageBox.Show("Вы успешно вошли как Официант");
                                Oficiant oficiant = new Oficiant();
                                this.Close();
                                oficiant.Show();
                                return;
                            
                            

                        }
                    }
                    else if (userMod?.Role == 2 && userAdm?.Login == null)
                    {
                        if (userMod?.Login == log_textbox.Text && userMod?.Password == pass_textbox.Password)
                        {
                            
                                MessageBox.Show("Вы успешно вошли как Повар");
                                Povar povar = new Povar();
                                this.Close();
                                povar.Show();
                                return;
                            
                            
                        }
                    }
                    else if (userMod?.Role == null && userAdm?.Login != null)
                    {
                        if (userAdm?.Login == log_textbox.Text && userAdm?.Password == pass_textbox.Password)
                        {
                            
                                MessageBox.Show("Вы успешно вошли как Админ");
                                Admin admin = new Admin();
                                this.Close();
                                admin.Show();
                                return;
                            
                            
                        }

                    }
                    else { if (userMod == null && userAdm == null) { MessageBox.Show("Неправильные данные, введите капчу"); WrongCount++; Showelem(); } else MessageBox.Show("Логин повторяется"); }
                }
                else
                {
                    MessageBox.Show("Заполните поля");
                }
            
            
        }
        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)_random.Next(100, 200),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256)));
                border.Height = _random.Next(2, 10);
                border.Width = _random.Next(10, 400);

                border.RenderTransform = new RotateTransform(_random.Next(0, 360));


                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, _random.Next(0, 300));
                Canvas.SetTop(border, _random.Next(0, 150));





                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)_random.Next(100, 200),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256)));
                ellipse.Height = ellipse.Width = _random.Next(20, 40);

                CanvasNoise.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, _random.Next(0, 300));
                Canvas.SetTop(ellipse, _random.Next(0, 150));
            }
        }
        private void GenerateSymbols(int count)
        {
            string alphabet = "WERPASFHKXVBM234578";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(_random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol;
                captcha_txt_HIDDEN += symbol;
                lbl.FontSize = _random.Next(40, 80);
                lbl.RenderTransform = new RotateTransform(_random.Next(-45, 45));
                lbl.Margin = new Thickness(20, 0, 20, 0);

                //lbl.Foreground = ra

                SPanelSymbols.Children.Add(lbl);


            }
        }
            void Showelem()
            {
                SPanelSymbols.Visibility = Visibility.Visible; CanvasNoise.Visibility = Visibility.Visible; captcha_txt.Visibility = Visibility.Visible;
            }
            
            private void UpdateCaptcha()
            {
                SPanelSymbols.Children.Clear();
                CanvasNoise.Children.Clear();
                captcha_txt_HIDDEN = "";
                GenerateSymbols(4);

                GenerateNoise(30);

            }


            private void WaitingEventEn(object Source, EventArgs e)
            {
            enter_but.IsEnabled = true;
            MessageBox.Show("Вы можете войти");
            _timerEn.Stop();
            UpdateCaptcha(); //запускаем обновление капчи
        }
        }
    
}


