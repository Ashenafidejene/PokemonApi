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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetAll()
        {
            return Ok(await _pokemonService.GetAllPokemonsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<ActionResult> AddPokemon([FromBody] Pokemon pokemon)
        {
            await _pokemonService.AddPokemonAsync(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePokemon(int id, [FromBody] Pokemon updatedPokemon)
        {
            var result = await _pokemonService.UpdatePokemonAsync(id, updatedPokemon);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePokemon(int id)
        {
            var result = await _pokemonService.DeletePokemonAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}