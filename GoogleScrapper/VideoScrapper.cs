using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public bool AltaCalidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public VideoScrapper(string nombreVideo, int indexDuracion = 0, int indexFecha = 0, bool altaCalidad = false, DateTime fechaInicio = new DateTime(), DateTime fechaFin = new DateTime())
        {
            NombreVideo = nombreVideo;
            IndexDuracion = indexDuracion;
            IndexFecha = indexFecha;
            AltaCalidad = altaCalidad;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public List<ResultadoVideo> ObtenerLinksVideos(int pagina = 0)
        {
            List<ResultadoVideo> resultadoVideoList = new List<ResultadoVideo>();
            string request = MainForm.URLGoogle + NombreVideo.Replace(" ", "+") + videoURLSelect;

            if (IndexDuracion != 0 || IndexFecha != 0 || AltaCalidad)
            {
                request += "&tbs=";
                List<string> tbsParam = new List<string>();
                if (IndexDuracion != 0)
                    tbsParam.Add("dur:" + GetCodigoDuracion(IndexDuracion));
                if (IndexFecha != 0)
                    tbsParam.Add(GetCodigoFecha(IndexFecha));
                if (AltaCalidad)
                    tbsParam.Add("hq:h");
                request += string.Join(",", tbsParam);
            }
            if (pagina != 0)
                request += "&start=" + pagina * 10;

            var htmlDoc = web.Load(request);
            var htmlNodePosBody = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div");
            if (htmlNodePosBody != null)
            {
                foreach (var anchor in htmlNodePosBody.Descendants("a").Where(x => x.NodeType == HtmlNodeType.Element && x.HasAttributes))// && x.Attributes[0].Value == "egMi0 kCrYT"
                {
                    var href = anchor.Attributes.Where(x => x.Name == "href").FirstOrDefault();
                    if (href != null)
                    {
                        string link = href.Value;
                        var titulo = anchor.InnerText;
                        if (titulo != "" && link.Contains("http"))
                        {
                            link = link.Substring(link.IndexOf("http"));
                            if (resultadoVideoList.Where(x => x.URLVideo == link).FirstOrDefault() != null)
                            {
                                resultadoVideoList.Where(x => x.URLVideo == link).First().Title += "  Duracion:" + titulo;
                            }
                            resultadoVideoList.Add(new ResultadoVideo(link, titulo));
                        }
                    }
                }
            }
            resultadoVideoList = resultadoVideoList.Where(x=>!x.URLVideo.Contains("support.google.com")).ToList();
            return resultadoVideoList.DistinctBy(x => x.URLVideo).ToList();
        }

        public int GetNroPaginas(BackgroundWorker worker)
        {
            int nroPaginas = 1;
            string request = MainForm.URLGoogle + NombreVideo.Replace(" ", "+") + videoURLSelect;

            if (IndexDuracion != 0 || IndexFecha != 0 || AltaCalidad)
            {
                request += "&tbs=";
                List<string> tbsParam = new List<string>();
                if (IndexDuracion != 0)
                    tbsParam.Add("dur:" + GetCodigoDuracion(IndexDuracion));
                if (IndexFecha != 0)
                    tbsParam.Add(GetCodigoFecha(IndexFecha));
                if (AltaCalidad)
                    tbsParam.Add("hq:h");
                request += string.Join(",", tbsParam);
            }

            var htmlDoc = web.Load(request);

            while (htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div/div/div[1]/a") != null || htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[4]/div/div/div[1]/a") != null || htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/footer/div[1]/div/div/a") != null)
            {
                htmlDoc = web.Load($"{request}&start={nroPaginas * 10}");
                nroPaginas++;
                worker.ReportProgress(0, new ProgresoTotal(0, 0, nroPaginas));
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
                    resp = "qdr:h";
                    break;
                case 2:
                    resp = "qdr:d";
                    break;
                case 3:
                    resp = "qdr:w";
                    break;
                case 4:
                    resp = "qdr:m";
                    break;
                case 5:
                    resp = "qdr:y";
                    break;
                case 6:
                    resp = $"cdr:1,cd_min:{FechaInicio.ToString("M/d/yyyy")},cd_max:{FechaFin.ToString("M/d/yyyy")}";//cdr:1,cd_min:2/2/2023,cd_max:7/21/2023
                    break;
                default:
                    break;
            }

            return resp;
        }
    }
}
