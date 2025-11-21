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
        private Doctor RegisterUser = new Doctor();
        private Doctor SignIn_User = new Doctor();

        private Patient New_patient = new Patient();
        private Patient patient = new Patient();

        private Counter count_users = new Counter();

        public MainWindow ()
        {

            InitializeComponent( );
            Register_patient.DataContext = New_patient;
            reg.DataContext = RegisterUser;
            sign.DataContext = SignIn_User;
            find_patient.DataContext = patient;
            patient_appointment.DataContext = patient;
            change.DataContext = patient;

            counter.DataContext = count_users;

            Count();
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
            var currentUser = RegisterUser;
            currentUser.Name = "";
            currentUser.LastName = "";
            currentUser.MiddleName = "";
            currentUser.Password = "";
            currentUser.Specialization = "";
        }

        private void Sign_in(object sender, RoutedEventArgs e)
        {
            
            int indent = Convert.ToInt32(id.Text.ToString());
                
            if(File.Exists($"D_{indent.ToString().PadLeft(5, '0')}.json"))
            {
                string file = $"D_{indent.ToString().PadLeft(5, '0')}.json";
                string json = File.ReadAllText(file);
                SignIn_User = JsonSerializer.Deserialize<Doctor>(json);
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
                MessageBox.Show("Такого доктора нет");
            }
        }
            
        

        private void Reg_Patient (object sender, RoutedEventArgs e)
        {
            try
            {
                if (SignIn_User.Name != null)
                {
                    var pacient = New_patient;
                    if (New_patient.Name != null && New_patient.LastName != null && New_patient.MiddleName != null && New_patient.Birthday != null && New_patient.Recomendation != null && New_patient.Diagnos != null)
                    {
                        int i = 0;
                        while (true)
                        {
                            if (File.Exists($"D_{i.ToString().PadLeft(7, '0')}.json"))
                            {
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        pacient.Id = $"{i}";
                        pacient.LastDoctor = SignIn_User.Id;
                        pacient.LastAppointment = DateTime.Now.ToString();

                        string jsonString = JsonSerializer.Serialize(pacient);

                        string fileName = $"D_{i.ToString().PadLeft(7, '0')}.json";

                        File.WriteAllText(fileName, jsonString);

                        MessageBox.Show($"Ваш индентификатор={i.ToString().PadLeft(7, '0')}");

                        pacient.Name = "";
                        pacient.LastName = "";
                        pacient.MiddleName = "";
                        pacient.Birthday = "";
                        pacient.Recomendation = "";
                        pacient.Diagnos = "";
                    }
                    else
                    {
                        MessageBox.Show("Заполните пустые поля");
                    }
                }
                else
                {
                    MessageBox.Show("Войдите как доктор");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void find(object sender, RoutedEventArgs e)
        {
            
            if (patient.Id != null && patient.Id != null)
            {
                if (SignIn_User.Name != null)
                {
                    int indent = Convert.ToInt32(patient.Id);
                    string fileName = $"D_{indent.ToString().PadLeft(7, '0')}.json";
                    if (File.Exists(fileName))
                    {
                        string jsonString = File.ReadAllText(fileName);
                        patient = JsonSerializer.Deserialize<Patient>(jsonString)!;
                        if (patient != null)
                        {
                            change.DataContext = patient;
                            patient_appointment.DataContext = patient;
                            patient.Id = $"{indent}";
                            fileName = $"D_{patient.LastDoctor.ToString().PadLeft(5, '0')}.json";
                            jsonString = File.ReadAllText(fileName);
                            Doctor BDoc = JsonSerializer.Deserialize<Doctor>(jsonString)!;
                            patient.LastDoctor = $"{BDoc.Name} {BDoc.LastName} {BDoc.MiddleName}";

                        }
                        else
                        {
                            MessageBox.Show("Пусто");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Такого пациента нет");
                    }
                    find_patient.DataContext = patient;
                }
                else
                {
                    MessageBox.Show("Доктор не найден - войдите как доктор");
                }
            }
            else
            {
                MessageBox.Show("Заполните пустые поля");
            }
        }
           
        

        private void Save_appointment(object sender, RoutedEventArgs e)
        {
            
            if (SignIn_User.Name != null)
            {
                var save_pacient = patient;
                if (patient.Name != null && patient.LastName != null && patient.MiddleName != null && patient.Birthday != null && patient.Recomendation != null && patient.Diagnos != null)
                {
                    int indent = Convert.ToInt32(patient.Id);
                    save_pacient.LastDoctor = SignIn_User.Id;
                    save_pacient.LastAppointment = DateTime.Now.ToString();

                    string jsonString = JsonSerializer.Serialize(save_pacient);

                    string fileName = $"D_{indent.ToString().PadLeft(7, '0')}.json";
                    File.WriteAllText(fileName, jsonString);
                    MessageBox.Show($"Ваш ID={indent.ToString().PadLeft(7, '0')}");

                    save_pacient.Name = "";
                    save_pacient.LastName = "";
                    save_pacient.MiddleName = "";
                    save_pacient.Birthday = "";
                    save_pacient.LastAppointment = "";
                    save_pacient.LastDoctor = "";
                    save_pacient.Recomendation = "";
                    save_pacient.Diagnos = "";

                    patient.Id = "";
                }
                else
                {
                    MessageBox.Show("Заполните пустые поля");
                }
            }
            else
            {
                MessageBox.Show("Доктор не найден - войдите как доктор");
            }
        }
           
        

        private void Reset(object sender, RoutedEventArgs e)
        {
            if (patient.Name != null)
            {
                int indent = Convert.ToInt32(patient.Id);

                string fileName = $"D_{indent.ToString().PadLeft(7, '0')}.json";
                string jsonString = File.ReadAllText(fileName);
                var Reset_patient = JsonSerializer.Deserialize<Patient>(jsonString)!;

                patient.Name = Reset_patient.Name;
                patient.LastName = Reset_patient.LastName;
                patient.MiddleName = Reset_patient.MiddleName;
                patient.Birthday = Reset_patient.Birthday;
                
            }
            else
            {
                MessageBox.Show("Пациент не найден");
            }
        }

        private void Count()
        {
            int indent = 0;
            while (true)
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
            count_users.Doctors = indent.ToString();
            indent = 0;
            while (true)
            {
                if (File.Exists($"D_{indent.ToString().PadLeft(7, '0')}.json"))
                {
                    indent++;
                }
                else
                {
                    break;
                }
            }
            count_users.Pacients = indent.ToString();
            counter.DataContext = count_users;
        }


    }
}