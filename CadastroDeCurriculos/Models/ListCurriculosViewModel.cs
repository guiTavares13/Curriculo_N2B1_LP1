using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class ListCurriculosViewModel
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
    }
}
