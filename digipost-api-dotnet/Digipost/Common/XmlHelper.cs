using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Common
{
    public class XmlHelper
    {
        public static T DeSerialize<T>(IAsyncResult result)
        {
            try
            {
                var response = Http.GetResponse(result);
                return DeSerialize<T>(response.GetResponseStream());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T DeSerialize<T>(Stream stream)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(T));

                return (T)deserializer.Deserialize(stream);


            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
