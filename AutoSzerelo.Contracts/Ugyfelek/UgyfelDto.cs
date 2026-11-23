namespace AutoSzerelo.Contracts.Ugyfelek;

public class UgyfelDto
{
    public Guid Id { get; init; }

    public string Nev { get; init; }

    public string Lakcim { get; init; }

    public string Email { get; init; }
}