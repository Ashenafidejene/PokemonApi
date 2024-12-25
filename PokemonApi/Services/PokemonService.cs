using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonApi
{
    public class PokemonService : IPokemonService
    {
        private readonly List<Pokemon> _pokemons = new();

        public Task<IEnumerable<Pokemon>> GetAllPokemonsAsync() => Task.FromResult((IEnumerable<Pokemon>)_pokemons);

        public Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            return Task.FromResult(_pokemons.FirstOrDefault(p => p.Id == id));
        }

        public Task AddPokemonAsync(Pokemon pokemon)
        {
            _pokemons.Add(pokemon);
            return Task.CompletedTask;
        }

        public Task<bool> UpdatePokemonAsync(int id, Pokemon updatedPokemon)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return Task.FromResult(false);

            pokemon.Name = updatedPokemon.Name;
            pokemon.Type = updatedPokemon.Type;
            pokemon.Ability = updatedPokemon.Ability;
            pokemon.Level = updatedPokemon.Level;

            return Task.FromResult(true);
        }

        public Task<bool> DeletePokemonAsync(int id)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return Task.FromResult(false);

            _pokemons.Remove(pokemon);
            return Task.FromResult(true);
        }
    }
}