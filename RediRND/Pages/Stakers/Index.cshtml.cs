using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.Stakers
{
    public class IndexModel : PageModel
    {
        private readonly RediRndContext _context;

        public IndexModel(RediRndContext context)
        {
            _context = context;
        }

        public IList<Staker> Staker { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stakers != null)
            {
                Staker = await _context.Stakers.ToListAsync();
            }
        }
    }
}
