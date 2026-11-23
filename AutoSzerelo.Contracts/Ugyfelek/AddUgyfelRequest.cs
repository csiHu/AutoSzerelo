using System.ComponentModel.DataAnnotations;

namespace AutoSzerelo.Contracts.Ugyfelek;

public class AddUgyfelRequest
{
    [Required]
    public string Nev { get; init; }

    [Required]
    public string Lakcim { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }
}