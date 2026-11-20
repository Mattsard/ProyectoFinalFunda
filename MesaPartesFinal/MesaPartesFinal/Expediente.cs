using System;
using System.Collections.Generic;
using System.Text;

namespace MesaPartes
{
    public class Expediente
    {
        public string Codigo { get; private set; }
        public string Solicitante { get; private set; }
        public string TipoDocumento { get; private set; }
        public string Asunto { get; private set; }
        public string Fecha { get; private set; }
        public string Estado { get; private set; }

        public Expediente(string codigo, string solicitante, string tipoDocumento,
                          string asunto, string fecha, string estado)
        {
            Codigo = codigo;
            Solicitante = solicitante;
            TipoDocumento = tipoDocumento;
            Asunto = asunto;
            Fecha = fecha;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"({Codigo}) -> {Solicitante} - {TipoDocumento} - {Asunto} - {Fecha} - {Estado}";
        }
    }
}