using Microsoft.EntityFrameworkCore;
using AutoSzerelo.Api.Models;

namespace AutoSzerelo.Api.Repositories;

public class UgyfelRepository : IUgyfelRepository
{
    private readonly AppDbContext _appDbContext;

    public UgyfelRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Ugyfel>> GetUgyfelekAsync()
    {
        return await _appDbContext.Ugyfelek.AsNoTracking().ToListAsync();
        
    }

    public async Task<Ugyfel> GetUgyfelAsync(Guid id)
    {
        return await _appDbContext.Ugyfelek
            .FirstOrDefaultAsync(ugyfel => ugyfel.Id == id);
    }

    public void AddUgyfel(Ugyfel ugyfel)
    {
        _appDbContext.Ugyfelek.Add(ugyfel);
    }

    public void DeleteUgyfel(Ugyfel ugyfel)
    {
        _appDbContext.Ugyfelek.Remove(ugyfel);
    }

    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}