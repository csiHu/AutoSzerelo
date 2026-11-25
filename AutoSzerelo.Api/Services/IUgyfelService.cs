using AutoSzerelo.Api.Models;

namespace AutoSzerelo.Api.Services;

public interface IUgyfelService
{
    Task<List<Ugyfel>> GetUgyfelekAsync();

    Task<Ugyfel> GetUgyfelAsync(Guid id);

    Task<Guid> AddUgyfelAsync(Ugyfel ugyfel);

    Task UpdateUgyfelAsync(Guid id, Ugyfel ugyfel);

    Task DeleteUgyfelAsync(Guid id);
}