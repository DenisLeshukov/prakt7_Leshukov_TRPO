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
    public class Patient: INotifyPropertyChanged
    {
        private string? id;
        private string name;
        private string lastname;
        private string middlename;
        private string birthday;
        private string lastappointment;
        private string lastdoctor;
        private string diagnos;
        private string recomendation;


        [JsonIgnore]
        public string? Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => lastname;
            set { lastname = value; OnPropertyChanged(); }
        }
        public string MiddleName
        {
            get => middlename;
            set { middlename = value; OnPropertyChanged(); }
        }
        public string Birthday
        {
            get => birthday;
            set { birthday = value; OnPropertyChanged(); }
        }
        public string LastAppointment
        {
            get => lastappointment;
            set { lastappointment = value; OnPropertyChanged(); }
        }
        public string LastDoctor
        {
            get => lastdoctor;
            set { lastdoctor = value; OnPropertyChanged(); }
        }
        public string Diagnos
        {
            get => diagnos;
            set { diagnos = value; OnPropertyChanged(); }
        }
        public string Recomendation
        {
            get => recomendation;
            set { recomendation = value; OnPropertyChanged(); }
        }

        



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
