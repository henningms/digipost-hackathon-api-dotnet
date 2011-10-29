using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    public class Suggestion : BaseModel
    {
        private string _searchString;
        private Link _link;

        [XmlElement("searchString")]
        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                NotifyPropertyChanged("SearchString");
            }
        }

        [XmlElement("link")]
        public Link Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                NotifyPropertyChanged("Link");
            }
        }
    }
}
