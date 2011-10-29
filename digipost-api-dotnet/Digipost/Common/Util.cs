using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Digipost.Digipost.Common
{
    public class Util
    {
        public static X509Certificate2 GetCertificate(string filePath, string password)
        {
            try
            {
                return GetCertificate(filePath, password, X509KeyStorageFlags.Exportable);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public static X509Certificate2 GetCertificate(string filePath, string password, X509KeyStorageFlags flags)
        {
            try
            {
                return new X509Certificate2(filePath, password, flags);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
