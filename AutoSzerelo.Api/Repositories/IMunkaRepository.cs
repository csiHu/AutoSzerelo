using AutoSzerelo.Api.Models;

namespace AutoSzerelo.Api.Repositories;

public interface IMunkaRepository
{
    Task<List<Munka>> GetMunkakAsync();

    Task<Munka> GetMunkaAsync(Guid id);

    Task<Munka> GetMunkaWithUgyfelAsync(Guid id);

    Task<List<Munka>> GetUgyfelMunkaiAsync(Guid ugyfelId);

    void AddMunka(Munka munka);

    void DeleteMunka(Munka munka);

    Task SaveChangesAsync();
}