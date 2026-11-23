using AutoSzerelo.Contracts.Enums;

namespace AutoSzerelo.Contracts.Munkak;

public class MunkaDto
{
    public Guid Id { get; init; }

    public Guid UgyfelId { get; init; }

    public string UgyfelNev { get; init; }

    public string Rendszam { get; init; }

    public int GyartasiEv { get; init; }

    public MunkaKategoria Kategoria { get; init; }

    public string HibaLeiras { get; init; }

    public int HibaSulyossag { get; init; }

    public string JavitasiNaplo { get; init; }

    public MunkaAllapot Allapot { get; init; }

    public double BecsultMunkaora { get; init; }
}