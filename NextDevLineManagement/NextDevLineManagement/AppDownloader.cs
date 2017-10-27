using NextDevLineManagement.MainWindowModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDevLineManagement
{
    public class AppDownloader
    {

        public NextAppItem App { get; private set; }
        private string BaseUrl { get; set; }
        private string Branch { get; set; }

        public AppDownloader(NextAppItem app, string baseUrl, string branch)
        {
            App = app;
            Branch = branch;
            BaseUrl = baseUrl;
        }

        public async Task<int> CheckoutAsync()
        {
            return await Task.Run( () => 
            {
                App.State = NextAppItem.StateTypes.Downloading;
                Download();
                App.State = NextAppItem.StateTypes.Finished;
                return 1; 
            });
        }

        private void Download()
        {
            var command = @"T:\@work\NextWay\9_External\Tools\SVN\svn checkout " + BaseUrl + App.Name + Branch + " " + App.DestinationFolder;
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                Console.Error.WriteLine(objException.StackTrace);
            }
        }

    }
}
