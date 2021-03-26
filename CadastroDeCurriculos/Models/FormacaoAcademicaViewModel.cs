using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class FormacaoAcademicaViewModel
    {
        public int Id { get; set; }
        public int Cod_DadosPessoais { get; set }
        public string NomeInstituicao { get; set; }
        public string Tipo { get; set; }
        public string Curso { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
    }
}
