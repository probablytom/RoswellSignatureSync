using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SignatureParser()
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://OU=RoswellAD,DC=roswell,DC=local"); // NEEDS CHANGED PER-INSTALLATION!
            DirectorySearcher searcher = new DirectorySearcher(entry);

            searcher.Filter = "(&(objectClass=user)(anr=" + System.Environment.UserName + "))";

            SearchResultCollection results = searcher.FindAll();



        }

    }
}
