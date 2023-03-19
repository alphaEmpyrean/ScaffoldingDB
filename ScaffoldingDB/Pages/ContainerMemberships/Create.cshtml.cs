using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.ContainerMemberships
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
        ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "Id");
        ViewData["StakerId"] = new SelectList(_context.Stakers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ContainerMembership ContainerMembership { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ContainerMemberships == null || ContainerMembership == null)
            {
                return Page();
            }

            _context.ContainerMemberships.Add(ContainerMembership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
