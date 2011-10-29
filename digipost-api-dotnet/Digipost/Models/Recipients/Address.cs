using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    [Serializable()]
    public class Address : BaseModel
    {
        private string _street, _houseNumber, _city;
        private int _zipCode;

        [XmlElement("street")]
        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                NotifyPropertyChanged("Street");
            }
        }

        [XmlElement("houseNumber")]
        public string HouseNumber
        {
            get
            {
                return _houseNumber;
            }
            set
            {
                _houseNumber = value;
                NotifyPropertyChanged("HouseNumber");
            }
        }

        [XmlElement("zipcode")]
        public int ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                _zipCode = value;
                NotifyPropertyChanged("ZipCode");
            }
        }

        [XmlElement("city")]
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }
    }
}
