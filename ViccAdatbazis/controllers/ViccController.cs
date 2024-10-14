using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : Controller
    {
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }


        //Viccek listázása
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv).ToListAsync();
        }

        //Vicc lekérése
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GettVicc(int id)
        {
            var vicc = _context.Viccek.Find(id);
            if (vicc == null) { return NotFound(); }
            return vicc;
        }


        //Vicc feltöltése
        [HttpPost]

        public async Task<ActionResult> PostVicc(Vicc feltoltottVicc)
        {
            _context.Viccek.Add(feltoltottVicc);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        //Vicc módosítása
        [HttpPost("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc modositottVicc)
        {
            //var vicc = _context.Viccek.Find(id);
            if (id != modositottVicc.Id)
            {
                return BadRequest();
            }
            _context.Entry(modositottVicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
            
        }

        //Vicc törlése
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {

            var torlendoVicc = _context.Viccek.Find(id);
            if (torlendoVicc == null) { return NotFound(); }
            if(torlendoVicc.Aktiv)
            {
                torlendoVicc.Aktiv = false;
                _context.Entry(torlendoVicc).State = EntityState.Modified;
            }
            else
            {
                _context.Viccek.Remove(torlendoVicc);
            }
           await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
