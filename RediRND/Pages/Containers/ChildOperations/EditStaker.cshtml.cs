using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Repositories;
using RediRND.Persistence;

namespace RediRND.Pages.Containers
{
    public class EditChildStakerModel : PageModel
    {
        private readonly RediRndContext _context;
        private readonly ContainerRepository _containerService;

        public EditChildStakerModel(RediRndContext context, ContainerRepository containerService)
        {
            _context = context;
            _containerService = containerService;
        }

        [BindProperty]
        public int Weight { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
            {
                return NotFound();
            }
            var containerMembership = await _containerService.GetContainerMembershipByIdWithStaker(id.Value, childId.Value);
            ViewData["ContainerMembership"] = containerMembership;
            Weight = containerMembership.Weight;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
                return NotFound(); 

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _containerService.UpdateChildStakerAsync(id.Value, Weight, childId.Value);

            return RedirectToPage("/Containers/Details", new { id, childId });
        }

        private bool ContainerMembershipExists(int containerId, int stakerId)
        {
          return (_context.ContainerMemberships?.Any(e => e.ContainerId == containerId && e.StakerId == stakerId)).GetValueOrDefault();
        }
    }
}
