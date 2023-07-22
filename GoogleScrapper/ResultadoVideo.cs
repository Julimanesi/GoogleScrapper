using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScrapper
{
    public class ResultadoVideo
    {
        public string URLVideo { get; set; }
        public string Title { get; set; }

        public ResultadoVideo(string uRLVideo, string title)
        {
            URLVideo = uRLVideo;
            Title = title;
        }
    }
}
