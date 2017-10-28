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
    public class NAppDownloader
    {

        public NApp App { get; private set; }
        private string BaseUrl { get; set; }
        private string Branch { get; set; }

        public NAppDownloader(NApp app, string baseUrl, string branch)
        {
            App = app;
            Branch = branch;
            BaseUrl = baseUrl;
        }

        public async void CheckoutAsync()
        {
            await Task.Run(() =>
            {
                App.State = NApp.StateTypes.Downloading;
                Download();
                if (Directory.Exists(App.DestinationFolder))
                {
                    App.State = NApp.StateTypes.Finished;
                }
                else
                {
                    App.State = NApp.StateTypes.Error;
                }
            });
        }

        private void Download()
        {
            var command = @"T:\@work\NextWay\9_External\Tools\SVN\svn checkout " + BaseUrl + App.Name + Branch + " " + App.DestinationFolder;
            App.DownloadOutput = command+Environment.NewLine;
            try
            {
                var hidden = true;
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", " " + command);
                procStartInfo.RedirectStandardOutput = hidden;
                procStartInfo.RedirectStandardError = hidden;
                procStartInfo.UseShellExecute = !hidden;
                procStartInfo.CreateNoWindow = hidden;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.ErrorDataReceived += ConsoleDataReceived;
                proc.OutputDataReceived += ConsoleDataReceived;
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                
                
                proc.WaitForExit();
                Console.WriteLine(App.DownloadOutput);
            }
            catch (Exception objException)
            {
                Console.Error.WriteLine(objException.StackTrace);
            }
        }

        private void ConsoleDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                App.DownloadOutput += e.Data + Environment.NewLine;
        }

    }
}
