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
using System.Threading.Tasks;
using BuildCast.Helpers;
using Realms;
using Windows.Foundation.Collections;
using System.Linq;

namespace BuildCast.DataModel.DM2
{
    public class Episode2 : RealmObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Episode"/> class.
        /// Public Constructor required by EF
        /// </summary>
        public Episode2()
        {
        }

        public Episode2(
            string key,
            string title,
            string description,
            string itemThumbnail,
            DateTimeOffset publishDate,
            TimeSpan duration,
            string subtitle,
            Feed2 feed = null,
            string feedId = "")
        {
            this.UriKey = key;
            this.LocalFileName = BackgroundDownloadHelper.SafeHashUri(new Uri(this.UriKey));
            this.Title = title;
            this.Description = description;
            this.ItemThumbnail = itemThumbnail;

            this.PublishDate = publishDate;
            this.Duration = duration;
            this.Subtitle = subtitle;

            if (feed != null)
            {
                //this.Feeds.Add(feed);
                //this.FeedId = feed?.UriKey?.ToString();
            }
            //else if (!string.IsNullOrEmpty(feedId))
            //{
            //    this.FeedId = feedId;
            //}
        }

        public Episode2(string key, string title, string description, string itemThumbnail)
            : this(
            key,
            title,
            description,
            itemThumbnail,
            DateTimeOffset.MinValue,
            TimeSpan.MinValue,
            string.Empty)
        {
        }

        public Episode2(Episode source, Feed2 feed)
        {
            this.Description = source.Description;
            this.Duration = source.Duration;
            this.IsDownloaded = source.IsDownloaded;
            this.ItemThumbnail = source.ItemThumbnail;
            this.UriKey = source.Key;
            this.LocalFileName = source.LocalFileName;
            this.PublishDate = source.PublishDate;
            this.Subtitle = source.Subtitle;
            this.Title = source.Title;
            this.Feeds.Add(feed);
        }

        [PrimaryKey]
        public string UriKey_ { get; set; }

        [Ignored]
        public string UriKey
        {
            get
            {
                return UriKey_;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Key cannot be null");
                }

                UriKey_ = value;
                LocalFileName = BackgroundDownloadHelper.SafeHashUri(new Uri(UriKey_));
            }
        }

        public PlaybackState PlaybackState { get; set; }

        public string LocalFileName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ItemThumbnail { get; set; }

        public bool IsDownloaded { get; set; }

        public Feed2 Feed
        {
            get
            {
                if (Feeds.Count >= 1)
                {
                    return Feeds[0];
                }

                return null;
            }
        }

        public IList<Feed2> Feeds
        {
            get;
        }

        public DateTimeOffset PublishDate { get; set; }

        public string FormatPublishDate()
        {
            string formattedDate = this.PublishDate.Month.ToString() + "/" + this.PublishDate.Day.ToString() + "/" + this.PublishDate.Year.ToString();
            return formattedDate;
        }

        public double GetPercent()
        {
            return PlaybackState?.GetPercentDouble(this) ?? 0;
        }

        public string GetPercentString()
        {
            return PlaybackState.GetPercentDouble(this).ToString();
        }

        private Double Duration_ { get; set; }

        [Ignored]
        public TimeSpan Duration
        {
            get { return TimeSpan.FromMilliseconds(Duration_); }
            set { Duration_ = value.TotalMilliseconds; }
        }

        public string Subtitle { get; set; }

        public override string ToString()
        {
            return Title ?? string.Empty;
        }

        //public async Task SetDownloaded()
        //{
        //    using (LocalStorageContext lsc = new LocalStorageContext())
        //    {
        //        lsc.Update(this);
        //        this.IsDownloaded = true;
        //        await lsc.SaveChangesAsync();
        //    }
        //}

        public async Task DeleteDownloaded()
        {
            if (IsDownloaded)
            {
                //await BackgroundDownloadHelper.DeleteDownload(LocalFileName);
                var trans = DataModelManager.RealmInstance.BeginWrite();
                this.IsDownloaded = false;
                trans.Commit();
            }
        }

        internal static Episode BuildFromValueSet(ValueSet values)
        {
            Episode fi = new Episode(
                key: values.GetString(nameof(UriKey)),
                title: values.GetString(nameof(Title)),
                description: values.GetString(nameof(Description)),
                itemThumbnail: values.GetString(nameof(ItemThumbnail)),
                publishDate: values.GetDateTimeOffset(nameof(PublishDate)),
                duration: values.GetTimeSpan(nameof(Duration)),
                subtitle: values.GetString(nameof(Subtitle)));
            //feedId: values.GetString(nameof(FeedId)));

            return fi;
        }

        internal void AddToValueSet(ValueSet values)
        {
            ValueSet feedItem = new ValueSet();
            values.Add("feeditem", feedItem);
            feedItem.Add(nameof(UriKey), UriKey.ToString());
            feedItem.Add(nameof(Title), Title);
            feedItem.Add(nameof(Description), Description);
            feedItem.Add(nameof(ItemThumbnail), ItemThumbnail);
            //feedItem.Add(nameof(FeedId), FeedId);
            feedItem.Add(nameof(PublishDate), PublishDate.ToUnixTimeMilliseconds());
            feedItem.Add(nameof(Duration), Duration.TotalMilliseconds);
            feedItem.Add(nameof(Subtitle), Subtitle);
        }

        //private Feed GetFeed()
        //{
        //    return FeedStore.AllFeeds.Where(l => l.Uri.OriginalString == this.FeedId).FirstOrDefault();
        //}
    }
}