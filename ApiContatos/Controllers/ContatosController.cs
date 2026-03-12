using ApiContatos.Data;
using ApiContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ApiContatos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContatosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarContatos()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> BuscarContatoPorId(int id) { 
            var contatoById = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);
            if (contatoById == null) {
                return NotFound("Usuario não encotrado");
            }
            return Ok(contatoById);
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> CriarContato(Contato contato) {
            if (!VerificarEmail(contato.Email!))
            {
                return BadRequest(new { message = "Email invalido" });
            }
            
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BuscarContatoPorId), new { id = contato.Id }, contato);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Contato>> AtualizarContato(int id, Contato contato)
        //{

        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarPorId(int id) { 

            var contatoResult = await _context.Contatos.FirstOrDefaultAsync(i  => i.Id == id);

            if (contatoResult == null) {
                return BadRequest(new { message = "Nenhum contato encontrado" });
            }

            _context.Contatos.Remove(contatoResult);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool VerificarEmail(string email) {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
