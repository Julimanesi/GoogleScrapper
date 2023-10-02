using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScrapper
{
    public class YoutubeApi
    {
        public string Busqueda { get; set; } = string.Empty;
        public int maxResults { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string regionCode { get; set; } = string.Empty;
        public SearchResource.ListRequest.VideoDurationEnum videoDuration { get; set; }
        public SearchResource.ListRequest.OrderEnum orden { get; set; }
        public SearchResource.ListRequest.SafeSearchEnum safeSearch { get; set; }
        public SearchResource.ListRequest.VideoCaptionEnum subtitulos { get; set; }
        public SearchResource.ListRequest.VideoDefinitionEnum definicion { get; set; }
        public SearchResource.ListRequest.VideoTypeEnum tipoVideo { get; set; }
        public string CategoriaVideo { get; set; } = string.Empty;
        public string TipoBusqueda { get; set; } = string.Empty;
        public SearchListResponse? ListaRespuesta { get; set; }
        public SearchResource.ListRequest.ChannelTypeEnum TipoCanal { get; set; }
        public string Idioma { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        public YoutubeApi(string busqueda,int maxResults, DateTime fechaInicio, DateTime fechaFin, string regionCode, SearchResource.ListRequest.VideoDurationEnum videoDuration, SearchResource.ListRequest.OrderEnum orden, SearchResource.ListRequest.SafeSearchEnum safeSearch, SearchResource.ListRequest.VideoCaptionEnum subtitulos, SearchResource.ListRequest.VideoDefinitionEnum definicion, SearchResource.ListRequest.VideoTypeEnum tipoVideo, string categoriaVideo,string tipoBusqueda, SearchResource.ListRequest.ChannelTypeEnum tipocanal, string idioma,string pais)
        {
            this.Busqueda = busqueda;
            this.maxResults = maxResults;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.regionCode = regionCode;
            this.videoDuration = videoDuration;
            this.orden = orden;
            this.safeSearch = safeSearch;
            this.subtitulos = subtitulos;
            this.definicion = definicion;
            this.CategoriaVideo = categoriaVideo;
            this.tipoVideo = tipoVideo;
            this.TipoBusqueda = tipoBusqueda;
            TipoCanal = tipocanal;
            Idioma = idioma;
            Pais = pais;
        }

        //private async Task Run(int maxResults ,DateTime fechaInicio ,DateTime fechaFin ,string regionCode,SearchResource.ListRequest.VideoDurationEnum videoDuration, SearchResource.ListRequest.OrderEnum orden, SearchResource.ListRequest.SafeSearchEnum safeSearch, SearchResource.ListRequest.VideoCaptionEnum subtitulos, SearchResource.ListRequest.VideoDefinitionEnum definicion,string categoria)
        public async Task Run()
        {
            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = KeyAndPasswords.YoutubeApiKey,
                    ApplicationName = this.GetType().ToString()
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = Busqueda; // Replace with your search term.
                searchListRequest.Type = TipoBusqueda;
                searchListRequest.MaxResults = this.maxResults;
                searchListRequest.PublishedAfter = this.fechaInicio.ToString("yyyy-MM-ddTHH:mm:ssZ");
                searchListRequest.PublishedBefore = this.fechaFin.ToString("yyyy-MM-ddTHH:mm:ssZ");
                searchListRequest.RegionCode = this.regionCode;
                searchListRequest.RegionCode = Pais;
                if (Idioma != "" && Idioma != "iv")
                    searchListRequest.RelevanceLanguage = Idioma;
                searchListRequest.SafeSearch = this.safeSearch;
                searchListRequest.Order = this.orden;
                if (TipoBusqueda == "video")
                {
                    searchListRequest.VideoDuration = this.videoDuration;
                    searchListRequest.VideoCaption = this.subtitulos;
                    searchListRequest.VideoDefinition = this.definicion;
                    if (this.CategoriaVideo != null && this.CategoriaVideo != "")
                        searchListRequest.VideoCategoryId = this.CategoriaVideo;
                    searchListRequest.VideoType = this.tipoVideo;
                }
                if (TipoBusqueda == "channel")
                    searchListRequest.ChannelType = TipoCanal;
                // Call the search.list method to retrieve results matching the specified query term.
                ListaRespuesta = await searchListRequest.ExecuteAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar videos en Youtube(API)");
            }
        }
    }
}
