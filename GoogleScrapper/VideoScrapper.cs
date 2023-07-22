using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace GoogleScrapper
{
    public class VideoScrapper
    {
        private const string videoURLSelect = "&tbm=vid";
        private static HtmlWeb web = new HtmlWeb();

        public string NombreVideo { get; set; }
        public int IndexDuracion { get; set; }
        public int IndexFecha { get; set; }

        public VideoScrapper(string nombreVideo,int indexDuracion = 0, int indexFecha = 0)
        {
            NombreVideo = nombreVideo;
            IndexDuracion = indexDuracion;
            IndexFecha = indexFecha;
        }

        public List<ResultadoVideo> ObtenerLinksVideos(int pagina = 0)
        {
            List<ResultadoVideo> resultadoVideoList = new List<ResultadoVideo>();
            string request = Form1.URLGoogle + NombreVideo.Replace(" ", "+") + videoURLSelect;

            if (IndexDuracion != 0)
                request += "&tbs=dur:" + GetCodigoDuracion(IndexDuracion);
            if (IndexFecha != 0)
                request += GetCodigoFecha(IndexFecha);
            if (pagina != 0)
                request += "&start=" + pagina * 10;

            var htmlDoc = web.Load(request);
            var htmlNodePosBody = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div");
            if (htmlNodePosBody != null)
            {
                foreach (var anchor in htmlNodePosBody.Descendants("a").Where(x => x.NodeType == HtmlNodeType.Element && x.HasAttributes))// && x.Attributes[0].Value == "egMi0 kCrYT"
                {
                    var href = anchor.Attributes.Where(x => x.Name == "href").FirstOrDefault();
                    if (href != null) {
                        string link = href.Value;
                        var titulo = anchor.InnerText;
                        if (titulo != "")
                        {
                            if (resultadoVideoList.Where(x => x.URLVideo == link).FirstOrDefault() != null)
                            {
                                resultadoVideoList.Where(x => x.URLVideo == link).First().Title+="  Duracion:"+titulo;
                            }
                            resultadoVideoList.Add(new ResultadoVideo(link, titulo));
                        }
                    }
                }
            }
            return resultadoVideoList.DistinctBy(x => x.URLVideo).ToList();
        }

        public int GetNroPaginas(ref Label NumeroPagTotales)
        {
            int nroPaginas = 0;
            string request = Form1.URLGoogle + NombreVideo.Replace(" ", "+") + videoURLSelect;
            if (IndexDuracion != 0)
                request += "&tbs=dur:" + GetCodigoDuracion(IndexDuracion);
            if (IndexFecha != 0)
                request += GetCodigoFecha(IndexFecha);
            var htmlDoc = web.Load(request);
            
            while (htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div/div/div[1]/a") != null)
            {
                htmlDoc = web.Load($"{request}&start={nroPaginas*10}");
                nroPaginas++;
                NumeroPagTotales.Text = $"Numero de paginas encontradas:{nroPaginas}";
            }
            return nroPaginas;
        }

        private string GetCodigoDuracion(int index)
        {
            string resp = "";

            switch (index)
            {
                case 1:
                    resp = "s";
                    break;
                case 2:
                    resp = "m";
                    break;
                case 3:
                    resp = "l";
                    break;
                default:
                    break;
            }

            return resp;
        }

        private string GetCodigoFecha(int index)
        {
            string resp = "";

            switch (index)
            {
                case 1:
                    resp = "&tbs=qdr:h";
                    break;
                case 2:
                    resp = "&tbs=qdr:d";
                    break;
                case 3:
                    resp = "&tbs=qdr:w";
                    break;
                case 4:
                    resp = "&tbs=qdr:m";
                    break;
                case 5:
                    resp = "&tbs=qdr:y";
                    break;
                default:
                    break;
            }

            return resp;
        }
    }
}
