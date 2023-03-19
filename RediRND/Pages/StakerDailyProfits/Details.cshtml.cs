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
    public class DetailsModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public DetailsModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

      public StakerDailyProfit StakerDailyProfit { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StakerDailyProfits == null)
            {
                return NotFound();
            }

            var stakerdailyprofit = await _context.StakerDailyProfits.FirstOrDefaultAsync(m => m.StakerId == id);
            if (stakerdailyprofit == null)
            {
                return NotFound();
            }
            else 
            {
                StakerDailyProfit = stakerdailyprofit;
            }
            return Page();
        }
    }
}
