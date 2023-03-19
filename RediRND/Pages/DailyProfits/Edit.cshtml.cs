using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.DailyProfits
{
    public class EditModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public EditModel(RediRND.Persistence.RediRndContext context)
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

            var dailyprofit =  await _context.DailyProfits.FirstOrDefaultAsync(m => m.Date == id);
            if (dailyprofit == null)
            {
                return NotFound();
            }
            DailyProfit = dailyprofit;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DailyProfit).State = EntityState.Modified;

            await _context.Entry(DailyProfit).Collection(dp => dp.StakerDailyProfits).LoadAsync();

            foreach (var stakerDailyProfit in DailyProfit.StakerDailyProfits)
            {
                stakerDailyProfit.Profit = stakerDailyProfit.Stake * DailyProfit.Profit;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyProfitExists(DailyProfit.Date))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            return RedirectToPage("/Index");
        }

        private bool DailyProfitExists(DateTime id)
        {
          return (_context.DailyProfits?.Any(e => e.Date == id)).GetValueOrDefault();
        }
    }
}
