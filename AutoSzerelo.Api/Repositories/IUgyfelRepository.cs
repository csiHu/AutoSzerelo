using AutoSzerelo.Api.Models;

namespace AutoSzerelo.Api.Repositories;

public interface IUgyfelRepository
{
    Task<List<Ugyfel>> GetUgyfelekAsync();

    Task<Ugyfel> GetUgyfelAsync(Guid id);

    void AddUgyfel(Ugyfel ugyfel);

    void DeleteUgyfel(Ugyfel ugyfel);

    Task SaveChangesAsync();
}