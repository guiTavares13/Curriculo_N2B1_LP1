using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeCurriculos.Models;

namespace CadastroDeCurriculos.DAO
{
    public class EnderecoDAO
    {
        public void Inserir(EnderecoViewModel endereco)
        {
            string sql =
            "insert into Endereco(cod_endereco,CEP,rua,bairro,cidade,Estado)" +
            "values(@cod_endereco, @cep, @rua, @bairro, @cidade, @estado)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(endereco));
        }
        public void Alterar(EnderecoViewModel endereco)
        {
            string sql =
            "update Endereco set CEP = @CEP, " +
            "rua = @rua, " +
            "bairro = @bairro," +
            "cidade = @cidade," +
            "estado = @estado where cod_endereco = @cod_endereco";
            HelperDAO.ExecutaSQL(sql, CriaParametros(endereco));
        }

        private SqlParameter[] CriaParametros(EnderecoViewModel endereco)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("cod_endereco", endereco.codEndereco);
            parametros[1] = new SqlParameter("CEP", endereco.cep);
            parametros[2] = new SqlParameter("rua", endereco.rua);
            parametros[3] = new SqlParameter("bairro", endereco.bairro);
            parametros[4] = new SqlParameter("cidade", endereco.cidade);
            parametros[5] = new SqlParameter("estado", endereco.estado);
            return parametros;
        }
        public void Excluir(int codEndereco)
        {
            string sql = "delete Endereco where cod_endereco =" + codEndereco;
            HelperDAO.ExecutaSQL(sql, null);
        }
        private EnderecoViewModel MontaEndereco(DataRow registro)
        {
            EnderecoViewModel e = new EnderecoViewModel();
            e.codEndereco = Convert.ToInt32(registro["cod_Endereco"]);
            e.cep = registro["cep"].ToString();
            e.rua = registro["rua"].ToString();
            e.bairro = registro["bairro"].ToString();
            e.cidade = registro["cidade"].ToString();
            e.estado = registro["estado"].ToString();
            return e;
        }

        public EnderecoViewModel Consulta(int codEndereco)
        {
            string sql = "select * from Endereco where cod_endereco = " + codEndereco;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaEndereco(tabela.Rows[0]);
        }

        public List<EnderecoViewModel> Listagem()
        {
            string sql = "select * from Endereco";
            List<EnderecoViewModel> lista = new List<EnderecoViewModel>();
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);


            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                for (int qtd = 0; qtd < tabela.Rows.Count; qtd++)
                    lista.Add(MontaEndereco(tabela.Rows[qtd]));

                return lista;
            }

        }
        public int ProximoCodEndereco()
        {
            string sql = "select isnull(max(cod_endereco) +1, 1) as 'MAIOR' from Endereco";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
