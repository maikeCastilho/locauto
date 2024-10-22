using System.ComponentModel;

namespace Locacoes.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public DateOnly DataLocacao { get; set; }
        public decimal ValorTotal { get; set; }
        [DisplayName("Cliente")]

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
        public List<VeiculoLocado>? VeiculosLocados { get; set; }
    }
}
