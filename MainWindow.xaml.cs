using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt7_Leshukov
{
    
    public partial class MainWindow :Window
    {
        private User RegisterUser = new User();
        private User SignIn_User = new User();
        public MainWindow ()
        {
            InitializeComponent( );
            reg.DataContext = RegisterUser;
            sign.DataContext = SignIn_User;
        }

        private void Reg_User (object sender, RoutedEventArgs e)
        {
            try
            {
                var regUser = RegisterUser;
                if (regUser.Name != null && regUser.LastName != null && regUser.MiddleName != null && regUser.Password != null && regUser.Correct_Password != null)
                {
                    if (regUser.Password == regUser.Correct_Password)
                    {
                        int indent = 0;
                        while(true)
                        {

                        
                            if (File.Exists($"D_{indent.ToString().PadLeft(5, '0')}.json"))
                            {
                                indent++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        regUser.Id = $"{indent}";
                        string json = JsonSerializer.Serialize(regUser);
                        string file = $"D_{indent.ToString().PadLeft(5, '0')}.json";
                        File.WriteAllText(file, json);
                        MessageBox.Show($"Вы зарегестрировались, ваш айди: {indent.ToString().PadLeft(5, '0')}");
                        regUser.Name = "";
                        regUser.LastName = "";
                        regUser.MiddleName = "";
                        regUser.Password = "";
                        regUser.Specialization = "";
                        regUser.Correct_Password = "";

                    }
                    else
                    {
                        MessageBox.Show("Пароль не подтвержден");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
            catch
            {

            }
        }

        private void Reset_Reg (object sender, RoutedEventArgs e)
        {

        }

        private void Sign_in(object sender, RoutedEventArgs e)
        {
            try
            {
                int indent = Convert.ToInt32(id.Text.ToString());
                
                if(File.Exists($"D_{indent.ToString().PadLeft(5, '0')}.json"))
                {
                    string file = $"D_{indent.ToString().PadLeft(5, '0')}.json";
                    string json = File.ReadAllText(file);
                    SignIn_User = JsonSerializer.Deserialize<User>(json);
                    if(SignIn_User != null)
                    {
                        if(SignIn_User.Password == correct.Text.ToString())
                        {
                            sign.DataContext = SignIn_User;
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Файл пуст");
                    }
                }
                else
                {
                    MessageBox.Show("Такого ID не существует");
                }
            }
            catch
            {

            }
        }

        private void Reg_Patient (object sender, RoutedEventArgs e)
        {

        }
    }
}