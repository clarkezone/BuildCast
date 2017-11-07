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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BuildCast.DataModel;
using BuildCast.DataModel.DM2;
using BuildCast.Helpers;
using BuildCast.Services.Navigation;
using Windows.UI.Xaml.Navigation;
using Realms;

namespace BuildCast.ViewModels
{
    public class FeedDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        private INavigationService _navService;
        private bool _loading;
        //private FeedEpisodeSource _episodeSource;
        private IQueryable<Episode2> _episodeSource;

        public event PropertyChangedEventHandler PropertyChanged;

        public Feed2 CurrentFeed { get; private set; }

        //public FeedEpisodeSource EpisodeData
        //{
        //    get
        //    {
        //        return _episodeSource;
        //    }
        //}
        public IQueryable<Episode2> EpisodeData
        {
            get
            {
                return _episodeSource;
            }
        }

        public bool Loading
        {
            get => _loading;
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Loading)));
                }
            }
        }

        public Episode2 PersistedEpisode { get; set; }

        public FeedDetailsViewModel(INavigationService navigationService)
        {
            _navService = navigationService;

            Loading = true;
        }

        public async Task NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            Loading = true;

            if (navigationMode != NavigationMode.Back && parameter is Feed2 feed)
            {
                CurrentFeed = feed;
                //_episodeSource = new FeedEpisodeSource(CurrentFeed);
                _episodeSource = CurrentFeed.Episodes.OrderByDescending(ob => ob.PublishDate);


                var test = DataModelManager.RealmInstance.All<Episode2>().ToArray().Where(e => e.Feeds.Any(f => f == CurrentFeed)).ToList();

            }

            if (navigationMode != NavigationMode.Back)
            {
                PersistedEpisode = null;
            }

            Loading = false;
        }

        public async Task<int> RefreshData()
        {
            var newEpisodes = await CurrentFeed.GetNewEpisodesAsync();
            //foreach (var episode in newEpisodes)
            //{
            //    CurrentFeed.Episodes.Add(episode);
            //}

            return newEpisodes.Count;
        }

        public void GoToEpisodeDetails(Episode2 detailsItem)
        {
            PersistedEpisode = detailsItem;
            _navService.NavigateToEpisodeAsync(detailsItem, CurrentFeed);
        }

        // TODO: Move these episode specific functions to the Episode themselves, pending further review.
        public void PlayEpisode(Episode2 episode)
        {
            _navService.NavigateToPlayerAsync(episode);
        }

        public void FavoriteEpisode(Episode2 episode)
        {
            //TODO:fix
            throw new Exception();
            //using (var db = new LocalStorageContext())
            //{
            //    db.Favorites.Add(new Favorite(episode));
            //    db.SaveChanges();
            //}
        }

        public void DownloadEpisode(Episode2 episode)
        {
            var task = BackgroundDownloadHelper.Download(new Uri(episode.UriKey));
        }

        public async Task RemoveTopThree()
        {
            //TODO:
            //await CurrentFeed.RemoveTopThreeItems();
            //EpisodeData.RemoveAt(0);
            //EpisodeData.RemoveAt(0);
            //EpisodeData.RemoveAt(0);
        }
    }
}
