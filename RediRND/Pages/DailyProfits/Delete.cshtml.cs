using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.DailyProfits
{
    public class DeleteModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public DeleteModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DailyProfit DailyProfit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(DateTime? id)
        {
            if (id == null || _context.DailyProfits == null)
            {
                return NotFound();
            }

            var dailyprofit = await _context.DailyProfits.FirstOrDefaultAsync(m => m.Date == id);

            if (dailyprofit == null)
            {
                return NotFound();
            }
            else 
            {
                DailyProfit = dailyprofit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(DateTime? id)
        {
            if (id == null || _context.DailyProfits == null)
            {
                return NotFound();
            }
            var dailyprofit = await _context.DailyProfits.FindAsync(id);

            if (dailyprofit != null)
            {
                DailyProfit = dailyprofit;
                _context.DailyProfits.Remove(DailyProfit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
