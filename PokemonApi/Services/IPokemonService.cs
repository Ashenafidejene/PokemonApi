using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync();
        Task<Pokemon?> GetPokemonByIdAsync(string id);
        Task<Pokemon?> GetPokemonByTypeAsync(string type);
        Task AddPokemonAsync(Pokemon pokemon);
        Task<bool> UpdatePokemonAsync(string id, Pokemon updatedPokemon);
        Task<bool> DeletePokemonAsync(string id);
    }
}