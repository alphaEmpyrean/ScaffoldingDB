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
    public class DeleteModel : PageModel
    {
        private readonly ScaffoldingDB.Data.RediRndContext _context;

        public DeleteModel(ScaffoldingDB.Data.RediRndContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Containers == null)
            {
                return NotFound();
            }
            var container = await _context.Containers.FindAsync(id);

            if (container != null)
            {
                Container = container;
                _context.Containers.Remove(Container);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
