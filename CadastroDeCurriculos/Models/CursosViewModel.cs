using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class CursosViewModel
    {
        public int Id { get; set; }
        public int CodDadosPessoais { get; set; }
        public string NomeCurso { get; set; }
        public string Instituicao { get; set; }
        public int QuantHoras { get; set; }
    }
}
