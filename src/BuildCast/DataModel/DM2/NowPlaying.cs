using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace BuildCast.DataModel.DM2
{
    class NowPlaying : RealmObject
    {
        public Episode2 Episode { get; set; }
    }
}
