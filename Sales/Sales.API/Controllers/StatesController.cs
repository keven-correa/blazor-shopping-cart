using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;
using Sales.Shared.Mappers.States;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]CreateStateDto stateDto)
        {
            try
            {
                var state = new State() { Name = stateDto.Name, CountryId = stateDto.CountryId };
                _context.Add(state);
                await _context.SaveChangesAsync();

                return Ok(state);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un estado con el nombre: {stateDto.Name}")
                    : (ActionResult)Conflict(dbEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _context.States
                .Include(c => c.Cities)
                .AsNoTracking()
                .ToListAsync());
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.States
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody]UpdateStateDto stateDto)
        {
            try
            {
                var state = new State() { Name = stateDto.Name, Id = id, CountryId = stateDto.CountryId };
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un estado con el nombre: {stateDto.Name}")
                    : (ActionResult)Conflict(dbEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            _context.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

