using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class ExperienciasProfissionaisViewModel
    {
        public int Id { get; set; }
        public int CodDadosPessoais { get; set; }
        public string Empresa { get; set; }
        public string NomeCargo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public string Descricao { get; set; }
    }
}