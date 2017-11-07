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

using System;
using System.Threading.Tasks;
using BuildCast.DataModel;
using BuildCast.DataModel.DM2;
using BuildCast.Helpers;
using BuildCast.Services.Navigation;
using Windows.UI.Xaml.Navigation;

namespace BuildCast.ViewModels
{
    public class EpisodeDetailsViewModel : INavigableTo
    {
        private INavigationService _navigationService;

        public EpisodeDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public event EventHandler DownloadError;

        public Episode2 CurrentEpisode { get; set; }

        // These methods need to be moved into the Episode
        public void PlayCurrentEpisode()
        {
            //TODO
            //_navigationService.NavigateToPlayerAsync(CurrentEpisode);
        }

        public void FavoriteCurrentEpisode()
        {
            using (var db = new LocalStorageContext())
            {
                //TODO
                //db.Favorites.Add(new Favorite(CurrentEpisode));
                //db.SaveChanges();
            }
        }

        public void DownloadCurrentEpisode()
        {
            var task = BackgroundDownloadHelper.Download(new Uri(CurrentEpisode.UriKey));
            task.ContinueWith(t => DownloadError?.Invoke(this, EventArgs.Empty), TaskContinuationOptions.OnlyOnFaulted);
        }

        public Task NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (parameter is Episode2 episode)
            {
                CurrentEpisode = episode;
            }

            return Task.CompletedTask;
        }
    }
}
