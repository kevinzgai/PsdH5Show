using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psd.H5Show.Model.Tools
{
    public class kitmodel
    {
        public List<KitList> KitList { get; set; }
        public string name { get; set; }
        public kitmodel()
        {
            KitList = new List<Tools.KitList>();

        }

    }

    public class KitList
    {
        public string etype { get; set; }
        public string Kid { get; set; }
        public string link { get; set; }
        public string effect { get; set; }
        public string duration { get; set; }
        public string delay { get; set; }
        public string img { get; set; }
        public int angleZ { get; set; }
        public style style { get; set; }
    }
    public class style
    {
        public string top { get; set; }
        public string left { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }
}
