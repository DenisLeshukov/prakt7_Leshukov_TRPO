using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prakt7_Leshukov
{
    public class Doctor: INotifyPropertyChanged
    {
        private string? correct_password;
        [JsonIgnore]

        private string? id;
        [JsonIgnore]

        private string name;
        private string lastName;
        private string middleName;
        private string specialization;
        private string password;

        [JsonIgnore]
        public string? Correct_Password
        {
            get => correct_password;
            set
            {
                correct_password = value;
                OnPropertyChanged( );
            }
        }

        public string? Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged( );
            } 
        } 
        public string Name
        {
            get => name;
            set { name = value;
                OnPropertyChanged( );
            }
        }
        public string LastName
        {
            get => lastName;
            set { lastName = value;
                OnPropertyChanged( );
            }
        }
        public string MiddleName
        {
            get => middleName;
            set { middleName = value;
                OnPropertyChanged( );
            }
        }

        public string Specialization
        {
            get => specialization;
            set { specialization = value;
                OnPropertyChanged( );
            }
        }
        public string Password
        {
            get => password;
            set { password = value;
                OnPropertyChanged( );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged ([CallerMemberName] string? propName =
        null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
