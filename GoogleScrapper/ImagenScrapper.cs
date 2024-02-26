using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScrapper
{
    public class ImagenScrapper
    {
        private const string videoURLSelect = "&tbm=isch";
        private static HtmlWeb web = new HtmlWeb();

        private int IndexTamanio;
        private int IndexColor;
        private int IndexTipo;
        private int IndexFecha;
        private string NombreImagen = "";

        public ImagenScrapper(string nombreImagen, int indexTamanio, int indexColor, int indexTipo, int indexFecha) 
        {
            NombreImagen = nombreImagen;
            IndexTamanio = indexTamanio;
            IndexColor = indexColor;
            IndexTipo = indexTipo;
            IndexFecha = indexFecha;
        }

        public List<ResultadoImagen> ObtenerLinksImagenes(bool desdeURL = false)
        {
            List<ResultadoImagen> resultadoImagenList = new List<ResultadoImagen>();
            string request = desdeURL ? NombreImagen : MainForm.URLGoogle + NombreImagen.Replace(" ", "+") + videoURLSelect;

            if (IndexTamanio > 0 || IndexColor > 0 || IndexTipo > 0 || IndexFecha > 0 )
            {
                request += "&tbs=";
                List<string> tbsParam = new List<string>();
                if (IndexTamanio > 0)
                    tbsParam.Add("isz:" + GetCodigoTamanio(IndexTamanio));
                //if (IndexColor != 0)
                //    tbsParam.Add();
                if (IndexTipo > 0)
                    tbsParam.Add("itp:" + GetCodigoTipo(IndexTipo));
                if (IndexFecha > 0)
                    tbsParam.Add("qdr:" + GetCodigoFecha(IndexFecha));
                request += string.Join("%2C", tbsParam);
            }
            

            var htmlDoc = web.Load(request);
            var htmlNodePosBody = htmlDoc.DocumentNode.SelectSingleNode("/html/body");
            if (htmlNodePosBody != null)
            {
                foreach (var imagen in htmlNodePosBody.Descendants("img").Where(x => x.NodeType == HtmlNodeType.Element && x.HasAttributes))
                {
                    var href = desdeURL ? imagen.Attributes.Where(x => x.Name == "src" || x.Name == "data-src").FirstOrDefault() : imagen.Attributes.Where(x => x.Name == "data-src").FirstOrDefault();
                    if (href != null)
                    {
                        string link = href.Value;
                        resultadoImagenList.Add(new ResultadoImagen() { URL = link });
                    }
                }
            }
            
            return resultadoImagenList.DistinctBy(x => x.URL).ToList();
        }

        private string GetCodigoTamanio(int IndexTamanio)
        {
            string cod = "";
            switch(IndexTamanio)
            {
                case 1: //Grandes
                    cod = "l";
                    break;
                case 2: //medianas
                    cod = "m";
                    break;
                case 3: //Iconos
                    cod = "i";
                    break;
            }
            return cod;
        }

        private string GetCodigoTipo(int IndexTipo)
        {
            string cod = "";
            switch (IndexTipo)
            {
                case 1: //Imagenes prediseñadas
                    cod = "clipart";
                    break;
                case 2: //Dibujo de lineas
                    cod = "lineart";
                    break;
                case 3: //Gif
                    cod = "animated";
                    break;
            }
            return cod;
        }

        private string GetCodigoFecha(int IndexFecha)
        {
            string cod = "";
            switch (IndexFecha)
            {
                case 1: // ultimas 24 horas
                    cod = "d";
                    break;
                case 2: //ultima semana
                    cod = "w";
                    break;
                case 3: //ultimo mes
                    cod = "m";
                    break;
                case 4: //ultimo año
                    cod = "y";
                    break;
            }
            return cod;
        }
    }
}
