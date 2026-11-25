using AutoSzerelo.Api.Exceptions;
using AutoSzerelo.Api.Models;
using AutoSzerelo.Api.Repositories;

namespace AutoSzerelo.Api.Services;

public class UgyfelService : IUgyfelService
{
    private readonly IUgyfelRepository _ugyfelRepository;

    public UgyfelService(IUgyfelRepository ugyfelRepository)
    {
        if (ugyfelRepository is null)
        {
            throw new ArgumentNullException(nameof(ugyfelRepository));
        }

        _ugyfelRepository = ugyfelRepository;
    }

    public Task<List<Ugyfel>> GetUgyfelekAsync()
    {
        return _ugyfelRepository.GetUgyfelekAsync();
    }

    public Task<Ugyfel> GetUgyfelAsync(Guid id)
    {
        return _ugyfelRepository.GetUgyfelAsync(id);
    }

    public async Task<Guid> AddUgyfelAsync(Ugyfel ugyfel)
    {
        if (ugyfel is null)
        {
            throw new ArgumentNullException(nameof(ugyfel));
        }

        ugyfel.Id = Guid.CreateVersion7();
        _ugyfelRepository.AddUgyfel(ugyfel);
        await _ugyfelRepository.SaveChangesAsync();
        return ugyfel.Id;
    }

    public async Task UpdateUgyfelAsync(Guid id, Ugyfel ugyfel)
    {
        if (ugyfel is null)
        {
            throw new ArgumentNullException(nameof(ugyfel));
        }

        var ugyfelToUpdate = await GetRequiredUgyfelAsync(id);
        ugyfelToUpdate.Nev = ugyfel.Nev;
        ugyfelToUpdate.Lakcim = ugyfel.Lakcim;
        ugyfelToUpdate.Email = ugyfel.Email;
        await _ugyfelRepository.SaveChangesAsync();
    }

    public async Task DeleteUgyfelAsync(Guid id)
    {
        var ugyfel = await GetRequiredUgyfelAsync(id);
        _ugyfelRepository.DeleteUgyfel(ugyfel);
        await _ugyfelRepository.SaveChangesAsync();
    }

    private async Task<Ugyfel> GetRequiredUgyfelAsync(Guid id)
    {
        var ugyfel = await _ugyfelRepository.GetUgyfelAsync(id);
        if (ugyfel is null)
        {
            throw new EntityNotFoundException($"Ugyfel with id '{id}' does not exist");
        }

        return ugyfel;
    }
}