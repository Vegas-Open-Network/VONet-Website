using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VONetWebsite.Pages
{
    public class VolunteerModel : PageModel
    {
        private readonly ILogger<VolunteerModel> _logger;

        public VolunteerModel(ILogger<VolunteerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}