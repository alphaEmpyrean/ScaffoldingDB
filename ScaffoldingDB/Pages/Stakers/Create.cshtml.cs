using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.Stakers
{
    public class CreateModel : PageModel
    {
        private readonly ScaffoldingDB.Data.RediRndContext _context;

        public CreateModel(ScaffoldingDB.Data.RediRndContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Staker Staker { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Stakers == null || Staker == null)
            {
                return Page();
            }

            _context.Stakers.Add(Staker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
