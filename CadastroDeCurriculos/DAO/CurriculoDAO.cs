using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeCurriculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCurriculos.DAO
{
    public class CurriculoDAO 
    {
        public List<ListCurriculosViewModel> ListarCurriculos()
        {
            string sql = "select d.CPF,d.nome,e.CEP,e.rua from DadosPessoais d inner join Endereco e on d.id_endereco = e.id_endereco";
            List<ListCurriculosViewModel> lista = new List<ListCurriculosViewModel>();
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);


            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                for (int qtd = 0; qtd < tabela.Rows.Count; qtd++)
                    lista.Add(MontaListCurriculo(tabela.Rows[qtd]));

                return lista;
            }
        }

        private ListCurriculosViewModel MontaListCurriculo(DataRow registro)
        {
            ListCurriculosViewModel l = new ListCurriculosViewModel();
            l.CPF = registro["CPF"].ToString();
            l.Nome = registro["Nome"].ToString();
            l.CEP = registro["CEP"].ToString();
            l.Rua = registro["Rua"].ToString();
            return l;
        }
    }
}
