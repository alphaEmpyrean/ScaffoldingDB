using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Services;
using RediRND.App.Tools;
using RediRND.Persistence;
using RediRND.App.Repositories;


namespace RediRND.Pages.Containers
{
    public class EditModel : PageModel
    {
        private readonly ContainerRepository _containerService;

        public EditModel( ContainerRepository containerService)
        {
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
                Container = await _containerService.GetByIdAsync(id.Value);

                // Build list of possible parent container
                // Do not include self selection as an option
                List<Container> containerList = await _containerService.GetAllAsync();
                ViewData["ParentSelectList"] = SelectListBuilder.ContainersNotBelowInHeirarchyPath(containerList, id.Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    // Build list of possible parent container
                    // Do not include self selection as an option
                    List<Container> containerList = await _containerService.GetAllAsync();
                    ViewData["ParentSelectList"] = SelectListBuilder.ContainersNotBelowInHeirarchyPath(containerList, Container.Id);

                    return Page();
                }                    

                await _containerService.UpdateAsync(Container);
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
