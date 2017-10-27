﻿using NextDevLineManagement.MainWindowModel;
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
        private string _workFolder;


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
        public string WorkFolder
        {
            get { return _workFolder; }
            set
            {
                _workFolder = value;
                OnPropertyChanged("WorkFolder");
            }
        }

        public ObservableCollection<CheckedListItem<NextAppItem>> AppListItems { get; set; }
        

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
            WorkFolder = @"T:\@workTest";

            AppListItems = new ObservableCollection<CheckedListItem<NextAppItem>>();
            AppListItems.Add(new CheckedListItem<NextAppItem>(new NextAppItem() { Name = "MarketplaceBusinessLine",DestinationFolder=WorkFolder+@"\1_Applications\MarketplaceBusinessLine"}, true));
            AppListItems.Add(new CheckedListItem<NextAppItem>(new NextAppItem() { Name = "Marketplace", DestinationFolder = WorkFolder + @"\1_Applications\Marketplace" }, true));
            AppListItems.Add(new CheckedListItem<NextAppItem>(new NextAppItem() { Name = "ConfigurationManagementScripts", DestinationFolder = WorkFolder + @"\0_Scripts" }, true));
        }

        private string GetAppUrl(NextAppItem app)
        {
            return string.Format("{0}{1}/{2}", BaseAddress, app.Name, Branch);
        }

        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MultiCheckoutManager mng = new MultiCheckoutManager(BaseAddress, Branch);
            foreach (var item in AppListItems.Where(x => x.IsChecked))
            {
                mng.AddApp(item.Item);
            }
            await mng.DownloadAllAsync();
            MessageBox.Show("Download finished", "Download", MessageBoxButton.OK, MessageBoxImage.Information);
        }



    }
}
