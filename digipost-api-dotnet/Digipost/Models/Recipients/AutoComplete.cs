using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Digipost.Digipost.Models.Recipients
{
    [XmlRoot(ElementName = "autocomplete", Namespace = "http://api.digipost.no/schema/v1")]
    public class AutoComplete : BaseModel
    {
        private ObservableCollection<Suggestion> _suggestionList = new ObservableCollection<Suggestion>();

        [XmlElement("suggestion")]
        public ObservableCollection<Suggestion> Suggestions
        {
            get
            {
                return _suggestionList;
            }
            set
            {
                _suggestionList = new ObservableCollection<Suggestion>(value.ToList());
                NotifyPropertyChanged("Suggestions");
            }
        }
    }
}
