using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    [XmlRoot(ElementName = "recipients", Namespace = "http://api.digipost.no/schema/v1")]
    public class Recipients : BaseModel
    {
        private ObservableCollection<Recipient> _recipientList = new ObservableCollection<Recipient>();

        [XmlElement("recipient")]
        public ObservableCollection<Recipient> RecipientList
        {
            get
            {
                return _recipientList;
            }
            set
            {
                _recipientList = new ObservableCollection<Recipient>(value.ToList());
                NotifyPropertyChanged("RecipientList");
            }
        }
    }
}
