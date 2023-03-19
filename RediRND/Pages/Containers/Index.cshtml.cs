using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Repositories;

namespace RediRND.Pages.Containers
{
    public class IndexModel : PageModel
    {

        private readonly ContainerRepository _containerService;

        public IndexModel( ContainerRepository containerService)
        {
            _containerService = containerService;
        }

        public IList<Container> Container { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Container = await _containerService.GetAllWithParentAsync();
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
