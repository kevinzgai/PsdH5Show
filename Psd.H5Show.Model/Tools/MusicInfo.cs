using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psd.H5Show.Model.Tools
{
    public class MusicInfo
    {
        public MusicInfo()
        { }

        public string filename
        { set; get; }
        public string name { set; get; }
        public string musicsize { set; get; }
        public string musicname { set; get; }
        public string singer { set; get; }
        public string album { set; get; }
        public string duration { set; get; }
    }
}
