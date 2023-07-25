using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScrapper
{
    public class ProgresoTotal
    {
        public int NroResultadosActuales { get; set; }
        public int NroPaginasVisitadas { get; set; }
        public int NroPaginasEncontradas { get; set; }

        public ProgresoTotal(int nroResultadosActuales, int nroPaginasVisitadas, int nroPaginasEncontradas)
        {
            NroResultadosActuales = nroResultadosActuales;
            NroPaginasVisitadas = nroPaginasVisitadas;
            NroPaginasEncontradas = nroPaginasEncontradas;
        }
    }
}
