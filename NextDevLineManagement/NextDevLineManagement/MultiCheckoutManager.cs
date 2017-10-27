using NextDevLineManagement.MainWindowModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDevLineManagement
{
    public class MultiCheckoutManager
    {
        const int MaxSimultaneousDownload = 2;

        public List<AppDownloader> Queue { get; set; }
        public string BaseUrl { get; set; }
        public string Branch { get; set; }

        public MultiCheckoutManager(string baseUrl, string branch)
        {
            Queue = new List<AppDownloader>();
            BaseUrl = baseUrl;
            Branch = branch;
            Queue = new List<AppDownloader>();
        }

        public void AddApp(NextAppItem app)
        {
            app.State = NextAppItem.StateTypes.Waiting;
            AppDownloader svn = new AppDownloader(app, BaseUrl, Branch);
            Queue.Add(svn);
        }

        public async Task<bool> DownloadAllAsync()
        {
            var toDownload = Queue.Where(x => x.App.State == NextAppItem.StateTypes.Waiting).ToList();
            while (toDownload.Count() > 0)
            {
                var downloading = Queue.Where(x => x.App.State == NextAppItem.StateTypes.Downloading);
                var freeSlots = MaxSimultaneousDownload - downloading.Count();
                for (int i = 0; i < freeSlots; i++)
                {
                    if (toDownload.Count() > i)
                    {
                        toDownload[i].CheckoutAsync();
                    }
                }
                await Task.Delay(2000);
                toDownload = Queue.Where(x => x.App.State == NextAppItem.StateTypes.Waiting).ToList();
            }

            return true;
        }

    }
}
