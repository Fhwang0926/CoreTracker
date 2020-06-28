using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Timers;
using System.Windows.Forms;

namespace CoreTracker
{
    public partial class Form2 : Form
    {
        Stopwatch sw = new Stopwatch();
        int autoCloseCount = 0;
        int autoCloseCountMAX = 5;
        static bool downloadSuccessful = false;
        public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Form2()
        {
            InitializeComponent();
        }

        // The event that will fire whenever the progress of the WebClient is changed
        public void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            downloadLabelSet(string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00")));

            // Update the progressbar percentage only when the value is not the same.
            downloadBarValueSet(e.ProgressPercentage);

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            downloadLabelSet(string.Format("{0} MB's / {1} MB's / {2}",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"),
                e.ProgressPercentage.ToString() + "%"
            ));
            Application.DoEvents();
        }

        public bool getDownloadStatus()
        {
            return downloadSuccessful;
        }

        private void downloadCompleted(Object source, EventArgs e)
        {
            Application.DoEvents();
            if (autoCloseCount >= autoCloseCountMAX)
            {
                downloadSuccessful = true;
                timer.Stop();
                timer.Dispose();
            } else
            {
                lb_download_status.Text = $"Download completed!, Auto start new installer {autoCloseCountMAX - autoCloseCount} sec later";
                autoCloseCount++;
            }
        }

        // The event that will trigger when the WebClient is completed
        public void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            timer.Interval = 1000;
            timer.Tick += new EventHandler(downloadCompleted);

            timer.Stop();
            autoCloseCount = 0;
            timer.Start();
        }

        #region CrossThread

        delegate void lb_download_status_set(string data);
        delegate void lb_download_status_value(int data);
        private void downloadLabelSet(string data)
        {
            if (lb_download_status.InvokeRequired)
            {
                lb_download_status_set call = new lb_download_status_set(downloadLabelSet);
                this.Invoke(call, data);
            }
            else
            {
                lb_download_status.Text = data;
            }
        }
        private void downloadBarValueSet(int data)
        {
            if (lb_download_status.InvokeRequired)
            {
                lb_download_status_value call = new lb_download_status_value(downloadBarValueSet);
                this.Invoke(call, data);
            }
            else
            {
                pb_download.Value = data;
            }
        }

        #endregion
    }
}
