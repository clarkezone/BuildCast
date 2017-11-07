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

namespace BuildCast.Services.Navigation
{
    public interface INavigationService
    {
        event EventHandler<bool> IsNavigatingChanged;

        event EventHandler Navigated;

        bool CanGoBack { get; }

        bool IsNavigating { get; }

        Task NavigateToPodcastsAsync();

        Task NavigateToFavoritesAsync();

        Task NavigateToDownloadsAsync();

        Task NavigateToNotesAsync();

        Task NavigateToNowPlayingAsync();

        Task NavigateToSettingsAsync();

        Task NavigateToFeedAsync(Feed2 feed);

        Task NavigateToEpisodeAsync(Episode2 episode, Feed2 selector);

        Task NavigateToEpisodeAsync(Episode2 episode);

        Task NavigateToPlayerAsync(Episode2 episode);

        Task NavigateToPlayerAsync(InkNote inkNote);

        Task NavigateToInkNoteAsync(InkNote inkNote);
        Task GoBackAsync();
    }
}
