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
using Realms;
using Windows.UI.Xaml;

namespace BuildCast.DataModel.DM2
{
    public class ItemSource<T> : INotifyCollectionChanged, IList, IDisposable
        where T : RealmObject
    {
        private IQueryable<T> _query;

        private IDisposable changeMonitor;

        public ItemSource(IQueryable<T> query)
        {
            _query = query;

            var dispatcher = Window.Current.Dispatcher;

            _query.AsRealmCollection().CollectionChanged += ItemSource_CollectionChanged;
        }

        private void ItemSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged(this, e);
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

        public void Dispose()
        {
            changeMonitor.Dispose();
        }

        public IEnumerator GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            // TODO: this is expensive, is there a cheaper way?
            return _query.ToList().IndexOf((T)value);
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
