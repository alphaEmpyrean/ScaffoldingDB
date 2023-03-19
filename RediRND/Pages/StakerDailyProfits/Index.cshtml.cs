using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.StakerDailyProfits
{
    public class IndexModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public IndexModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

        public IList<StakerDailyProfit> StakerDailyProfit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StakerDailyProfits != null)
            {
                StakerDailyProfit = await _context.StakerDailyProfits
                .Include(s => s.Staker).ToListAsync();
            }
        }
    }
}
