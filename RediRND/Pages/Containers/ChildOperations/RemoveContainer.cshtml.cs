using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Repositories;
using RediRND.Persistence;

namespace RediRND.Pages.Containers
{
    public class RemoveChildContainerModel : PageModel
    {
        private readonly RediRndContext _context;
        private readonly ContainerRepository _containerService;

        public RemoveChildContainerModel(RediRndContext context, ContainerRepository containerService)
        {
            _context = context;
            _containerService = containerService;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
                return NotFound();

            try
            {
                var container = await _containerService.GetByIdAsync(childId.Value);

                // Check to make sure child container exists and has the proper parent
                if (container == null || container.ParentId != id)
                    return NotFound();

                ViewData["ChildContainer"] = container;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }

            return Page();
        }      

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
                return Page();

            try
            {
                var childContainer = await _containerService.GetByIdAsync(childId.Value);
                await _containerService.RemoveChildContainer(id.Value, childContainer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }

            return RedirectToPage("/Containers/Details", new { id });
        }
    }
}
