using CadastroDeCurriculos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.DAO
{
    public class ListIndexDAO
    {

        private SqlParameter[] CriaParametrosListagem(ListCurriculosViewModel dados)
        {
            SqlParameter[] parametrosListagem = new SqlParameter[6];
            parametrosListagem[0] = new SqlParameter("CPF", dados.CPF);
            parametrosListagem[1] = new SqlParameter("NOME", dados.Nome);
            parametrosListagem[2] = new SqlParameter("CEP", dados.CEP);
            parametrosListagem[3] = new SqlParameter("RUA", dados.Rua);
            return parametrosListagem;
        }
        public List<ListCurriculosViewModel> ListagemIndex()
        {
            string sql = "select  dp.id_dadosPessoais, dp.CPF, dp.nome, en.CEP, en.rua  from DadosPessoais as dp  JOIN Endereco as en on  dp.id_endereco = en.id_endereco";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<ListCurriculosViewModel> retorno = new List<ListCurriculosViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaLista(registro));
            }

            return retorno;
        }
        private ListCurriculosViewModel MontaLista(DataRow registro)
        {
            ListCurriculosViewModel c = new ListCurriculosViewModel();
            c.ID = Convert.ToInt32(registro["id_dadosPessoais"].ToString());
            c.CPF = registro["CPF"].ToString();
            c.Nome = registro["nome"].ToString();
            c.CEP = registro["CEP"].ToString();
            c.Rua = registro["RUA"].ToString();
            return c;
        }

    }
}
