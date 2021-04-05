﻿using CadastroDeCurriculos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.DAO
{
    public class DadosPessoaisDAO
    {

        public void Inserir(DadosPessoaisViewModel dados)
        {
            string sql =
            "insert into DadosPessoais(id_dadosPessoais, CPF,nome,telefone,email,cargo_pretendido,id_Endereco)" +
            "values(@id_dadosPessoais, @CPF, @nome, @telefone, @email, @cargo_pretendido,@Id_Endereco)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }
        public void Alterar(DadosPessoaisViewModel dados)
        {
            string sql =
            "update DadosPessoais set CPF = @CPF, " +
            "nome = @nome, " +
            "telefone = @telefone, " +
            "email = @email, " +
            "id_endereco = @Id_endereco, " +
            "cargo_pretendido = @cargo_pretendido where id_dadosPessoais = @id_dadosPessoais";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }

        public void Excluir(int IdDados)
        {
            string sql = "delete DadosPessoais where id_dadosPessoais =" + IdDados;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public DadosPessoaisViewModel Consulta(int Id)
        {
            string sql = "select * from DadosPessoais where id_dadosPessoais = " + Id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaCurriculo(tabela.Rows[0]);
        }

        private SqlParameter[] CriaParametros(DadosPessoaisViewModel dados)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id_dadosPessoais", dados.Id);
            parametros[1] = new SqlParameter("CPF", dados.Cpf);
            parametros[2] = new SqlParameter("nome", dados.Nome);
            parametros[3] = new SqlParameter("telefone", dados.Telefone);
            parametros[4] = new SqlParameter("email", dados.Email);
            parametros[5] = new SqlParameter("cargo_pretendido", dados.CargoPretendido);
            parametros[6] = new SqlParameter("id_Endereco", dados.Id_Endereco);
            return parametros;
        }

        private DadosPessoaisViewModel MontaCurriculo(DataRow registro)
        {
            DadosPessoaisViewModel c = new DadosPessoaisViewModel();
            c.Id = Convert.ToInt32(registro["id_dadosPessoais"].ToString());
            c.Cpf = registro["CPF"].ToString();
            c.Nome = registro["nome"].ToString();
            c.Telefone = registro["telefone"].ToString();
            c.Email = registro["email"].ToString();
            c.CargoPretendido = registro["cargo_pretendido"].ToString();
            c.Id_Endereco = Convert.ToInt32(registro["id_Endereco"].ToString());
            return c;
        }

        public List<DadosPessoaisViewModel> Lista()
        {
            string sql = "select * from DadosPessoais";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<DadosPessoaisViewModel> retorno = new List<DadosPessoaisViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaCurriculo(registro));
            }

            return retorno;
        }
    }
}
