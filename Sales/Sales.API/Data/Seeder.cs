using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class Seeder
    {
        private readonly DataContext _context;

        public Seeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "República Dominicana"
                });
                _context.Countries.Add(new Country
                {
                    Name = "México"
                });
                _context.Countries.Add(new Country
                {
                    Name = "Colombia"
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
