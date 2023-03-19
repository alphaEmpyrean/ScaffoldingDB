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

namespace RediRND.Pages.StakerDailyProfits
{
    public class EditModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public EditModel(RediRND.Persistence.RediRndContext context)
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

            var stakerdailyprofit =  await _context.StakerDailyProfits.FirstOrDefaultAsync(m => m.StakerId == id);
            if (stakerdailyprofit == null)
            {
                return NotFound();
            }
            StakerDailyProfit = stakerdailyprofit;
           ViewData["StakerId"] = new SelectList(_context.Stakers, "Id", "Id");
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

            _context.Attach(StakerDailyProfit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StakerDailyProfitExists(StakerDailyProfit.StakerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StakerDailyProfitExists(int id)
        {
          return (_context.StakerDailyProfits?.Any(e => e.StakerId == id)).GetValueOrDefault();
        }
    }
}
