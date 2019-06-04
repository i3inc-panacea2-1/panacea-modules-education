using Panacea.Controls;
using Panacea.Core;
using Panacea.Modules.Education.Models;
using Panacea.Modules.Education.Views;
using Panacea.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Education.ViewModels
{
    [View(typeof(EducationList))]
    class EducationListViewModel : ViewModelBase
    {
        ObservableCollection<EducationCategory> _categories;
        public ObservableCollection<EducationCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        List<EducationCategory> _rootCategories;
        public List<EducationCategory> RootCategories
        {
            get => _rootCategories;
            set
            {
                _rootCategories = value;
                OnPropertyChanged();
            }
        }

        EducationCategory _selectedCategory;
        public EducationCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                UpdateSelectedContent();
            }
        }

        EducationCategory _selectedContent;
        public EducationCategory SelectedContent
        {
            get => _selectedContent;
            set
            {
                _selectedContent = value;
                OnPropertyChanged();
            }
        }

        string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
            }
        }

        async Task UpdateSelectedContent()
        {
            SearchTerm = "";

            //DescriptionBox.Visibility = Visibility.Visible;
            try
            {
                var cat = SelectedCategory;
                var response =
                    await
                        _http.GetObjectAsync<GetCategoryResponse>($"education/get_category/{SelectedCategory.Id}/");
                if (response.Success && SelectedCategory == cat)
                {
                    SelectedContent = response.Result.Education.EducationCategory;
                }
            }
            catch
            {
            }
        }

        public EducationListViewModel(IHttpClient http, EducationPlugin plugin)
        {
            _http = http;
            TileClickCommand = new RelayCommand(args =>
            {
                var item = args as EduItem;
                //_websocket.PopularNotify("Education", "EducationItem", item.Id);
                switch (item.EducationType)
                {
                    case "Videos":
                        plugin.OpenVideo(item);
                        break;
                    case "EBooks":
                        plugin.OpenEbook(item);
                        break;
                    case "Questionnaires":
                        plugin.OpenQuestionnaire(item);
                        break;
                    case "Webpages":
                        plugin.OpenWebpage(item);
                        break;
                    case "Games":
                        plugin.OpenGame(item);
                        break;
                    case "Audio":
                        plugin.OpenAudio(item);
                        break;
                }
            });
        }

        public override async void Activate()
        {
            try
            {
                var response = await _http.GetObjectAsync<GetCategoriesResponse>("education/get_categories/");
                if (response.Success)
                {
                    Categories = new ObservableCollection<EducationCategory>(response.Result.Education.EducationCategories);
                    if (Categories.Count > 0)
                        SelectedCategory = Categories.First();
                }
            }
            catch
            {
            }
        }

        private readonly IHttpClient _http;

        public RelayCommand TileClickCommand { get; }
    }
}
