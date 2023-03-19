using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RediRND.App.Entities;
using RediRND.App.Repositories;
using RediRND.App.Tools;

namespace RediRND.Pages.Containers
{
    public class AddChildModel : PageModel
    {

        private readonly ContainerRepository _containerService;


        public AddChildModel(ContainerRepository containerService)
        {
            _containerService = containerService;
        }

        [BindProperty]
        public int StakerId { get; set; } = default!;

        [BindProperty]
        public int Weight { get; set; } = 1;

        public int ContainerId { get; set; } = default!;

        public List<SelectListItem> StakerSelectList { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ContainerId= id.Value;

            try
            {
                List<Staker> stakersNotInContainer = await _containerService.GetNonChildrenStakersByContainerIdAsync(id.Value);
                ViewData["ChildSelectList"] = SelectListBuilder.AllStakers(stakersNotInContainer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid || id == 0 || StakerId == 0 || id == null)
            {
                return Page();
            }

            try
            {
                await _containerService.AddChildStakerAsync(id.Value, Weight, StakerId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return NotFound();
            }

            return RedirectToPage($"/Containers/Details", new { id });
        }
    }
}
