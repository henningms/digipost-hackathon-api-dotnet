using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Digipost.Digipost.Common
{
    public class Http
    {
        public static HttpWebResponse SendRequest(HttpWebRequest request)
        {
            return request.GetResponse() as HttpWebResponse;
        }

        public static void SendRequestAsync(HttpWebRequest request, AsyncCallback callback)
        {
            request.BeginGetResponse(callback, request);

        }

        public static HttpWebResponse GetResponse(IAsyncResult result)
        {
            if (result == null) return null;

            return (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
        }

        public static XDocument GetResponseAsXml(IAsyncResult result)
        {
            try
            {
                var response = GetResponse(result);

                if (response == null) return null;

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return XDocument.Parse(reader.ReadToEnd());
                }
            }
            catch (WebException e)
            {
                throw;
            }

        }

        public static HttpWebRequest CreateGetRequest(DigiUser user, string url)
        {
            return CreateGetRequest(user, url, new byte[0]);
        }

        public static HttpWebRequest CreateGetRequest(DigiUser user, string url, byte[] body)
        {
            if (user == null || string.IsNullOrEmpty(url) || body == null) return null;
            if (user.Certificate == null) return null;

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            request.ContentType = Config.DigipostContentType;
            request.Accept = Config.DigipostContentType;

            request.Headers.Add("X-Digipost-UserId", user.Id.ToString());
            request.Date = DateTime.Now;
            request.Headers.Add("Content-MD5", Convert.ToBase64String(MD5.Create().ComputeHash(body)));

            var certificateSignature = CreateCertificateSignature(request, user);

            if (string.IsNullOrEmpty(certificateSignature)) return null;

            request.Headers.Add("X-Digipost-Signature", certificateSignature);
            return request;
        }

        public static string CreateCertificateSignature(HttpWebRequest request, DigiUser user)
        {
            var s = request.Method.ToUpper() + "\n" +
                    request.RequestUri.AbsolutePath.ToLower() + "\n" +
                    "content-md5: " + request.Headers["Content-MD5"] + "\n" +
                    "date: " + request.Date.ToUniversalTime().ToString("r") + "\n" +
                    "x-digipost-userid: " + request.Headers["X-Digipost-UserId"] + "\n" +
                    HttpUtility.UrlEncode(request.RequestUri.Query).ToLower() + "\n";

            var rsa = user.Certificate.PrivateKey as RSACryptoServiceProvider;

            if (rsa == null) return null;

            var privateKeyBlob = rsa.ExportCspBlob(true);
            var rsa2 = new RSACryptoServiceProvider();
            rsa2.ImportCspBlob(privateKeyBlob);

            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
            var signature = rsa2.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));

            return Convert.ToBase64String(signature);
        }


        public static string ExtractUriFromLink(string relEndsWith, XDocument xml, XmlNamespaceManager namespaceManager)
        {
            return xml
                .XPathSelectElements("//dp:link/dp:rel", namespaceManager)
                .Where(element => element.Value.EndsWith(relEndsWith))
                .First()
                .XPathSelectElement("../dp:uri", namespaceManager)
                .Value;
        }

    }
}
