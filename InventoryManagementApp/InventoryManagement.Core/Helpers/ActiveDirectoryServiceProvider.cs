using System.DirectoryServices;

namespace InventoryManagement.Core.Helpers
{
    public class ActiveDirectoryServiceProvider
    {
        /// <summary>
        /// Login Active Directory User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LoginByActiveDirectory(string userName, string password)
        {
            string path = @"LDAP://ou=users,ou=ouname,ou=ouname1,dc=dcname1,dc=dcname2";
            string domainName = @"MyDomain\";
            string domainNameWithUserName = domainName + userName;
            return Log(path, domainNameWithUserName, password);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private bool Log(string path, string user, string pass)
        {
            DirectoryEntry de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);

            DirectorySearcher ds = new DirectorySearcher(de);
            try
            {
                ds.FindOne();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
