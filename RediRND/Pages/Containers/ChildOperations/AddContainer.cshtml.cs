using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Repositories;
using RediRND.App.Tools;


namespace RediRND.Pages.Containers
{
    public class AddChildContainerModel : PageModel
    {

        private readonly ContainerRepository _containerRepository;

        public AddChildContainerModel(ContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        [BindProperty]
        public int ChildContainerId { get; set; } = default!;

        [BindProperty]
        public int Weight { get; set; } = 1;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["CurrentContainerId"]  = id.Value;

            try
            {
                List<Container> containerList = await _containerRepository.GetNonChildrenContainersByContainerIdAsync(id.Value);
                ViewData["ChildSelectList"] = SelectListBuilder.ContainersNotAboveInHeirarchyPath(containerList, id.Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                if (!ModelState.IsValid)
                {
                    List<Container> containerList = await _containerRepository.GetAllAsync();
                    ViewData["ChildSelectList"] = SelectListBuilder.ContainersNotAboveInHeirarchyPath(containerList, id.Value);
                    return Page();
                }

                Container newChild = await _containerRepository.GetByIdAsync(ChildContainerId);
                await _containerRepository.AddChildContainerAsync(id.Value, Weight, newChild);

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
