using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Exceptions;
using RediRND.App.Services;
using RediRND.Persistence;
using RediRND.App.Repositories;

namespace RediRND.Pages.Containers
{
    public class DeleteModel : PageModel
    {
        private readonly RediRndContext _context;
        private readonly ContainerRepository _containerService;

        public DeleteModel(RediRndContext context, ContainerRepository containerService)
        {
            _context = context;
            _containerService = containerService;
        }

        [BindProperty]
        public Container Container { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();            

            try
            {
                // Get container to be deleted
                Container = await _containerService.GetByIdWithParentAsync(id.Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                // Get container to be deleted
                await _containerService.DeleteByIdAsync(id.Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
