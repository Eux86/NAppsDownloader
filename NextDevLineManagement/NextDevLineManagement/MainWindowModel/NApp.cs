using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDevLineManagement.MainWindowModel
{
    public class NApp : INotifyPropertyChanged
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
        private string _downloadOutput;

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
        public string DownloadOutput
        {
            get { return _downloadOutput; }
            set
            {
                _downloadOutput = value;
                OnPropertyChanged("DownloadOutput");
            }
        }
        public enum StateTypes { Downloading, Finished, Waiting, Error };

        public NApp()
        {
            State = StateTypes.Waiting;
        }
    }
}
