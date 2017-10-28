using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextDevLineManagement.MainWindowModel;

namespace NextDevLineManagement
{
    class DataLoader
    {
        internal static ObservableCollection<CheckedListItem<NApp>> LoadAppList(string workFolder)
        {
            var list = new ObservableCollection<CheckedListItem<NApp>>();
            list.Add(new CheckedListItem<NApp>(new NApp() { Name = "MarketplaceBusinessLine", DestinationFolder = workFolder + @"\1_Applications\MarketplaceBusinessLine" }, true));
            list.Add(new CheckedListItem<NApp>(new NApp() { Name = "Marketplace", DestinationFolder = workFolder + @"\1_Applications\Marketplace" }, true));
            list.Add(new CheckedListItem<NApp>(new NApp() { Name = "ConfigurationManagementScripts", DestinationFolder = workFolder + @"\0_Scripts" }, true));
            return list;
        }
    }
}
