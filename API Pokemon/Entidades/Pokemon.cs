namespace API_Pokemon.Entidades
{
    public class Pokemon
    {
        public int Id { get; set; }
        public int IdTipo { get; set; }
        public string Nome { get; set; }
        public decimal Altura { get; set; }
        public int Experiencia { get; set; }
        public string Genero { get; set; }
        public Tipo Tipo { get; set; }  
    }
}
