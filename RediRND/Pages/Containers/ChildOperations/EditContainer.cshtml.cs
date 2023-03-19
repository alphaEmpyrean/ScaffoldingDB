using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Repositories;

namespace RediRND.Pages.Containers
{
    public class EditChildContainerModel : PageModel
    {
        private readonly ContainerRepository _containerService;

        public EditChildContainerModel(ContainerRepository containerService)
        {
            _containerService = containerService;
        }

        [BindProperty]
        public int Weight { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
                return NotFound();

            try
            {
                // Get container details
                Container childContainer = await _containerService.GetByIdAsync(childId.Value);
                Weight = childContainer.Weight;
                ViewData["ChildContainer"] = childContainer;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? childId)
        {
            if(id == null || childId == null) 
                return NotFound();

            try
            {
                Container childContainer = await _containerService.GetByIdAsync(childId.Value);

                if (!ModelState.IsValid)
                {                    
                    Weight = childContainer.Weight;
                    return Page();
                }

                childContainer.Weight = Weight;
                await _containerService.UpdateAsync(childContainer);
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
