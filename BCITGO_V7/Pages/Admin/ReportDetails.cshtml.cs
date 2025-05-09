
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.EntityFrameworkCore;

namespace BCITGO_V6.Pages.Admin
{
    public class ReportDetailsModel : PageModel
    {
        //        private readonly ApplicationDbContext _context;

        //        public ReportDetailsModel(ApplicationDbContext context)
        //        {
        //            _context = context;
        //        }

        //        public SupportTicket Report { get; set; } = new();

        //        public async Task<IActionResult> OnGetAsync(string id)
        //        {
        //            Report = await _context.SupportTicket.FirstOrDefaultAsync(r => r.SupportId == id);
        //            if (Report == null)
        //                return NotFound();

        //            return Page();
        //        }

        //        public async Task<IActionResult> OnPostAsync(string id)
        //        {
        //            Report = await _context.SupportTicket.FirstOrDefaultAsync(r => r.SupportId == id);
        //            if (Report == null)
        //                return NotFound();

        //            // Update the status
        //            Report.Status = Request.Form["status"];
        //            _context.Update(Report);
        //            await _context.SaveChangesAsync();

        //            return RedirectToPage("/Admin/Reports");
    }
}

