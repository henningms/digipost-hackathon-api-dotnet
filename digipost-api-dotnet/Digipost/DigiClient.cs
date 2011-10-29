using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Digipost.Digipost.Models;
using Digipost.Digipost.Models.Recipients;
using Digipost.Digipost.Common;

namespace Digipost.Digipost
{
    class DigiClient
    {   
        public delegate void SearchCompleteEventHandler(object sender, Recipients result);

        public delegate void SuggestCompleteEventHandler(object sender, AutoComplete result);

        public event SearchCompleteEventHandler SearchCompleted;
        public event SuggestCompleteEventHandler SuggestCompleted;

        private delegate void SearchDelegate(string searchString, string url);

        private SearchDelegate SearchMethod;

        private string _searchString;

        public DigiUser User
        {
            get;
            set;
        }

        public Recipients SearchItems
        {
            get;
            set;
        }

        public AutoComplete Suggestions
        {
            get;
            set;
        }

        public DigiClient()
        {
            SearchMethod = SearchForPerson;
        }

        protected virtual void OnSearchCompleted(Recipients recipients)
        {
            if (SearchCompleted != null)
                SearchCompleted(this, recipients);
        }
        private void BaseUrlSearch(string urlToGet, Delegate methodToCall, object param)
        {
            if (string.IsNullOrEmpty(urlToGet)) return;

            var baseUrlWorker = new BackgroundWorker();

            baseUrlWorker.DoWork += (s, e) =>
                                        {
                                            var request =
                                                Http.SendRequest(Http.CreateGetRequest(User, Config.ApiPath + "/"));

                                            e.Result = XmlHelper.DeSerialize<Links>(request.GetResponseStream());
                                        };

            baseUrlWorker.RunWorkerCompleted += (s, e) =>
                                                    {
                                                        var links = e.Result as Links;

                                                        if (links == null) return;

                                                        var url =
                                                            links.LinkItems.Where(
                                                                link => link.Relation.Contains(urlToGet)).First().Url;

                                                        methodToCall.DynamicInvoke(param, url);
                                                    };

            baseUrlWorker.RunWorkerAsync();
        }

        public void Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return;
            if (User == null) return;

            BaseUrlSearch("relations/search", SearchMethod, searchString);

        }

        private void SearchForPerson(string searchString, string url)
        {
            var searchWorker = new BackgroundWorker();

            searchWorker.DoWork += (s, e) =>
                                       {
                                           var path = url + "/" + HttpUtility.UrlEncode(searchString);
                                           var request = Http.SendRequest(Http.CreateGetRequest(User, path));

                                           var recipients =
                                               XmlHelper.DeSerialize<Recipients>(request.GetResponseStream());

                                           e.Result = recipients;
                                       };

            searchWorker.RunWorkerCompleted += (s, e) =>
                                                   {
                                                       var recipients = e.Result as Recipients;

                                                       SearchItems = recipients;
                                                       SearchCompleted(this, recipients);
                                                   };

            searchWorker.RunWorkerAsync();
        }
    }
}
