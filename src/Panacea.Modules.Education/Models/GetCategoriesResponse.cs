using Panacea.Models;
using Panacea.Multilinguality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Education.Models
{
    [DataContract]
    public class GetCategoriesResponse
    {
        [DataMember(Name = "Education")]
        public CategoriesWrapper Education { get; set; }
    }

    [DataContract]
    public class CategoriesWrapper
    {
        [DataMember(Name = "educationCategories")]
        public List<EducationCategory> EducationCategories { get; set; }
    }

    public class DataUrl
    {
        private string _url;

        public DataUrl()
        {
        }

        public DataUrl(string dataType, string url)
        {
            this.dataType = dataType;
            this.url = url;
        }

        public string dataType { get; set; }

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
    }



    [DataContract]
    public class EduItem : ServerItem
    {
        [DataMember(Name = "dataUrl")]
        public List<DataUrl> DataUrl { get; set; }

        [IsTranslatable]
        [DataMember(Name = "description")]
        public string Description
        {
            get => GetTranslation();
            set => SetTranslation(value);
        }

        public Boolean Active { get; set; }

        [DataMember(Name = "educationType")]
        public String EducationType { get; set; }

        [DataMember(Name = "priority")]
        public int Priority { get; set; }
    }

    [DataContract]
    public class EduItemWrapper
    {
        [DataMember(Name = "item")]
        public EduItem Item { get; set; }
    }

    public class EducationItemTile
    {
        public List<EduItem> Items { get; set; }
        public TranslatableObject Text { get; set; }
    }

    [DataContract]
    public class EducationCategory : ServerTreeItem<EduItem>
    {
        [IsTranslatable]
        [DataMember(Name = "description")]
        public string Description
        {
            get => GetTranslation();
            set => SetTranslation(value);
        }

        [DataMember(Name = "bkg")]
        public string Bkg { get; set; }

        [IsTranslatable]
        [DataMember(Name = "language")]
        public string Language
        {
            get => GetTranslation();
            set => SetTranslation(value);
        }

        List<EduItemWrapper> _items;
        [DataMember(Name = "items")]
        public List<EduItemWrapper> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalPages");
                OnPropertyChanged("PagedItems");

            }
        }

        public int TotalPages
        {
            get
            {
                var max = (int)Math.Ceiling((double)(Items.Count / Math.Floor(_contanerHeight / 60)));
                if (_currentPage > max)
                {
                    CurrentPage = max;
                }
                return max;
            }
        }

        int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged("PagedItems");
            }
        }

        public List<EduItemWrapper> PagedItems
        {
            get => Items.Skip((_currentPage - 1) * (int)Math.Floor(_contanerHeight / 60)).Take((int)Math.Floor(_contanerHeight / 60)).ToList();
        }

        double _contanerHeight = 500;
        public double ContainerHeight
        {
            get => _contanerHeight;
            set
            {
                _contanerHeight = value;
                OnPropertyChanged("TotalPages");
                OnPropertyChanged("PagedItems");
            }
        }

        public List<EduItem> EBooks
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("EBooks")))
                    ? Items.Where(i => i.Item.EducationType.Equals("EBooks")).Select(i => i.Item).ToList()
                    : null;
            }
        }
        protected List<EduItem> Games
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Games")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Games")).Select(i => i.Item).ToList()
                    : null;
            }
        }
        protected List<EduItem> Webpages
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Webpages")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Webpages")).Select(i => i.Item).ToList()
                    : null;
            }
        }

        protected List<EduItem> Videos
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Videos")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Videos")).Select(i => i.Item).ToList()
                    : null;
            }
        }

        protected List<EduItem> Podcasts
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Podcasts")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Podcasts")).Select(i => i.Item).ToList()
                    : null;
            }
        }

        protected List<EduItem> Questionnaires
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Questionnaires")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Questionnaires")).Select(i => i.Item).ToList()
                    : null;
            }
        }

        protected List<EduItem> Audio
        {
            get
            {
                return Items != null && Items.Any((i => i.Item.EducationType.Equals("Audio")))
                    ? Items.Where(i => i.Item.EducationType.Equals("Audio")).Select(i => i.Item).ToList()
                    : null;
            }
        }

        public List<EducationItemTile> GroupedItems
        {
            get
            {
                var dict = new List<EducationItemTile>();
                FillItemCategory(ref dict, Videos, "Videos");
                FillItemCategory(ref dict, EBooks, "Books");
                FillItemCategory(ref dict, Webpages, "Webpages");
                FillItemCategory(ref dict, Podcasts, "Podcasts");
                FillItemCategory(ref dict, Questionnaires, "Questionnaires");
                FillItemCategory(ref dict, Games, "Games");
                FillItemCategory(ref dict, Audio, "Audio");


                return dict;
            }
        }

        private void FillItemCategory(ref List<EducationItemTile> list, List<EduItem> items, string header)
        {
            if (items == null) return;
            list.Add(new EducationItemTile() { Items = items, Text = new TranslatableObject(header, new Translator("Education")) });
        }
    }
}
