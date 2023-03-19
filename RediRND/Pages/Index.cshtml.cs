using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.Persistence;

namespace RediRND.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RediRndContext _context;

        public IndexModel(ILogger<IndexModel> logger, RediRndContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<DailyProfit> DailyProfits { get; set; } = default!;

        [BindProperty]
        public DailyProfit DailyProfit { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DailyProfits != null)
            {
                DailyProfits = await _context.DailyProfits.ToListAsync();
            }

            DailyProfit = new DailyProfit() { Date = DateTime.Now };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DailyProfits == null || DailyProfit == null)
            {
                return Page();
            }

            try
            {
                _context.DailyProfits.Add(DailyProfit);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (entryExists(DailyProfit.Date))
                    return RedirectToPage("/DailyProfits/Edit", new { id = DailyProfit.Date });
                else
                    throw ex;
            }

            // Add Staker Profit Entries
            List<StakerDailyProfit> dailyProfitList = new();
            List<Staker> Stakers = await _context.Stakers.Include(s => s.ContainerMemberships).ToListAsync();
            foreach (Staker staker in Stakers)
            {
                decimal totalStake = 0;
                foreach (ContainerMembership cm in staker.ContainerMemberships)
                {
                    totalStake += cm.Stake;
                }
                dailyProfitList.Add(new StakerDailyProfit() { StakerId = staker.Id, Stake = totalStake, Date = DailyProfit.Date, Profit = totalStake * DailyProfit.Profit });
            }

            _context.StakerDailyProfits.AddRange(dailyProfitList);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        private bool entryExists(DateTime dateTime)
        {
            return _context.DailyProfits.Any(dp => dp.Date == dateTime);
        }
    }
}