using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RoswellSignatureSync
{
    public partial class RoswellSignatureSyncService : ServiceBase
    {

        // Variables for cleanliness
        string logSourceName = "RoswellSignatureSync";
        string logName = "RoswellSignatureSync";
        string signatureLocation = @"C:\Users\tom.wallis\AppData\Roaming\Microsoft\Signatures\rosNew.htm";

        // Variables for specifying service state
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        public RoswellSignatureSyncService()
        {
            this.AutoLog = false;
            InitializeComponent();

            // Define event log
            signatureSyncLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(logSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    logSourceName, logName);
            }
            signatureSyncLog.Source = logSourceName;
            signatureSyncLog.Log = logName;

            // Create a timer so we replace the signature every hour
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 36000; // This should be 360000 on production!
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

        }

        protected override void OnStart(string[] args)
        {
            // download a new signature (http://stackoverflow.com/questions/307688/how-to-download-a-file-from-a-url-in-c)
            // update the local signature
            downloadAndReplaceSignature();
        }

        protected override void OnStop()
        {
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // Download a new signature and replace the existing one
            downloadAndReplaceSignature();
        }

        // Download the signature from a website, replacing the file at `signatureLocation`. 
        protected bool downloadAndReplaceSignature()
        {
            using (var client = new WebClient())
            {
                signatureSyncLog.WriteEntry("Updating signature if an update exists.");
                client.DownloadFile("http://upkk988694be.probablytom.koding.io/Roswell/sigTestDownload.htm", signatureLocation);
            }
            return true;
        }
        protected bool downloadAndReplaceSignature(string url)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, signatureLocation);
            }
            return true;
        }

        // returns true if update necessary, false otherwise. 
        protected bool updateSignautre()
        {
            // replace local file in the AppData filder for this user with the recently downloaded signature.
            return false;
        }
    }
}
