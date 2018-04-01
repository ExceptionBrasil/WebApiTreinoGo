using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiTreinoGo.Modelos;

namespace WebApiTreinoGo.DAO
{
    public class PedidoContext:DbContext
    {
        public PedidoContext():base("name=local")
        {

        }

        public DbSet<PedidoDeCompras> Pedidos { get; set; } 
    }
}