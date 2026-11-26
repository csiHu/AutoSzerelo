using AutoSzerelo.Api.Models;
using AutoSzerelo.Contracts.Munkak;

namespace AutoSzerelo.Api.Mappers;

public static class MunkaMappers
{
    public static Munka ToMunkaEntity(this AddMunkaRequest request) =>
        new()
        {
            UgyfelId = request.UgyfelId,
            Rendszam = request.Rendszam,
            GyartasiEv = request.GyartasiEv,
            Kategoria = request.Kategoria,
            HibaLeiras = request.HibaLeiras,
            HibaSulyossag = request.HibaSulyossag,
            JavitasiNaplo = request.JavitasiNaplo,
            Allapot = request.Allapot,
        };

    public static Munka ToMunkaEntity(this UpdateMunkaRequest request) =>
        new()
        {
            UgyfelId = request.UgyfelId,
            Rendszam = request.Rendszam,
            GyartasiEv = request.GyartasiEv,
            Kategoria = request.Kategoria,
            HibaLeiras = request.HibaLeiras,
            HibaSulyossag = request.HibaSulyossag,
            JavitasiNaplo = request.JavitasiNaplo,
            Allapot = request.Allapot,
        };

    public static MunkaDto ToDto(this Munka entity, double becsultMunkaora = 0) =>
        new()
        {
            Id = entity.Id,
            UgyfelId = entity.UgyfelId,
            UgyfelNev = entity.Ugyfel?.Nev,
            Rendszam = entity.Rendszam,
            GyartasiEv = entity.GyartasiEv,
            Kategoria = entity.Kategoria,
            HibaLeiras = entity.HibaLeiras,
            HibaSulyossag = entity.HibaSulyossag,
            JavitasiNaplo = entity.JavitasiNaplo,
            Allapot = entity.Allapot,
            BecsultMunkaora = becsultMunkaora,
        };

    public static List<MunkaDto> ToDto(this List<Munka> entities, Func<Munka, double> calculateBecsultMunkaora) =>
        entities.Select(munka => munka.ToDto(calculateBecsultMunkaora(munka))).ToList();
}