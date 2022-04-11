using System.ComponentModel.DataAnnotations;

namespace BpmnEngine.Application.Models;

public class MessagesViewModel
{
    private const string RequiredLabel = "Pole wymagane";

    [Required(AllowEmptyStrings = false, ErrorMessage = RequiredLabel)]
    [Display(Name = "Klucz biznesowy procesu")]
    public string BusinessKey { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = RequiredLabel)]
    [Display(Name = "Treść wiadomości")]
    public string MessageContent { get; set; }
}