using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimFit360.SportPage;

namespace SimFit360.Classes
{
    internal class VideoItem
    {
        public string Id { get; set; }
        public string Kind { get; set; }
        public string Etag { get; set; }
        public ContentDetails ContentDetails { get; set; }
    }
}
