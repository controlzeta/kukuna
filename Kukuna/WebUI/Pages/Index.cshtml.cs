using AccesoDatos;
using AccesoDatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;
        private readonly SqlServerDbContext _context;
        public int ingridientId { get; set; }
        public List<SelectListItem> ingredients { get; set; }
        public List<Ingredient> lstIngredients { get; set; }
        private DAIngredients proxy;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Message { get; set; }


        //[BindProperty]
        //public Ingredient ingredient { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, SqlServerDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            proxy = new DAIngredients(_context);
        }

        public async Task<IActionResult> OnPostSendEmail()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return the page with validation errors
                return Page();
            }

            // Get SMTP settings from appsettings.json
            var smtpHost = _configuration["SmtpSettings:Host"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            var smtpUsername = _configuration["SmtpSettings:Username"];
            var smtpPassword = _configuration["SmtpSettings:Password"];
            //var fromEmail = _configuration["SmtpSettings:FromEmail"];
            var toEmail = _configuration["SmtpSettings:ToEmail"]; // The email address where you want to receive messages

            try
            {
                using (SmtpClient client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.EnableSsl = true; // Most SMTP servers require SSL/TLS
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(Email, "Contacto Web Control Zeta");
                        mailMessage.To.Add(toEmail); // Your receiving email address
                        mailMessage.Subject = $"Nuevo Mensaje de Contacto - {Name}";
                        mailMessage.Body = $"Nombre: {Name}\nEmail: {Email}\n\nMensaje:\n{Message}";
                        mailMessage.IsBodyHtml = false; // Set to true if you want to send HTML email

                        await client.SendMailAsync(mailMessage);
                    }
                }

                // Redirect to a success page or display a success message
                TempData["SuccessMessage"] = "¡Tu mensaje ha sido enviado con éxito!";
                //return RedirectToPage("/Contact"); // Or a confirmation page
            }
            catch (SmtpException ex)
            {
                // Log the exception (e.g., using ILogger)
                // For now, let's just add an error to ModelState
                ModelState.AddModelError(string.Empty, $"Error al enviar el mensaje: {ex.Message}");
                // In a production app, you'd want more robust logging and error handling.
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {ex.Message}");
                return Page();
            }
            return Page();
        }

        public void OnGet()
        {
            lstIngredients = proxy.GetIngredients();
            ingredients = lstIngredients.Select(m =>
            new SelectListItem { Text = m.IngredientName, Value = m.IngredientId.ToString() }).ToList();
            //ingredient = proxy.GetIngredientsById((int)1);
        }


        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id != null)
        //    {

        //        ingredient = proxy.GetIngredientsById((int)id);

        //        if (ingredient == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return Page();
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if (ingredient != null)
            //{
            //    if (proxy.InsertIngredient(ingredient.IngredientName, ingredient.Category))
            //    {
            //        ingredient = new Ingredient();
            //    }
            //}
            lstIngredients = proxy.GetIngredients();
            return RedirectToPage("./Index");
        }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    DAIngredients proxy = new DAIngredients();
        //    lstIngredients = proxy.GetIngredients();
        //    ingredients = lstIngredients.Select(m =>
        //    new SelectListItem { Text = m.IngredientName, Value = m.IngredientId.ToString() }).ToList        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ingredient = proxy.GetIngredientsById((int)id);

        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}
    }
}
