using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public PokemonService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PokemonDatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config["PokemonDatabaseSettings:DatabaseName"]);
            _pokemons = database.GetCollection<Pokemon>(config["PokemonDatabaseSettings:PokemonCollectionName"]);
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync()
        {
            return await _pokemons.Find(_ => true).ToListAsync();
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(string id)
        {
            return await _pokemons.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pokemon?> GetPokemonByTypeAsync(string type)
        {
            return await _pokemons.Find(p => p.Type == type).FirstOrDefaultAsync();
        }

        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            await _pokemons.InsertOneAsync(pokemon);
        }

        public async Task<bool> UpdatePokemonAsync(string id, Pokemon updatedPokemon)
        {
            var result = await _pokemons.ReplaceOneAsync(p => p.Id == id, updatedPokemon);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeletePokemonAsync(string id)
        {
            var result = await _pokemons.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}