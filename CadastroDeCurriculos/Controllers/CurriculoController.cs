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
                DadosPessoaisViewModel dados = new DadosPessoaisViewModel();
                dados.Id = dao.ProximoId();
                return View("NewCurriculo", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadFormacaoAcademica(FormacaoAcademicaViewModel formacao, int IdDadosPessoais)
        {

            try
            {
                if (formacao == null)
                {
                    FormacaoAcademicaDAO dao = new FormacaoAcademicaDAO();
                    formacao.Id = dao.ProximoId();
                    return View("CadFormacaoAcademica");
                }
                else
                    return View("CadFormacaoAcademica", formacao);


            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadExperienciaProfissional(ExperienciasProfissionaisViewModel experiencia)
        {

            try
            {
                if (experiencia == null)
                {
                    ExperienciasProfissionaisDAO dao = new ExperienciasProfissionaisDAO();
                    experiencia.Id = dao.ProximoId();
                    return View("CadExperienciaProfissional");
                }
                else
                    return View("CadExperienciaProfissional");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult CadCursos(CursosViewModel curso)
        {
            try
            {
                if (curso == null)
                {
                    CursosDAO dao = new CursosDAO();
                    curso.Id = dao.ProximoId();
                    return View("CadCursos");
                }
                else
                    return View("CadCursos", curso);

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

        public IActionResult SalvarDadosPessoais(DadosPessoaisViewModel dados, string Operacao, EnderecoViewModel dadoEnd, int IdDadosPessoais)
        {
            try
            {
                ValidaDados(dados, Operacao);
                if (ModelState.IsValid)
                {
                    DadosPessoaisDAO dao = new DadosPessoaisDAO();
                    EnderecoViewModel end = new EnderecoViewModel();
                    EnderecoDAO daoEnd = new EnderecoDAO();
                    if (Operacao == "I")
                    {
                        ViewBag.IdDadosPessoais = dados.Id;
                        ViewBag.Operacao = "I";
                        FormacaoAcademicaDAO daoformacao = new FormacaoAcademicaDAO();
                        FormacaoAcademicaViewModel formacao = new FormacaoAcademicaViewModel();
                        dao.Inserir(dados);
                        formacao.Id = daoformacao.ProximoId();
                        return View("CadFormacaoAcademica",formacao);
                    }
                    else
                    {
                        ViewBag.Operacao = "A";
                        dao.Alterar(dados);
                        dadoEnd = daoEnd.Consulta(dados.Id_Endereco);
                        ViewBag.IdDadosPessoais = IdDadosPessoais;
                        return View("NewEndereco", dadoEnd);
                    }
                    //  end.idEndereco = (new EnderecoDAO().MesmoId());
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

        public IActionResult SalvarFormacaoAcademica(FormacaoAcademicaViewModel formacao, string Operacao, int IdDadosPessoais)
        {
            try
            {
                CursosDAO daoCurso = new CursosDAO();
                CursosViewModel curso = daoCurso.Consulta(IdDadosPessoais);
                FormacaoAcademicaDAO dao = new FormacaoAcademicaDAO();
                if (Operacao == "I")
                {
                    CursosViewModel cursos = new CursosViewModel();
                    ViewBag.Operacao = "I";
                    ViewBag.IdDadosPessoais = IdDadosPessoais;
                    dao.Inserir(formacao);
                    cursos.Id = daoCurso.ProximoId();
                    return View("CadCursos", cursos);
                }
                else
                {
                    ViewBag.Operacao = "A";
                    dao.Alterar(formacao);
                }
                ViewBag.IdDadosPessoais = IdDadosPessoais;
                return View("CadCursos", curso);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


        public IActionResult SalvarEndereco(EnderecoViewModel endereco, string Operacao, int IdDadosPessoais)
        {
            try
            {
                FormacaoAcademicaDAO daoFormacao = new FormacaoAcademicaDAO();
                FormacaoAcademicaViewModel formacao = daoFormacao.Consulta(IdDadosPessoais);
                EnderecoDAO dao = new EnderecoDAO();
                if (Operacao == "I")
                {
                    ViewBag.Operacao = "I";
                    dao.Inserir(endereco);
                }
                else
                {
                    ViewBag.Operacao = "A";
                    dao.Alterar(endereco);
                }
                ViewBag.IdDadosPessoais = IdDadosPessoais;
                return View("CadFormacaoAcademica", formacao);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult SalvarCurso(CursosViewModel curso, string Operacao, int IdDadosPessoais)
        {
            try
            {
                ExperienciasProfissionaisDAO daoExperiencia = new ExperienciasProfissionaisDAO();
                ExperienciasProfissionaisViewModel experiencia = daoExperiencia.Consulta(IdDadosPessoais);
                CursosDAO dao = new CursosDAO();
                if (Operacao == "I")
                {
                    ExperienciasProfissionaisViewModel experiencias = new ExperienciasProfissionaisViewModel();
                    ViewBag.Operacao = "I";
                    dao.Inserir(curso);
                    experiencias.Id = daoExperiencia.ProximoId();
                    return View("CadExperienciaProfissional", experiencias);
                }
                else
                {
                    ViewBag.Operacao = "A";
                    dao.Alterar(curso);
                }
                ViewBag.IdDadosPessoais = IdDadosPessoais;
                return View("CadExperienciaProfissional", experiencia);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult SalvarExperienciaProfissional(ExperienciasProfissionaisViewModel experiencia, string Operacao)
        {
            try
            {
                ExperienciasProfissionaisDAO dao = new ExperienciasProfissionaisDAO();
                if (Operacao == "I")
                {
                    ViewBag.Operacao = "I";
                    dao.Inserir(experiencia);
                }
                else
                {
                    ViewBag.Operacao = "A";
                    dao.Alterar(experiencia);
                }
                return RedirectToAction("Index");
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
                ViewBag.IdDadosPessoais = id;
                // PreparaListaCidadesParaCombo();

                DadosPessoaisDAO dao = new DadosPessoaisDAO();
                DadosPessoaisViewModel dadosPessoais = dao.Consulta(id);
                if (dadosPessoais == null)
                    return RedirectToAction("index");
                else
                    return View("NewCurriculo", dadosPessoais);
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
                DadosPessoaisDAO daoDadosPessoais = new DadosPessoaisDAO();
                CursosDAO daoCursos = new CursosDAO();
                ExperienciasProfissionaisDAO daoExperiencias = new ExperienciasProfissionaisDAO();
                FormacaoAcademicaDAO daoFormacao = new FormacaoAcademicaDAO();
                daoDadosPessoais.Excluir(id);
                daoCursos.Excluir(id);
                daoExperiencias.Excluir(id);
                daoFormacao.Excluir(id);
                
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
