using CadastroDeCurriculos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.DAO
{
    public class CursosDAO
    {

        public void Inserir(CursosViewModel curso)
        {
            string sql =
            "insert into Cursos(id_curso,cod_DadosPessoais,nome_curso,instituicao,qtd_horas)" +
            "values(@id_curso, @cod_DadosPessoais, @nome_curso, @instituicao, @qtd_horas)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(curso));
        }
        public void Alterar(CursosViewModel curso)
        {
            string sql =
            "update Cursos set nome_curso = @nome_curso, " +
            "instituicao = @instituicao, " +
            "qtd_horas = @qtd_horas where id_curso = @id_curso";
            HelperDAO.ExecutaSQL(sql, CriaParametros(curso));
        }

        public void Excluir(int idcurso)
        {
            string sql = "delete Cursos where id_curso =" + idcurso;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public CursosViewModel Consulta(int idcurso)
        {
            string sql = "select * from Cursos where id_curso = " + idcurso;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaCurriculo(tabela.Rows[0]);
        }
        private SqlParameter[] CriaParametros(CursosViewModel curso)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id_curso", curso.Id);
            parametros[1] = new SqlParameter("cod_DadosPessoais", curso.CodDadosPessoais);
            parametros[2] = new SqlParameter("nome_curso", curso.NomeCurso);
            parametros[3] = new SqlParameter("instituicao", curso.Instituicao);
            parametros[4] = new SqlParameter("qtd_horas", curso.QuantHoras);
            return parametros;
        }

        private CursosViewModel MontaCurriculo(DataRow registro)
        {
            CursosViewModel formacao = new CursosViewModel();
            formacao.Id = Convert.ToInt32(registro["id_curso"]);
            formacao.CodDadosPessoais = Convert.ToInt32(registro["cod_DadosPessoais"]);
            formacao.NomeCurso = registro["nome_curso"].ToString();
            formacao.Instituicao = registro["instituicao"].ToString();
            formacao.QuantHoras = Convert.ToInt32(registro["qtd_horas"].ToString());
            return formacao;
        }
    }
}
