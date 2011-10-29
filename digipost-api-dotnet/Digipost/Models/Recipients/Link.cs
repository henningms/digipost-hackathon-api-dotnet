using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    [Serializable]
    public class Link : BaseModel
    {
        private string _relation, _url, _mediaType;

        [XmlAttribute("rel")]
        public string Relation
        {
            get
            {
                return _relation;
            }
            set
            {
                _relation = value;
                NotifyPropertyChanged("Relation");
            }
        }

        [XmlAttribute("uri")]
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                NotifyPropertyChanged("Url");
            }
        }

        [XmlAttribute("mediaType")]
        public string MediaType
        {
            get
            {
                return _mediaType;
            }
            set
            {
                _mediaType = value;
                NotifyPropertyChanged("MediaType");
            }
        }
    }
}
