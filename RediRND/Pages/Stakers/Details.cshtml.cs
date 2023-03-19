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
    public class DetailsModel : PageModel
    {
        private readonly RediRND.Persistence.RediRndContext _context;

        public DetailsModel(RediRND.Persistence.RediRndContext context)
        {
            _context = context;
        }

      public Staker Staker { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stakers == null)
            {
                return NotFound();
            }

            var staker = await _context.Stakers
                .Include(s => s.StakerDailyProfits)
                .Include(s => s.ContainerMemberships)
                .ThenInclude(cm => cm.Container)
                .FirstOrDefaultAsync(m => m.Id == id);
            
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
    }
}
