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
    public class DetailsModel : PageModel
    {
        private readonly RediRndContext _context;

        public DetailsModel(RediRndContext context)
        {
            _context = context;
        }

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
    }
}
