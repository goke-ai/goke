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
    public class DetailsModel : PageModel
    {
        private readonly Goke.Web.ServerUI.Data.ApplicationDbContext _context;

        public DetailsModel(Goke.Web.ServerUI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Card Card { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            else
            {
                Card = card;
            }
            return Page();
        }
    }
}
