using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeCurriculos.DAO;
using CadastroDeCurriculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCurriculos.Controllers
{
    public class EnderecoController : Controller
    {
         public IActionResult Index()
        {
            try
            {
                EnderecoDAO dao = new EnderecoDAO();
                List<EnderecoViewModel> lista = dao.Listagem();
                return View("ListEndereco",lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Menu()
        {
            return View("ListEndereco");
        }

        public IActionResult NovoEndereco()
        {
            try
            {
                ViewBag.Operacao = "I";

                EnderecoDAO dao = new EnderecoDAO();
                EnderecoViewModel endereco = new EnderecoViewModel();
                return View("FormEndereco", endereco);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult SalvarEndereco(EnderecoViewModel endereco, string Operacao)
        {
            try
            {
                EnderecoDAO dao = new EnderecoDAO();
                if (Operacao == "I")
                    dao.Inserir(endereco);
                else
                    dao.Alterar(endereco);

                return RedirectToAction("CadFormacaoAcademica");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                EnderecoDAO dao = new EnderecoDAO();
                EnderecoViewModel endereco = dao.Consulta(id);
                if (endereco == null)
                    return RedirectToAction("index");
                else
                    return View("FormEndereco", endereco);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                EnderecoDAO dao = new EnderecoDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }



    }
}
