using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.ContainerMemberships
{
    public class DeleteModel : PageModel
    {
        private readonly RediRndContext _context;

        public DeleteModel(RediRndContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ContainerMembership ContainerMembership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ContainerMemberships == null)
            {
                return NotFound();
            }

            var containermembership = await _context.ContainerMemberships.FirstOrDefaultAsync(m => m.ContainerId == id);

            if (containermembership == null)
            {
                return NotFound();
            }
            else 
            {
                ContainerMembership = containermembership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ContainerMemberships == null)
            {
                return NotFound();
            }
            var containermembership = await _context.ContainerMemberships.FindAsync(id);

            if (containermembership != null)
            {
                ContainerMembership = containermembership;
                _context.ContainerMemberships.Remove(ContainerMembership);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
