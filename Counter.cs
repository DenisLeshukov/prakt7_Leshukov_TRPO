using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prakt7_Leshukov
{
    public class Counter: INotifyPropertyChanged
    {
        public string doctors;
        public string pacients;

        public string Doctors
        {
            get => doctors;
            set { doctors = value; OnPropertyChanged(); }
        }
        public string Pacients
        {
            get => pacients;
            set { pacients = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
