using RediRND.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Exceptions;
using RediRND.App.Services;

namespace RediRND.Pages.Containers
{
    public class DetailsModel : PageModel
    {
        private readonly ContainerRepository _containerService;

        public DetailsModel(ContainerRepository containerService)
        {
            _containerService = containerService;
        }

        public Container Container { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Container = await _containerService.GetByIdWithAllThenWithStaker(id.Value);
            }
            catch (Exception ex)
            {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return NotFound();
            }

            return Page();
        }
    }
}
