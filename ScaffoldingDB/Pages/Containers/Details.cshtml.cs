using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.Containers
{
    public class DetailsModel : PageModel
    {
        private readonly ScaffoldingDB.Data.RediRndContext _context;

        public DetailsModel(ScaffoldingDB.Data.RediRndContext context)
        {
            _context = context;
        }

      public Container Container { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Containers == null)
            {
                return NotFound();
            }

            var container = await _context.Containers.FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }
            else 
            {
                Container = container;
            }
            return Page();
        }
    }
}
