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
    public class DeleteModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public DeleteModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.StakerDailyProfits == null)
            {
                return NotFound();
            }
            var stakerdailyprofit = await _context.StakerDailyProfits.FindAsync(id);

            if (stakerdailyprofit != null)
            {
                StakerDailyProfit = stakerdailyprofit;
                _context.StakerDailyProfits.Remove(StakerDailyProfit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
