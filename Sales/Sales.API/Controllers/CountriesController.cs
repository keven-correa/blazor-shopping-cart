using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

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
        public async Task<ActionResult> PostAsync(Country country)
        {
<<<<<<< HEAD
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un país con el nombre: {country.Name}")
                    : (ActionResult)Conflict(dbEx.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
=======
            var countryExists =  await _context.Countries.Where(c => c.Name == country.Name).FirstOrDefaultAsync();
            if(countryExists != null)
            {
                return Conflict($"Ya existe un pais con el nombre {country.Name}");
            }
            //if (countryExists != null)
            //{
            //    return Conflict("Ya existe un pais con ese nombre");
            
            _context.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);
>>>>>>> eeccff5e7fc996ef67f290fbe1979a62035661b8
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _context.Countries
                                    //.OrderBy(i => i.Id)
                                    .AsNoTracking()
                                    .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }


        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            try
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbEx)
            {
                return dbEx.InnerException!.Message.Contains("duplicate")
                    ? Conflict($"Ya existe un país con el nombre: {country.Name}")
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
