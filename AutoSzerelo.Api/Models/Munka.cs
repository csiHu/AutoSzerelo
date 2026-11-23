using AutoSzerelo.Contracts.Enums;

namespace AutoSzerelo.Api.Models;

public class Munka
{
    public Guid Id { get; set; }

    public Guid UgyfelId { get; set; }

    public Ugyfel Ugyfel { get; set; }

    public string Rendszam { get; set; }

    public int GyartasiEv { get; set; }

    public MunkaKategoria Kategoria { get; set; }

    public string HibaLeiras { get; set; }

    public int HibaSulyossag { get; set; }

    public string JavitasiNaplo { get; set; }

    public MunkaAllapot Allapot { get; set; }
}