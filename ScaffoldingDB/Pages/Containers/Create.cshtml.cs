using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.Containers
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
        ViewData["ParentId"] = new SelectList(_context.Containers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Container Container { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Containers == null || Container == null)
            {
                return Page();
            }

            _context.Containers.Add(Container);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
