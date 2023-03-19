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

namespace RediRND.Pages.Stakers
{
    public class EditModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public EditModel(RediRND.Persistence.RediRndContext context)
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

            var staker =  await _context.Stakers.FirstOrDefaultAsync(m => m.Id == id);
            if (staker == null)
            {
                return NotFound();
            }
            Staker = staker;
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

            _context.Attach(Staker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StakerExists(Staker.Id))
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

        private bool StakerExists(int id)
        {
          return (_context.Stakers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
