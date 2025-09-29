using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VONetWebsite.Pages
{
    public class preregisterModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Phone { get; set; } = string.Empty;

        [BindProperty]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public string City { get; set; } = "Las Vegas";

        [BindProperty]
        public string ZipCode { get; set; } = string.Empty;

        [BindProperty]
        public string PropertyType { get; set; } = string.Empty;

        [BindProperty]
        public List<string> Interests { get; set; } = new List<string>();

        [BindProperty]
        public bool Newsletter { get; set; } = true;

        [BindProperty]
        public string HearAbout { get; set; } = string.Empty;

        [BindProperty]
        public string Comments { get; set; } = string.Empty;

        public void OnGet()
        {
            // Initialize default values if needed
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // TODO: In a real implementation, you would:
                // 1. Save the registration data to a database
                // 2. Send confirmation email to the user
                // 3. Notify the team about the new registration
                // 4. Add to newsletter if requested
                
                // For now, we'll just log the registration (in production, you'd use proper logging)
                var registrationData = new
                {
                    Timestamp = DateTime.UtcNow,
                    Name = $"{FirstName} {LastName}",
                    Email,
                    Phone,
                    Address = $"{Address}, {City} {ZipCode}",
                    PropertyType,
                    Interests = string.Join(", ", Interests),
                    Newsletter,
                    HearAbout,
                    Comments
                };

                // In production, you might save to database like:
                // await _context.PreRegistrations.AddAsync(new PreRegistration { ... });
                // await _context.SaveChangesAsync();

                // For now, we'll redirect to a success page or show a success message
                TempData["SuccessMessage"] = $"Thank you {FirstName}! Your pre-registration has been submitted successfully. We'll be in touch soon.";
                
                return RedirectToPage("/preregister");
            }
            catch (Exception ex)
            {
                // Log the error in production
                ModelState.AddModelError("", "Sorry, there was an error processing your registration. Please try again or contact support.");
                return Page();
            }
        }
    }
}