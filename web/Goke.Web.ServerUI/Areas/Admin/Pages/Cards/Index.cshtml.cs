using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Goke.Web.ServerUI.Data;
using Goke.Web.ServerUI.Models;

namespace Goke.Web.ServerUI.Areas.Admin.Pages.Cards
{
    public class IndexModel : PageModel
    {
        private readonly Goke.Web.ServerUI.Data.ApplicationDbContext _context;

        public IndexModel(Goke.Web.ServerUI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Card> Card { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Card = await _context.Cards
                .Include(c => c.Owner).ToListAsync();
        }
    }
}
