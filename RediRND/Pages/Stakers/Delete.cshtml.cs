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
    public class DeleteModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public DeleteModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Staker Staker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stakers == null)
            {
                return NotFound();
            }

            var staker = await _context.Stakers.FirstOrDefaultAsync(m => m.Id == id);

            if (staker == null)
            {
                return NotFound();
            }
            else 
            {
                Staker = staker;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stakers == null)
            {
                return NotFound();
            }
            var staker = await _context.Stakers.FindAsync(id);

            if (staker != null)
            {
                Staker = staker;
                _context.Stakers.Remove(Staker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
