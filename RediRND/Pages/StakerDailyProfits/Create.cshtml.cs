using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.StakerDailyProfits
{
    public class CreateModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public CreateModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StakerId"] = new SelectList(_context.Stakers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public StakerDailyProfit StakerDailyProfit { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.StakerDailyProfits == null || StakerDailyProfit == null)
            {
                return Page();
            }

            _context.StakerDailyProfits.Add(StakerDailyProfit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
