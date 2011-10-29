using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    [Serializable()]
    public class Recipient : BaseModel
    {
        private string _firstName, _lastName, _digiPostAddress;
        private Address _address;
        private Link _link;

        [XmlElement("firstName")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        [XmlElement("lastName")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        [XmlElement("digipostAddress")]
        public string DigipostAddress
        {
            get
            {
                return _digiPostAddress;
            }
            set
            {
                _digiPostAddress = value;
                NotifyPropertyChanged("DigipostAddress");
            }
        }

        [XmlElement("address")]
        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
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
