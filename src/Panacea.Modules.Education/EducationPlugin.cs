using Panacea.Core;
using Panacea.Models;
using Panacea.Modularity.Books;
using Panacea.Modularity.Media.Channels;
using Panacea.Modularity.MediaPlayerContainer;
using Panacea.Modularity.UiManager;
using Panacea.Modularity.WebBrowsing;
using Panacea.Modules.Education.Models;
using Panacea.Modules.Education.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Education
{
    public class EducationPlugin : ICallablePlugin
    {
        private readonly PanaceaServices _core;

        public EducationPlugin(PanaceaServices core)
        {
            _core = core;
        }

        public Task BeginInit()
        {
            return Task.CompletedTask;
        }

        public void Call()
        {
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                ui.Navigate(new EducationListViewModel(_core.HttpClient, this));
            }

            //_websocket.PopularNotifyPage("Education");
        }

        public void Dispose()
        {

        }

        public Task EndInit()
        {
            return Task.CompletedTask;
        }

        public Task Shutdown()
        {
            return Task.CompletedTask;
        }

        public void OpenItem(ServerItem item)
        {
        }

        public void OpenEbook(EduItem book)
        {
            var pl = _core.PluginLoader.GetPlugins<IBooksPlugin>().FirstOrDefault();
            if (pl == null) return;
            pl.OpenBook(new Modularity.Books.Models.Book()
            {
                DataUrl =  book.DataUrl,
                Name = book.Name,
                ImgThumbnail = book.ImgThumbnail,
                Description = book.Description
                
            }, "Education");
            //var json = _serializer.Serialize(book);
            //_comm.RaiseEvent("books-openbook", this,
            //    new Dictionary<string, object> { { "bookJsonObj", json }, { "plugin", "Education" } });
        }

        public void OpenVideo(EduItem video)
        {
            if (video.DataUrl.Count <= 0) return;
            if (_core.TryGetMediaPlayerContainer(out IMediaPlayerContainer player))
            {
                player.Play(new MediaRequest(new IptvMedia { URL = _core.HttpClient.RelativeToAbsoluteUri(video.DataUrl[0].Url), Name = video.Name })
                {
                    AllowPip = true
                });
            }

        }

        public void OpenAudio(EduItem video)
        {
            if (video.DataUrl.Count <= 0) return;
            if (_core.TryGetMediaPlayerContainer(out IMediaPlayerContainer player))
            {
                player.Play(new MediaRequest(new IptvMedia { URL = _core.HttpClient.RelativeToAbsoluteUri(video.DataUrl[0].Url), Name = video.Name })
                {
                    AllowPip = false,
                    ShowVideo = false,
                    MediaPlayerPosition = MediaPlayerPosition.Notification,
                    FullscreenMode = FullscreenMode.NoFullscreen,
                });
            }
        }

        public void OpenQuestionnaire(EduItem book)
        {
            //todo _comm.RaiseEvent("questionnaire-open", this, new Dictionary<string, dynamic> { { "questionnaireid", book.Id } });
        }

        public void OpenWebpage(EduItem webpage)
        {
            if(_core.TryGetWebBrowser(out IWebBrowserPlugin web))
            {
                web.OpenUnmanaged(_core.HttpClient.RelativeToAbsoluteUri(webpage.DataUrl[0].Url));
            }
        }

        public void OpenGame(EduItem webpage)
        {
            if (_core.TryGetWebBrowser(out IWebBrowserPlugin web))
            {
                web.OpenUnmanaged(_core.HttpClient.RelativeToAbsoluteUri(webpage.DataUrl[0].Url));
            }
        }
    }
}
