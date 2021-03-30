using CadastroDeCurriculos.Models;
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
            "insert into DadosPessoais(Id, CPF,nome,telefone,email,cargo_pretendido)" +
            "values(@Id, @CPF, @nome, @telefone, @email, @cargo_pretendido)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }
        public void Alterar(DadosPessoaisViewModel dados)
        {
            string sql =
            "update DadosPessoais set CPF = @CPF, " +
            "nome = @nome, " +
            "telefone = @telefone," +
            "email = @email," +
            "cargo_pretendido = @cargo_pretendido where Id = @Id";
            HelperDAO.ExecutaSQL(sql, CriaParametros(dados));
        }

        public void Excluir(int IdDados)
        {
            string sql = "delete DadosPessoais where Id =" + IdDados;
            HelperDAO.ExecutaSQL(sql, null);
        }

        public DadosPessoaisViewModel Consulta(int Id)
        {
            string sql = "select * from DadosPessoais where Id = " + Id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaCurriculo(tabela.Rows[0]);
        }

        private SqlParameter[] CriaParametros(DadosPessoaisViewModel dados)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("Id", dados.Id);
            parametros[1] = new SqlParameter("CPF", dados.Cpf);
            parametros[2] = new SqlParameter("nome", dados.Nome);
            parametros[3] = new SqlParameter("telefone", dados.Telefone);
            parametros[4] = new SqlParameter("email", dados.Email);
            parametros[5] = new SqlParameter("cargo_pretendido", dados.CargoPretendido);
            return parametros;
        }

        private DadosPessoaisViewModel MontaCurriculo(DataRow registro)
        {
            DadosPessoaisViewModel c = new DadosPessoaisViewModel();
            c.Id = Convert.ToInt32(registro["Id"]);
            c.Cpf = registro["CPF"].ToString();
            c.Nome = registro["nome"].ToString();
            c.Telefone = registro["telefone"].ToString();
            c.Email = registro["email"].ToString();
            c.CargoPretendido = registro["cargo_pretendido"].ToString();
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
