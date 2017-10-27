using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDevLineManagement.MainWindowModel
{
    public class NextAppItem : INotifyPropertyChanged
    {
        #region PropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        private string _name;
        private StateTypes _state;
        private string _destinationFolder;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public StateTypes State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public string DestinationFolder
        {
            get { return _destinationFolder; }
            set
            {
                _destinationFolder = value;
                OnPropertyChanged("DestinationFolder");
            }
        }
        public enum StateTypes { Downloading, Finished, Waiting };

        public NextAppItem()
        {
            State = StateTypes.Waiting;
        }
    }
}
