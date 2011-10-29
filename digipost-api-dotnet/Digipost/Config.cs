using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digipost.Digipost
{
    public class Config
    {
        private static string _apiUrl = "https://api.digipost.no";
        private static string _xmlNameSpace = "http://api.digipost.no/schema/v1";
        private static string _digipostMetaType = "application/vnd.digipost-v1+xml";

        public static string ApiPath 
        { 
            get
            {
                return _apiUrl;
            } 
        }

        public static string XmlNameSpace
        {
            get
            {
                return _xmlNameSpace;
            }
        }

        public static string DigipostContentType
        {
            get
            {
                return _digipostMetaType;
            }
        }

        
    }
}
