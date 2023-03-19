using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Repositories;
using RediRND.App.Services;
using RediRND.Persistence;

namespace RediRND.Pages.Containers
{
    public class RemoveChildStakerModel : PageModel
    {
        private readonly RediRndContext _context;
        private readonly ContainerRepository _containerService;

        public RemoveChildStakerModel(RediRndContext context, ContainerRepository containerService)
        {
            _context = context;
            _containerService = containerService;
        }

        [BindProperty]
         public ContainerMembership ContainerMembership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? childId)
        {
            if (id == null || childId == null)
            {
                return NotFound();
            }

            try
            {
                var containermembership = await _containerService.GetContainerMembershipByIdWithStaker(id.Value, childId.Value);
                ContainerMembership = containermembership;
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
            if (id == null || childId == null)
            {
                return NotFound();
            }

            try
            {
                await _containerService.RemoveChildStaker(id.Value, childId.Value);
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
