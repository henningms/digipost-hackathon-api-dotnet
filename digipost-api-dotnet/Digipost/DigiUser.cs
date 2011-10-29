using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Digipost.Digipost.Common;

namespace Digipost.Digipost
{
    public class DigiUser
    {
        public bool NotifyBySms
        {
            get;
            set;
        }

        public X509Certificate2 Certificate
        {
            get;
            set;
        }

        public XmlNamespaceManager NamespaceManager
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public DigiUser()
        {
            
        }

        public DigiUser(int userId) : this(userId, null, null)
        {
        }

        public DigiUser(int userId, string certificateFilePath, string password) : this(userId, certificateFilePath, password, false)
        {
        }

        public DigiUser(int userId, string certificateFilePath, string password, bool notifyBySms)
        {
            Id = userId;
            try
            {
                Certificate = Util.GetCertificate(certificateFilePath, password);
            }
            catch (Exception)
            {
                throw;
            }

            NamespaceManager = new XmlNamespaceManager(new NameTable());
            NamespaceManager.AddNamespace("dp", Config.XmlNameSpace);
            
        }
    }
}
