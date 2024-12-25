namespace PokemonApi
{
    public class Pokemon
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Ability { get; set; }
        public int Level { get; set; }
    }
}