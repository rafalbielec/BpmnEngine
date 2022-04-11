using System.ComponentModel.DataAnnotations;

namespace BpmnEngine.Application.Models;

public class CarHireViewModel
{
    private const string RequiredLabel = "Pole wymagane";

    [Required(AllowEmptyStrings = false, ErrorMessage = RequiredLabel)]
    [Display(Name = "Telefon kontaktowy")]
    public string? PhoneNumber { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = RequiredLabel)]
    [Display(Name = "Przejazd do")]
    public string? Destination { get; set; }
}