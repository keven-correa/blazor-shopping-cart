using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;
using Sales.Shared.Mappers.Countries;

namespace Sales.API.Controllers
{
    [Route("/api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]CreateCountryDto countryDto)
        {
            try
            {
                var country = new Country { Name= countryDto.Name };

                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un país con el nombre: {countryDto.Name}")
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
            return Ok(await _context.Countries
                .Include(c => c.States)
                .AsNoTracking()
                .ToListAsync());
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllCountries()
        {
            return Ok(await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities)
                .AsNoTracking()
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] UpdateCountryDto countryDto)
        {
            try
            {
                var country = new Country
                {
                    Name = countryDto.Name,
                    Id = id
                };
                _context.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un país con el nombre: {countryDto.Name}")
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
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
