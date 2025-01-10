using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // Get all Pokemons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetAll()
        {
            var pokemons = await _pokemonService.GetAllPokemonsAsync();
            return Ok(pokemons);
        }

        // Get a Pokemon by ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Pokemon>> GetById(string id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null) return NotFound("Pokemon not found.");
            return Ok(pokemon);
        }

        // Get a Pokemon by Type
        [HttpGet("search/{type}")]
        public async Task<ActionResult<Pokemon>> GetByType(string type)
        {
            var pokemon = await _pokemonService.GetPokemonByTypeAsync(type);
            if (pokemon == null) return NotFound("Pokemon of the specified type not found.");
            return Ok(pokemon);
        }

        // Add a new Pokemon
        [HttpPost]
        public async Task<ActionResult> AddPokemon([FromBody] Pokemon pokemon)
        {
            await _pokemonService.AddPokemonAsync(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
        }

        // Update an existing Pokemon by ID
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdatePokemon(string id, [FromBody] Pokemon updatedPokemon)
        {
            var result = await _pokemonService.UpdatePokemonAsync(id, updatedPokemon);
            if (!result) return NotFound("Pokemon not found for update.");
            return NoContent();
        }

        // Delete a Pokemon by ID
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeletePokemon(string id)
        {
            var result = await _pokemonService.DeletePokemonAsync(id);
            if (!result) return NotFound("Pokemon not found for deletion.");
            return NoContent();
        }
    }
}