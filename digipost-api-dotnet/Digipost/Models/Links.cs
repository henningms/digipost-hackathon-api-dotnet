using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Digipost.Digipost.Models.Recipients;

namespace Digipost.Digipost.Models
{
    [Serializable]
    [XmlRoot(ElementName = "links", Namespace = "http://api.digipost.no/schema/v1")]
    public class Links : BaseModel
    {
        private ObservableCollection<Link> _links = new ObservableCollection<Link>();

        [XmlElement("link")]
        public ObservableCollection<Link> LinkItems
        {
            get
            {
                return _links;
            }
            set
            {
                _links = new ObservableCollection<Link>(value.ToList());
                NotifyPropertyChanged("LinkItems");
            }
        } 
    }
}
