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
                ListIndexDAO dao = new ListIndexDAO();
                return View("Index", dao.ListagemIndex());
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
                CurriculoDAO daoCurriculo = new CurriculoDAO();
                DadosPessoaisViewModel dados = new DadosPessoaisViewModel();
             // dados.Id = (new CurriculoDAO()).ProximoId();
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


        private void ValidaDados(DadosPessoaisViewModel dadosPessoais, string operacao)
        {
            /*
            ModelState.Remove("Id");
            ModelState.Remove("CidadeId");
            ModelState.Remove("Mensalidade");
            ModelState.Remove ("DataNascimento");
            */
            ModelState.Clear();

            DadosPessoaisDAO dao = new DadosPessoaisDAO();
            if (operacao == "I" && dao.Consulta(dadosPessoais.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso.");
            if (operacao == "A" && dao.Consulta(dadosPessoais.Id) == null)
                ModelState.AddModelError("Id", "Currículo não existe.");
            if (dadosPessoais.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            if (string.IsNullOrEmpty(dadosPessoais.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
            if (string.IsNullOrEmpty(dadosPessoais.Cpf))
                ModelState.AddModelError("CPF", "Campo obrigatório.");
            if (string.IsNullOrEmpty(dadosPessoais.Email)) 
                ModelState.AddModelError("Email", "Informe o e-email.");
            if (string.IsNullOrEmpty(dadosPessoais.CargoPretendido))
                ModelState.AddModelError("CargoPretendido", "Informe um cargo pretendido!");
        }

        public IActionResult SalvarDadosPessoais(DadosPessoaisViewModel dados, string Operacao, EnderecoViewModel dadoEnd)
        {
            try
            {
                // ValidaDados(dados, Operacao);
                if (ModelState.IsValid)
                {
                    DadosPessoaisDAO dao = new DadosPessoaisDAO();
                    EnderecoViewModel end = new EnderecoViewModel();
                    EnderecoDAO daoEnd = new EnderecoDAO();
                    if (Operacao == "I")
                        dao.Inserir(dados);
                    else
                        dao.Alterar(dados);
                  //  end.idEndereco = (new EnderecoDAO().MesmoId());
                    return View("NewEndereco", dadoEnd);
                    }
                else
                {
                    ViewBag.Operacao = Operacao;
                    //PreparaListaCidadesParaCombo();
                    return View("Form", dados);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult SalvarFormacaoAcademica(FormacaoAcademicaViewModel formacao, string Operacao)
        {
            try
            {
                FormacaoAcademicaDAO dao = new FormacaoAcademicaDAO();
                // if (Operacao == "I")
                dao.Inserir(formacao);
                //  else
                //  dao.Alterar(endereco);

                return RedirectToAction("CadFormacaoAcademica");
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
               // if (Operacao == "I")
                    dao.Inserir(endereco);
              //  else
                  //  dao.Alterar(endereco);

                return RedirectToAction("CadFormacaoAcademica");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }





    }
}
