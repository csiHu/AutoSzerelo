using System.ComponentModel.DataAnnotations;
using AutoSzerelo.Contracts.Enums;
using AutoSzerelo.Contracts.Validations;

namespace AutoSzerelo.Contracts.Munkak;

public class AddMunkaRequest
{
    [Required]
    public Guid UgyfelId { get; init; }

    [Required]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$")]
    public string Rendszam { get; init; }

    [GyartasiEvRange]
    public int GyartasiEv { get; init; }

    [EnumDataType(typeof(MunkaKategoria))]
    public MunkaKategoria Kategoria { get; init; }

    [Required]
    public string HibaLeiras { get; init; }

    [Range(1, 10)]
    public int HibaSulyossag { get; init; }

    public string JavitasiNaplo { get; init; }

    [EnumDataType(typeof(MunkaAllapot))]
    public MunkaAllapot Allapot { get; init; }
}