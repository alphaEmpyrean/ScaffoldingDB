using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Repositories;
using RediRND.App.Services;
using RediRND.App.Tools;


namespace RediRND.Pages.Containers
{
    public class CreateModel : PageModel
    {
        private readonly ContainerRepository _containerService;

        public CreateModel(ContainerRepository containerService)
        {
            _containerService = containerService;
        }

        [BindProperty]
        public Container Container { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                // Build select list for possible parent conatiners
                List<Container> containerList = await _containerService.GetAllAsync();
                ViewData["ParentSelectList"] = SelectListBuilder.AllContainers(containerList);
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
                if (!ModelState.IsValid || Container == null)
                {
                    // Build select list for possible parent conatiners
                    List<Container> containerList = await _containerService.GetAllAsync();
                    ViewData["ParentSelectList"] = SelectListBuilder.AllContainers(containerList);

                    return Page();
                }

                // Save new container
                await _containerService.AddAsync(Container);
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
