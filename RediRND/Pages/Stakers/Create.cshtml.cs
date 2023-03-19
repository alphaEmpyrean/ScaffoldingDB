using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RediRND.App.Entities;
using RediRND.Persistence;

namespace RediRND.Pages.Stakers
{
    public class CreateModel : PageModel
    {
        private readonly RediRndContext _context;
        private readonly PasswordHasher<Staker> _passwordHasher;

        public CreateModel(RediRndContext context, PasswordHasher<Staker> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Staker Staker { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Stakers == null || Staker == null)
            {
                return Page();
            }
            
            // Hash password before adding to db
            Staker.PasswordHash = _passwordHasher.HashPassword(Staker, Staker.PasswordHash);

            _context.Stakers.Add(Staker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
