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
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml;
using Realms;

namespace BuildCast.DataModel.DM2
{
    public class FeedEpisodeSource : INotifyCollectionChanged, IList
    {
        Feed2 _feed;
        IQueryable<Episode2> _query;

        public FeedEpisodeSource(Feed2 feed)
        {
            _feed = feed;
            _query = _feed.Episodes.OrderByDescending(ob => ob.PublishDate).AsQueryable();

            var dispatcher = Window.Current.Dispatcher;

            var disposable = _feed.Episodes.AsRealmCollection().SubscribeForNotifications((s, e, x) => {

                // TODO: need a more general impl
                if (e?.InsertedIndices?.Length == 1)
                {
                    //dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
                    //{
                    object changedItem = this[e.InsertedIndices[0]];
                    var what = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItem, e.InsertedIndices[0]);
                    CollectionChanged(this, what);
                    //});
                }
            });
        }

        public object this[int index] { get => _query.ElementAt(index); set => throw new NotImplementedException(); }

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => true;

        public int Count => _query.Count();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
