namespace Locacoes.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public List<Modelo>? Modelos { get; set; }
    }
}
