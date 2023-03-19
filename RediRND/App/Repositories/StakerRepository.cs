using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.App.Exceptions;
using RediRND.Persistence;

namespace RediRND.App.Services
{
    public class StakerRepository
    {
        private readonly RediRndContext _context;
        public StakerRepository(RediRndContext context) 
        {
            _context = context;
        }

        private bool StakerExists(int id)
        {
            if (_context.Stakers == null)
                throw new NullDbSetException(nameof(Staker));

            return (_context.Stakers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
