using CadastroDeCurriculos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.DAO
{
    public class ExperienciasProfissionaisDAO
    {
        public void Inserir(ExperienciasProfissionaisViewModel dados)
        {
            string sql =
            "insert into ExperienciasProfissionais (id_experienciaProfissional,cod_DadosPessoais,empresa,nome_cargo,data_inicio,data_termino,descricao)" +
            "values(@id_experienciaProfissional, @cod_DadosPessoais, @empresa, @nome_cargo, @data_inicio, @data_termino, @descricao)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }
        public void Alterar(ExperienciasProfissionaisViewModel dados)
        {
            string sql =
            "update ExperienciasProfissionais  set empresa = @empresa, " +
            "nome_cargo = @nome_cargo, " +
            "data_inicio = @data_inicio," +
            "data_termino = @data_termino," +
            "descricao = @descricao where id_experienciaProfissional = @id_experienciaProfissional";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }

        public void Excluir(int IdDados)
        {
            string sql = "delete ExperienciasProfissionais  where id_experienciaProfissional =" + IdDados;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public ExperienciasProfissionaisViewModel Consulta(int Id)
        {
            string sql = "select * from ExperienciasProfissionais  where cod_DadosPessoais = " + Id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaCurriculo(tabela.Rows[0]);
        }
        private SqlParameter[] CriaParametros(ExperienciasProfissionaisViewModel dados)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id_experienciaProfissional", dados.Id);
            parametros[1] = new SqlParameter("cod_DadosPessoais", dados.CodDadosPessoais);
            parametros[2] = new SqlParameter("empresa", dados.Empresa);
            parametros[3] = new SqlParameter("nome_cargo", dados.NomeCargo);
            parametros[4] = new SqlParameter("data_inicio", dados.DataInicio);
            parametros[5] = new SqlParameter("data_termino", dados.DataTermino);
            parametros[6] = new SqlParameter("descricao", dados.Descricao);
            return parametros;
        }

        private ExperienciasProfissionaisViewModel MontaCurriculo(DataRow registro)
        {
            ExperienciasProfissionaisViewModel ep = new ExperienciasProfissionaisViewModel();
            ep.Id = Convert.ToInt32(registro["id_experienciaProfissional"]);
            ep.CodDadosPessoais = Convert.ToInt32(registro["cod_DadosPessoais"].ToString());
            ep.Empresa = registro["empresa"].ToString();
            ep.NomeCargo = registro["nome_cargo"].ToString();
            ep.DataInicio = Convert.ToDateTime(registro["data_inicio"].ToString());
            ep.DataTermino = Convert.ToDateTime(registro["data_termino"].ToString());
            ep.Descricao = registro["descricao"].ToString();
            return ep;
        }
    }
}

