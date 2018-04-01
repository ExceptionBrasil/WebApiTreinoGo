using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTreinoGo.Modelos
{
    /// <summary>
    /// Classe de modelo de exemplo utilizada para fins de testes 
    /// </summary>
    public class PedidoDeCompras
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DataInclusao { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public bool Aprovado { get; set; }

    }
}