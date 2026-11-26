using AutoSzerelo.Api.Models;

namespace AutoSzerelo.Api.Services;

public interface IMunkaService
{
    Task<List<Munka>> GetMunkakAsync();

    Task<Munka> GetMunkaAsync(Guid id);

    Task<Munka> GetMunkaWithUgyfelAsync(Guid id);

    Task<List<Munka>> GetUgyfelMunkaiAsync(Guid ugyfelId);

    Task<Guid> AddMunkaAsync(Munka munka);

    Task UpdateMunkaAsync(Guid id, Munka munka);

    Task DeleteMunkaAsync(Guid id);

    Task<int> GetAktivMunkakSzamaAsync();

    double CalculateBecsultMunkaora(Munka munka, int aktivMunkakSzama);
}