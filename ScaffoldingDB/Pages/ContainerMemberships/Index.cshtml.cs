using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Pages.ContainerMemberships
{
    public class IndexModel : PageModel
    {
        private readonly RediRndContext _context;

        public IndexModel(RediRndContext context)
        {
            _context = context;
        }

        public IList<ContainerMembership> ContainerMembership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ContainerMemberships != null)
            {
                ContainerMembership = await _context.ContainerMemberships
                .Include(c => c.Container)
                .Include(c => c.Staker).ToListAsync();
            }
        }
    }
}
