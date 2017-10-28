using NextDevLineManagement.MainWindowModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace NextDevLineManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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

        private string _baseAddress;
        private string _repoFolder;
        private string _installationFolder;
        private int _maxSimultaneousDownloads = 3;


        public string BaseAddress
        {
            get { return _baseAddress; }
            set
            {
                _baseAddress = value;
                OnPropertyChanged("BaseAddress");
            }
        }
        public string Branch
        {
            get { return _repoFolder; }
            set
            {
                _repoFolder = value;
                OnPropertyChanged("RepoFolder");
            }
        }
        public string InstallationFolder
        {
            get { return _installationFolder; }
            set
            {
                _installationFolder = value;
                OnPropertyChanged("InstallationFolder");
            }
        }
        public int MaxSimultaneousDownloads
        {
            get { return _maxSimultaneousDownloads; }
            set
            {
                _maxSimultaneousDownloads = value;
                OnPropertyChanged("MaxSimultaneousDownloads");
            }
        }

        public ObservableCollection<CheckedListItem<NApp>> AppListItems { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();
            LoadWindowData();
            DataContext = this;
        }

        private void LoadWindowData()
        {
            BaseAddress = @"https://10.161.125.46/svn/";
            Branch = "/Branch/Sprint/CCE.17.2";
            InstallationFolder = @"T:\@workTest";



            AppListItems = DataLoader.LoadAppList(InstallationFolder);
        }

        private string GetAppUrl(NApp app)
        {
            return string.Format("{0}{1}/{2}", BaseAddress, app.Name, Branch);
        }

        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MultiDownloadsManager mng = new MultiDownloadsManager(BaseAddress, Branch, MaxSimultaneousDownloads);
            foreach (var item in AppListItems.Where(x => x.IsChecked))
            {
                mng.AddApp(item.Item);
            }
            DownloadButton.IsEnabled = false;
            await mng.DownloadAllAsync();
            DownloadButton.IsEnabled = true;
            MessageBox.Show("Download finished", "Download", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void DetailsHyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            var app = (NApp)((Hyperlink)sender).Tag;
            var detailsWindows = new DownloadInfoWindow(app);
            detailsWindows.Show();
        }
    }
}
