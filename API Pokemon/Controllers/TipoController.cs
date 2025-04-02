using API_Pokemon;
using API_Pokemon.Entidades;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace API_Tipo.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Tipo>> Get()
        {
            return _context.Tipo.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tipo> GetById(int id)
        {
            var Tipo = _context.Tipo.Find(id);

            if (Tipo == null)
            {
                return NotFound();
            }

            return Ok(Tipo);
        }

        [HttpPost]
        public ActionResult<String> PostTipo(Tipo nome)
        {
            try
            {
                _context.Tipo.Add(nome);
                _context.SaveChanges();

                return "Tipo criado com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao criar Classificação" + ex.Message;
            }
        }

        [HttpPut]
        public ActionResult<string> PutTipo(Tipo Nome)
        {
            try
            {
                if (!ExisteTipo(Nome.Id))
                {
                    return "Nenhuma classe encontrado com Id fornecido";
                }
                _context.Tipo.Update(Nome);
                _context.SaveChanges();

                return "Classe alterado com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao alterar Classe" + ex.Message;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeletTipo(int id)
        {
            try
            {
                var Tipo = _context.Tipo.Find(id);
                if (Tipo == null)
                {
                    return "Nenhuma classe encontrado com Id fornecido";
                }

                _context.Tipo.Remove(Tipo);
                _context.SaveChanges();
                return "Classe excluida com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao excluir Classe" + ex.Message;
            }
        }

        private bool ExisteTipo(int id)
        {
            return _context.Tipo.Any(p => p.Id == id);
        }
    }
}
