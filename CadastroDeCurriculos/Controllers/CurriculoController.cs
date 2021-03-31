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

            try
            {
                DadosPessoaisDAO dao = new DadosPessoaisDAO();
                return View("Index", dao.Lista());
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }

        public IActionResult NewCurriculo()
        {
            try
            {
                ViewBag.Operacao = "I";

                DadosPessoaisDAO dao = new DadosPessoaisDAO();
                DadosPessoaisViewModel dados = new DadosPessoaisViewModel();
                return View("NewCurriculo", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadFormacaoAcademica()
        {
            try
            {
                ViewBag.Operacao = "I";

                FormacaoAcademicaDAO dao = new FormacaoAcademicaDAO();
                FormacaoAcademicaViewModel dados = new FormacaoAcademicaViewModel();
                return View("CadFormacaoAcademica", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadExperienciaProfissional()
        {
            try
            {
                ViewBag.Operacao = "I";

                ExperienciasProfissionaisDAO dao = new ExperienciasProfissionaisDAO();
                ExperienciasProfissionaisViewModel dados = new ExperienciasProfissionaisViewModel();
                return View("CadExperienciaProfissional", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadCursos()
        {
            try
            {
                ViewBag.Operacao = "I";

                CursosDAO dao = new CursosDAO();
                CursosViewModel dados = new CursosViewModel();
                return View("CadCurso", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
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
