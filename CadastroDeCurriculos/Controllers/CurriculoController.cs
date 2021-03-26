using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeCurriculos.DAO;
using CadastroDeCurriculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCurriculos.Controllers
{
    public class CurriculoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Menu(string id)
        {
            return View(id);
        }

        public IActionResult ListarCurriculos()
        {
            try
            {
                CurriculoDAO dao = new CurriculoDAO();
                List<ListCurriculosViewModel> lista = dao.ListarCurriculos();
                return View("ListCurriculo", lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
