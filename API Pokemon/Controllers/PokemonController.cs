using API_Pokemon.DTO;
using API_Pokemon.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace API_Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Pokemon>> Get()
        {

            var pokemons = _context.Pokemon
                .Include(p => p.Tipo)
                .ToList();
            var pokemonDTO = pokemons.Select(p => new PokemonDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Tipo = p.Tipo.Nome,
            }).ToList();
            return Ok(pokemonDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<Pokemon> GetById(int id)
        {
            var pokemon = _context.Pokemon.Find(id);
           
            if (pokemon == null) {
                return NotFound(); 
            }

            return Ok(pokemon);
        }

        [HttpPost]
        public ActionResult<String> PostPokemon(Pokemon pokemon)
        {
            try
            {
                _context.Pokemon.Add(pokemon);
                _context.SaveChanges();

                return "Pokemon criado com sucesso";
            }
            catch (Exception ex) {
                return "Erro ao criar Pokemon" + ex.Message;
            }
        }

        [HttpPut]
        public ActionResult<string> PutPokemon(Pokemon pokemon) {
            try
            {
                if (!ExistePokemon(pokemon.Id)) {
                    return "Nenhum Pokemon encontrado com Id fornecido";
                }
                _context.Pokemon.Update(pokemon);
                _context.SaveChanges();

                return "Pokemon alterado com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao alterar Pokemon" + ex.Message;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeletPokemon(int id)
        {
            try
            {
                var pokemon = _context.Pokemon.Find(id);
                if (pokemon == null) 
                {
                    return "Nenhum Pokemon encontrado com Id fornecido";
                }

                _context.Pokemon.Remove(pokemon);
                _context.SaveChanges();
                return "Pokemon excluido com sucesso";
            }
            catch (Exception ex) 
            {
                return "Erro ao excluir Pokemon" + ex.Message;
            }
        }

        private bool ExistePokemon(int id) { 
            return _context.Pokemon.Any(p => p.Id == id);
        }
    }
}
