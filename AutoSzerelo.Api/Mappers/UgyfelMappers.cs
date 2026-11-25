using AutoSzerelo.Api.Models;
using AutoSzerelo.Contracts.Ugyfelek;

namespace AutoSzerelo.Api.Mappers;

public static class UgyfelMappers
{
    public static Ugyfel ToUgyfelEntity(this UpdateUgyfelRequest dto) =>
        new()
        {
            Nev = dto.Nev,
            Lakcim = dto.Lakcim,
            Email = dto.Email,
        };

    public static Ugyfel ToUgyfelEntity(this AddUgyfelRequest dto) =>
        new()
        {
            Nev = dto.Nev,
            Lakcim = dto.Lakcim,
            Email = dto.Email,
        };

    public static UgyfelDto ToDto(this Ugyfel entity) =>
        new()
        {
            Id = entity.Id,
            Nev = entity.Nev,
            Lakcim = entity.Lakcim,
            Email = entity.Email,
        };

    public static List<UgyfelDto> ToDto(this List<Ugyfel> entity) =>
        entity
            .Select(x => x.ToDto())
            .ToList();
}