using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class DadosPessoaisViewModel
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CargoPretendido { get; set; }
        public int Id_Endereco { get; set; }
    }
}
