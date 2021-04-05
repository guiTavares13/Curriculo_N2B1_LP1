﻿using CadastroDeCurriculos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.DAO
{
    public class FormacaoAcademicaDAO
    {

        public int ProximoId()
        {
            string sql = "select isnull(max(id_formacaoAcademica) +1, 1) as 'MAIOR' from FormacaoAcademicas";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
        public void Inserir(FormacaoAcademicaViewModel formacao)
        {
            string sql =
            "insert into FormacaoAcademicas(id_formacaoAcademica,cod_DadosPessoais,nome_instituicao,tipo,curso,data_inicio, data_termino)" +
            "values(@id_formacaoAcademica, @cod_DadosPessoais,@nome_instituicao, @tipo, @curso, @data_inicio, @data_termino)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(formacao));
        }
        public void Alterar(FormacaoAcademicaViewModel formacao)
        {
            string sql =
            "update FormacaoAcademicas set nome_instituicao = @nome_instituicao, " +
            "tipo = @tipo, " +
            "curso = @curso," +
            "data_inicio = @data_inicio," +
            "data_termino = @data_termino where id_formacaoAcademica = @id_formacaoAcademica";
            HelperDAO.ExecutaSQL(sql, CriaParametros(formacao));
        }

        public void Excluir(int idformacao)
        {
            string sql = "delete FormacaoAcademicas where cod_DadosPessoais =" + idformacao;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public FormacaoAcademicaViewModel Consulta(int idformacao)
        {
            string sql = "select * from FormacaoAcademicas where cod_DadosPessoais = " + idformacao;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaFormacao(tabela.Rows[0]);
        }
        private SqlParameter[] CriaParametros(FormacaoAcademicaViewModel formacao)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id_formacaoAcademica", formacao.Id);
            parametros[1] = new SqlParameter("cod_DadosPessoais", formacao.Cod_DadosPessoais);
            parametros[2] = new SqlParameter("nome_instituicao", formacao.NomeInstituicao);
            parametros[3] = new SqlParameter("tipo", formacao.Tipo);
            parametros[4] = new SqlParameter("curso", formacao.Curso);
            parametros[5] = new SqlParameter("data_inicio", formacao.DataInicio.ToShortDateString());
            parametros[6] = new SqlParameter("data_termino", formacao.DataTermino.ToShortDateString());
            return parametros;
        }

        private FormacaoAcademicaViewModel MontaFormacao(DataRow registro)
        {
            FormacaoAcademicaViewModel formacao = new FormacaoAcademicaViewModel();
            formacao.Id = Convert.ToInt32(registro["id_formacaoAcademica"]);
            formacao.Cod_DadosPessoais = Convert.ToInt32(registro["cod_DadosPessoais"]);
            formacao.NomeInstituicao = registro["nome_instituicao"].ToString();
            formacao.Tipo = registro["tipo"].ToString();
            formacao.Curso = registro["curso"].ToString();
            formacao.DataInicio = Convert.ToDateTime(registro["data_inicio"].ToString());
            formacao.DataTermino = Convert.ToDateTime(registro["data_termino"].ToString());
            return formacao;
        }
    }
}
