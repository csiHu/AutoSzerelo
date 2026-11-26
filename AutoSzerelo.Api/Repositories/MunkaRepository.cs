using AutoSzerelo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSzerelo.Api.Repositories;

public class MunkaRepository : IMunkaRepository
{
    private readonly AppDbContext _appDbContext;

    public MunkaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Munka>> GetMunkakAsync()
    {
        return await _appDbContext.Munkak
            .AsNoTracking()
            .Include(munka => munka.Ugyfel)
            .ToListAsync();
    }

    public async Task<Munka> GetMunkaAsync(Guid id)
    {
        return await _appDbContext.Munkak
            .FirstOrDefaultAsync(munka => munka.Id == id);
    }

    public async Task<Munka> GetMunkaWithUgyfelAsync(Guid id)
    {
        return await _appDbContext.Munkak
            .AsNoTracking()
            .Include(munka => munka.Ugyfel)
            .FirstOrDefaultAsync(munka => munka.Id == id);
    }

    public async Task<List<Munka>> GetUgyfelMunkaiAsync(Guid ugyfelId)
    {
        return await _appDbContext.Munkak
            .AsNoTracking()
            .Include(munka => munka.Ugyfel)
            .Where(munka => munka.UgyfelId == ugyfelId)
            .ToListAsync();
    }

    public void AddMunka(Munka munka)
    {
        _appDbContext.Munkak.Add(munka);
    }

    public void DeleteMunka(Munka munka)
    {
        _appDbContext.Munkak.Remove(munka);
    }

    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}