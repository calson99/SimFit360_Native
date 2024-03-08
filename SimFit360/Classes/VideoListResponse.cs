using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimFit360.SportPage;

namespace SimFit360.Classes
{
    internal class VideoListResponse
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public List<VideoItem> Items { get; set; }
    }
}
