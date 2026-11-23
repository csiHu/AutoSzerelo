namespace AutoSzerelo.Api.Models;

public class Ugyfel
{
    public Guid Id { get; set; }

    public string Nev { get; set; }

    public string Lakcim { get; set; }

    public string Email { get; set; }

    public List<Munka> Munkak { get; set; } = new();
}