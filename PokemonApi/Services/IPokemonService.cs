using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync();
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task AddPokemonAsync(Pokemon pokemon);
        Task<bool> UpdatePokemonAsync(int id, Pokemon updatedPokemon);
        Task<bool> DeletePokemonAsync(int id);
    }
}