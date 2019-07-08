using Panacea.Controls;
using Panacea.Core;
using Panacea.Modules.Education.Models;
using Panacea.Modules.Education.Views;
using Panacea.Multilinguality;
using Panacea.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

        bool _busy;
        public bool IsBusy
        {
            get => _busy;
            set
            {
                _busy = value;
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
                SearchTerm = "";
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
                UpdateSelectedContent();
                OnPropertyChanged();
            }
        }

        async Task UpdateSelectedContent()
        {

            //DescriptionBox.Visibility = Visibility.Visible;
            try
            {
                if (IsBusy) return;
                _cts?.Cancel();
                var source = new CancellationTokenSource();
                _cts = source;


                SelectedContent = null;

                if (string.IsNullOrEmpty(SearchTerm))
                {
                    IsBusy = true;
                    var cat = SelectedCategory;
                    var response =
                        await
                            _http.GetObjectAsync<GetCategoryResponse>($"education/get_category/{SelectedCategory.Id}/");
                    if (response.Success && SelectedCategory == cat)
                    {
                        SelectedContent = response.Result.Education.EducationCategory;
                    }
                }
                else
                {
                    await Task.Delay(1000);
                    if (source.IsCancellationRequested) return;
                    IsBusy = true;
                    var response =
                    await
                       _http.GetObjectAsync<ObservableCollection<EduItem>>("education/find_items/" +
                                                                                    WebUtility.UrlEncode(SearchTerm) + "/" +
                                                                                    LanguageContext.Instance.Culture.Name +
                                                                                  "/");
                    if (source.IsCancellationRequested) return;
                    if (response.Success)
                    {
                        var itemWrapers = new List<EduItemWrapper>();
                        response.Result.ToList().ForEach(i => itemWrapers.Add(new EduItemWrapper { Item = i }));
                        var searchCollection = new EducationCategory
                        {
                            Name = @"Search results for: '" + SearchTerm + @"'",
                            Id = "search",
                            Items = itemWrapers
                        };
                        SelectedContent = searchCollection;

                    }
                    else SelectedContent = new EducationCategory();
                }
            }
            catch
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        CancellationTokenSource _cts;


        public EducationListViewModel(IHttpClient http, EducationPlugin plugin)
        {
            _http = http;
            RefreshCommand = new RelayCommand(args =>
            {
                UpdateSelectedContent();
            });

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
            if (Categories != null) return;
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

        public RelayCommand RefreshCommand { get; }
    }
}
