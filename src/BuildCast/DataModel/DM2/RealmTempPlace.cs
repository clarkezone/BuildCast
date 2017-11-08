using System;
using System.Linq;

namespace BuildCast.DataModel.DM2
{
    class RealmTempPlace
    {
        public static void CheckMigrate()
        {
            var r = DataModelManager.RealmInstance;
            var query = r.All<Episode2>().OrderByDescending(ob => ob.PublishDate);
            if (query.Count() == 0)
            {
                using (LocalStorageContext lc = new LocalStorageContext())
                {
                    var trans = DataModelManager.RealmInstance.BeginWrite();
                    foreach (var item in lc.EpisodeCache)
                    {
                        var feed = GetFeed(item);
                        var ep = WriteEpisode(item, feed);
                        
                    }

                    trans.Commit();
                }
            }
        }

        private static Episode2 WriteEpisode(Episode source, Feed2 feed)
        {
            if (feed == null)
            {
                throw new ArgumentException();
            }

            var ep = DataModelManager.RealmInstance.All<Episode2>().Where(oo => oo.UriKey_ == source.Key).FirstOrDefault();
            if (ep == null)
            {
                ep = new Episode2(source, feed);
                DataModelManager.RealmInstance.Add(ep);
            }

            return ep;
        }

        private static Feed2 GetFeed(Episode source)
        {
            if (source.Feed == null)
            {
                throw new ArgumentException("invalid source feed");
            }

            var query = DataModelManager.RealmInstance.All<Feed2>().Where(f => f.UriKey_ == source.Feed.Uri.ToString());

            var feed = query.FirstOrDefault();

            if (feed == null)
            {
                feed = new Feed2(source.Feed);
                DataModelManager.RealmInstance.Add(feed);
            }

            return feed;
        }
    }
}
