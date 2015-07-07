﻿using System;
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
using Microsoft.Exchange.WebServices.Data;

namespace RoswellSignatureSync
{

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


    public partial class RoswellSignatureSyncService : ServiceBase
    {

        // Variables for cleanliness
        string logSourceName = "RoswellSignatureSync";
        string logName = "RoswellSignatureSync";
        string signatureLocation = @"C:\Users\tom.wallis\AppData\Roaming\Microsoft\Signatures\rosNew.htm";

        
        // office365 sync variables
        ExchangeService exchangeService;
        UserConfiguration userConfig;
        string username = "roswell@tomwallis.onmicrosoft.com";
        string password = "Passw0rd";
        

        // Function import for reporting service state
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);


        // Constructor
        public RoswellSignatureSyncService()
        {
            this.AutoLog = false;
            InitializeComponent();
            
            // Setup functions at bottom of code
            setupErrorLogs();
            setupTimer();
            setupExchangeConnection();

        }



        // Service management functions BEGIN =================================

        protected override void OnStart(string[] args)
        {
            // Report signature state
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // download a new signature (http://stackoverflow.com/questions/307688/how-to-download-a-file-from-a-url-in-c)
            // update the local signature
            downloadAndReplaceSignature();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }


        protected override void OnStop()
        {
            // Report signature state
            // Update the service state to Stop Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }


        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // Download a new signature and replace the existing one
            downloadAndReplaceSignature();
        }

        // Service management functions END ===================================



        // LOCAL SYNCING FUNCTIONS BEGIN ======================================


        // Download the signature from a website, replacing the file at `signatureLocation`. 
        protected void downloadAndReplaceSignature()
        {
            // download and update local outlook signatures
            using (var client = new WebClient())
            {
                signatureSyncLog.WriteEntry("Updating signature if an update exists.");
                client.DownloadFile("http://upkk988694be.probablytom.koding.io/Roswell/sigTestDownload.htm", signatureLocation);
            }

            // use downlaoded file for OWA signature
            userConfig.Dictionary.Add("signaturehtml", signatureLocation); // do we use signatureLocation here?
            userConfig.Update();

        }

       

        // LOCAL SYNCING FUNCTIONS END ========================================


        // OFFICE365 SYNCING FUNCTIONS BEGIN ==================================

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }


        // OFFICE365 SYNCING FUNCTIONS END ====================================

        // Setup functions to clean up earlier code
        // SETUP FUNCTIONS BEGIN ==============================================

        protected void setupTimer()
        {
            // Create a timer so we replace the signature every hour
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 36000; // This should be 360000 on production!
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }


        protected void setupExchangeConnection()
        {
            // Connect to office365 Exchange
            exchangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP2); // For office365, this appears to be right -- see working powershell script.
            exchangeService.Credentials = new WebCredentials(username, password);
            exchangeService.UseDefaultCredentials = false; // CAN WE USE THIS?! UNSAFE WITH OFFICE365?! https://msdn.microsoft.com/en-us/library/office/dn567668.aspx#Create
            exchangeService.AutodiscoverUrl(username, RedirectionUrlValidationCallback);

            // Get a user config object
            userConfig = UserConfiguration.Bind(exchangeService,
                                                                  "OWA.UserOptions",
                                                                  WellKnownFolderName.Root,
                                                                  UserConfigurationProperties.All);

            // We SHOULD now be set up! In theory.
        }


        protected void setupErrorLogs()
        {
            signatureSyncLog = new System.Diagnostics.EventLog();
         
            // check it doesn't exist already
            if (!System.Diagnostics.EventLog.SourceExists(logSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    logSourceName, logName);
            }
            
            signatureSyncLog.Source = logSourceName;
            signatureSyncLog.Log = logName;
        }

        // SETUP FUNCTIONS END ================================================

    }
}
