using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

// Taken from http://stackoverflow.com/questions/1785751/how-to-get-company-and-department-from-active-directory-given-a-userprincipa
namespace RoswellSignatureSync
{
    public static class UserManagementExtensions
    {

        public static String GetProperty(this Principal principal, String property)
        {
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
                return directoryEntry.Properties[property].Value.ToString();
            else
                return String.Empty;
        }

    }
}