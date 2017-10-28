using NextDevLineManagement.MainWindowModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDevLineManagement
{
    public class MultiDownloadsManager
    {
        private int _maxSimultaneousDownload = 3;

        public List<NAppDownloader> Queue { get; set; }
        public string BaseUrl { get; set; }
        public string Branch { get; set; }

        public MultiDownloadsManager(string baseUrl, string branch, int maxDownloads = 3)
        {
            Queue = new List<NAppDownloader>();
            BaseUrl = baseUrl;
            Branch = branch;
            Queue = new List<NAppDownloader>();
            _maxSimultaneousDownload = maxDownloads;
        }

        public void AddApp(NApp app)
        {
            app.State = NApp.StateTypes.Waiting;
            NAppDownloader svn = new NAppDownloader(app, BaseUrl, Branch);
            Queue.Add(svn);
        }

        public async Task<bool> DownloadAllAsync()
        {
            var toDownload = Queue.Where(x => x.App.State == NApp.StateTypes.Waiting).ToList();
            while (toDownload.Any())
            {
                var downloading = Queue.Where(x => x.App.State == NApp.StateTypes.Downloading);
                var freeSlots = _maxSimultaneousDownload - downloading.Count();
                for (var i = 0; i < freeSlots; i++)
                {
                    if (toDownload.Count() > i)
                    {
                        toDownload[i].CheckoutAsync();
                    }
                }
                await Task.Delay(2000);
                toDownload = Queue.Where(x => x.App.State == NApp.StateTypes.Waiting).ToList();
            }

            return true;
        }

    }
}
