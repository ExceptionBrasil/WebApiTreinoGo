using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTreinoGo.DAO;

namespace WebApiTreinoGo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            using(PedidoContext contexto = new PedidoContext())
            {
                var lista = contexto.Pedidos.ToList();
                return View(lista);
            }
           
        }
    }
}
