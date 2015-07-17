using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RoswellSignatureSync
{
    // Replaces Roswell signature tags with data from Active Directory.
    class SignatureParser
    {
        
        string none = "";
        string username = "";
        string jobtitle = "";
        string displayName = "";
        string forename = "";
        string surname = "";
        string firstname = "";
        string lastname = "";
        string phone = "";
        string phonenumber = "";
        string fax = "";
        string faxnumber = "";
        string address="";
        string postcode = "";
        string room = "";
        string email = "";
        string emailaddress = "";
        string mobile="";
        string mobilenumber = "";
        string mobilephone = "";

        EventLog eventLog;

        public SignatureParser()
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://OU=RoswellAD,DC=roswell,DC=local"); // NEEDS CHANGED PER-INSTALLATION!
            DirectorySearcher searcher = new DirectorySearcher(entry);

            searcher.Filter = "(&(objectClass=user)(anr=" + System.Environment.UserName + "))";

            SearchResultCollection results = searcher.FindAll();

            // Testing UMExts:
            PrincipalContext domain = new PrincipalContext(ContextType.Domain);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(domain, Environment.UserDomainName);


        }

        public SignatureParser(EventLog log)
        {
            this.eventLog = log;
            /*
            this.eventLog.WriteEntry("Entered Signature Parser.");

            DirectoryEntry entry = new DirectoryEntry("LDAP://OU=RoswellAD,DC=roswell,DC=local"); // NEEDS CHANGED PER-INSTALLATION!
            this.eventLog.WriteEntry("Created DirectoryEntry.");
            DirectorySearcher searcher = new DirectorySearcher(entry);

            searcher.Filter = "(&(objectClass=user)(anr=" + System.Environment.UserName + "))";

            this.eventLog.WriteEntry("Setup filter.");
            SearchResultCollection results = searcher.FindAll();
            this.eventLog.WriteEntry("Collected results.");
            */
            this.eventLog.WriteEntry("Entered Signature Parser.");
            // Testing UMExts:
            PrincipalContext domain = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(domain, Environment.UserDomainName);
            this.eventLog.WriteEntry("User Domain Name:" + Environment.UserDomainName);
            this.eventLog.WriteEntry("UserName: " + Environment.UserName);

            ManagementObjectSearcher query = new ManagementObjectSearcher("select * from Win32_UserProfile where Loaded = True");
            this.eventLog.WriteEntry("Made query...");
            this.eventLog.WriteEntry("Got resultset...");
            try
            {

                foreach (ManagementObject result in query.Get())
                {
                    this.eventLog.WriteEntry("Got a result!");
                    
                    foreach (PropertyData property in result.Properties)
                    {
                        this.eventLog.WriteEntry(
               "---------------------------------------");
                        this.eventLog.WriteEntry(property.Name);
                        this.eventLog.WriteEntry("Description: " +
                            property.Qualifiers["Description"].Value);

                        this.eventLog.WriteEntry("Type: ");
                        this.eventLog.WriteEntry(property.Type.ToString());


                        this.eventLog.WriteEntry("Qualifiers: ");
                        foreach (QualifierData q in
                            property.Qualifiers)
                        {
                            this.eventLog.WriteEntry(q.Name);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Problem encountred!\nMessage:\n" + ex.Message);
            }

            try
            {

                testProperty("displayname", user);
                testProperty("SamAccountName", user);
                testProperty("givenname", user);
                testProperty("emailaddress", user);
                testProperty("VoiceTelephoneNumber", user);
            }
            catch (Exception ex)
            {
                this.eventLog.WriteEntry("Problem getting property!");
            }

        }

        private void testProperty(string property, UserPrincipal user)
        {
            this.eventLog.WriteEntry(property + ": " + user.GetProperty(property));
        }

    }
}
