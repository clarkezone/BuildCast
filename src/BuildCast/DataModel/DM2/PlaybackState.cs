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
using BuildCast.DataModel.DM2;
using Realms;

namespace BuildCast.DataModel
{
    public class PlaybackState : RealmObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackState"/> class.
        /// Public Constructor required by EF
        /// </summary>
        public PlaybackState()
        {
        }

        public PlaybackState(Episode2 episode)
        {
            this.Episode = episode;
        }

        //TODO BackPointer
        public Episode2 Episode { get; set; }

        public double ListenProgress { get; set; }

        public double GetPercentDouble (Episode2 e)
        {
            return (ListenProgress / e.Duration.TotalMilliseconds) * 100;
        }

        public string GetPercent(Episode2 e)
        {
            return $"{(int)((ListenProgress / e.Duration.TotalMilliseconds) * 100)}%";
        }
    }
}
