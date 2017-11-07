// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace BuildCast.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using BuildCast.DataModel;
    using BuildCast.DataModel.DM2;
    using BuildCast.Services.Navigation;
    using Microsoft.Toolkit.Uwp.Helpers;

    public class DownloadsViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        private IQueryable<Episode2> _downloads;

        public event PropertyChangedEventHandler PropertyChanged;

        public DownloadsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //_downloads = new ItemSource<Episode2>(DataModelManager.RealmInstance.All<Episode2>().Where(ep => ep.IsDownloaded == true).OrderByDescending(ob => ob.PublishDate));
            _downloads = DataModelManager.RealmInstance.All<Episode2>().Where(ep => ep.IsDownloaded == true).OrderByDescending(ob => ob.PublishDate);
        }

        public async void RemoveDownloadedEpisode(Episode2 episode)
        {
            if (episode != null)
            {
                await episode.DeleteDownloaded();
            }
        }

        public IQueryable<Episode2> Downloads
        {
            get
            {
                return _downloads;
            }
        }

        internal void NavigateToEpisode(Episode2 episode)
        {
            var ignored = _navigationService.NavigateToPlayerAsync(episode);
        }

        internal void NavigateToInkNote(InkNote ink)
        {
            var ignored = _navigationService.NavigateToInkNoteAsync(ink);
        }

        internal void NavigateToPlayerWithInk(InkNote ink)
        {
            var ignored = _navigationService.NavigateToPlayerAsync(ink);
        }
    }
}
