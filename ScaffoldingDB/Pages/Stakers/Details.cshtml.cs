using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.Stakers
{
    public class DetailsModel : PageModel
    {
        private readonly ScaffoldingDB.Data.RediRndContext _context;

        public DetailsModel(ScaffoldingDB.Data.RediRndContext context)
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
    }
}
