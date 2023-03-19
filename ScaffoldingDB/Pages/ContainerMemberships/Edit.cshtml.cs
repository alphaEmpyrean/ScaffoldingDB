using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.ContainerMemberships
{
    public class EditModel : PageModel
    {
        private readonly ScaffoldingDB.Data.RediRndContext _context;

        public EditModel(ScaffoldingDB.Data.RediRndContext context)
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

            var containermembership =  await _context.ContainerMemberships.FirstOrDefaultAsync(m => m.ContainerId == id);
            if (containermembership == null)
            {
                return NotFound();
            }
            ContainerMembership = containermembership;
           ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "Id");
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

            _context.Attach(ContainerMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerMembershipExists(ContainerMembership.ContainerId))
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

        private bool ContainerMembershipExists(int id)
        {
          return (_context.ContainerMemberships?.Any(e => e.ContainerId == id)).GetValueOrDefault();
        }
    }
}
