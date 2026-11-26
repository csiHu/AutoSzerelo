using AutoSzerelo.Api.Exceptions;
using AutoSzerelo.Api.Models;
using AutoSzerelo.Api.Repositories;
using AutoSzerelo.Contracts.Enums;

namespace AutoSzerelo.Api.Services;

public class MunkaService : IMunkaService
{
    private readonly IMunkaRepository _munkaRepository;
    private readonly IUgyfelRepository _ugyfelRepository;

    public MunkaService(IMunkaRepository munkaRepository, IUgyfelRepository ugyfelRepository)
    {
        if (munkaRepository is null)
        {
            throw new ArgumentNullException(nameof(munkaRepository));
        }

        if (ugyfelRepository is null)
        {
            throw new ArgumentNullException(nameof(ugyfelRepository));
        }

        _munkaRepository = munkaRepository;
        _ugyfelRepository = ugyfelRepository;
    }

    public Task<List<Munka>> GetMunkakAsync()
    {
        return _munkaRepository.GetMunkakAsync();
    }

    public Task<Munka> GetMunkaAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Munka ID cannot be empty.", nameof(id));
        }

        return _munkaRepository.GetMunkaAsync(id);
    }

    public Task<Munka> GetMunkaWithUgyfelAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Munka ID cannot be empty.", nameof(id));
        }

        return _munkaRepository.GetMunkaWithUgyfelAsync(id);
    }

    public async Task<List<Munka>> GetUgyfelMunkaiAsync(Guid ugyfelId)
    {
        if (await _ugyfelRepository.GetUgyfelAsync(ugyfelId) is null)
        {
            throw new EntityNotFoundException($"Ugyfel with id '{ugyfelId}' does not exist");
        }

        return await _munkaRepository.GetUgyfelMunkaiAsync(ugyfelId);
    }

    public async Task<Guid> AddMunkaAsync(Munka munka)
    {
        if (munka is null)
        {
            throw new ArgumentNullException(nameof(munka));
        }

        if (await _ugyfelRepository.GetUgyfelAsync(munka.UgyfelId) is null)
        {
            throw new EntityNotFoundException($"Ugyfel with id '{munka.UgyfelId}' does not exist");
        }

        if (munka.Allapot != MunkaAllapot.FelvettMunka)
        {
            throw new InvalidOperationException("Új munka kizárólag 'FelvettMunka' állapottal vehető fel.");
        }

        munka.Id = Guid.CreateVersion7();
        _munkaRepository.AddMunka(munka);
        await _munkaRepository.SaveChangesAsync();
        return munka.Id;
    }

    public async Task UpdateMunkaAsync(Guid id, Munka munka)
    {
        if (munka is null)
        {
            throw new ArgumentNullException(nameof(munka));
        }

        var munkaToUpdate = await GetRequiredMunkaAsync(id);

        if (await _ugyfelRepository.GetUgyfelAsync(munka.UgyfelId) is null)
        {
            throw new EntityNotFoundException($"Ugyfel with id '{munka.UgyfelId}' does not exist");
        }

        ValidateAllapotValtas(munkaToUpdate.Allapot, munka.Allapot, munka.JavitasiNaplo);

        munkaToUpdate.UgyfelId = munka.UgyfelId;
        munkaToUpdate.Rendszam = munka.Rendszam;
        munkaToUpdate.GyartasiEv = munka.GyartasiEv;
        munkaToUpdate.Kategoria = munka.Kategoria;
        munkaToUpdate.HibaLeiras = munka.HibaLeiras;
        munkaToUpdate.HibaSulyossag = munka.HibaSulyossag;
        munkaToUpdate.JavitasiNaplo = munka.JavitasiNaplo;
        munkaToUpdate.Allapot = munka.Allapot;

        await _munkaRepository.SaveChangesAsync();
    }

    public async Task DeleteMunkaAsync(Guid id)
    {
        var munka = await GetRequiredMunkaAsync(id);
        _munkaRepository.DeleteMunka(munka);
        await _munkaRepository.SaveChangesAsync();
    }

    public async Task<int> GetAktivMunkakSzamaAsync()
    {
        var munkak = await _munkaRepository.GetMunkakAsync();
        return munkak.Count(munka => munka.Allapot != MunkaAllapot.Befejezett);
    }

    public double CalculateBecsultMunkaora(Munka munka, int aktivMunkakSzama)
    {
        if (munka is null)
        {
            throw new ArgumentNullException(nameof(munka));
        }

        if (munka.Allapot == MunkaAllapot.Befejezett)
        {
            return 0;
        }

        double kategoriaAlapOra = munka.Kategoria switch
        {
            MunkaKategoria.Karosszeria => 3.0,
            MunkaKategoria.Motor => 8.0,
            MunkaKategoria.Futomu => 6.0,
            MunkaKategoria.Fekberendezes => 4.0,
            _ => 4.0,
        };

        int kor = Math.Max(0, DateTime.Now.Year - munka.GyartasiEv);
        double korSzorzo = kor switch
        {
            <= 5 => 0.5,
            <= 10 => 1.0,
            <= 20 => 1.5,
            _ => 2.0,
        };

        double sulyossagSzorzo = munka.HibaSulyossag switch
        {
            <= 2 => 0.2,
            <= 4 => 0.4,
            <= 7 => 0.6,
            <= 9 => 0.8,
            _ => 1.0,
        };

        double leterheltsegSzorzo = aktivMunkakSzama switch
        {
            <= 2 => 1.0,
            <= 5 => 1.2,
            <= 7 => 1.4,
            _ => 2.0,
        };

        return Math.Round(kategoriaAlapOra * korSzorzo * sulyossagSzorzo * leterheltsegSzorzo, 2);
    }

    private async Task<Munka> GetRequiredMunkaAsync(Guid id)
    {
        var munka = await _munkaRepository.GetMunkaAsync(id);
        if (munka is null)
        {
            throw new EntityNotFoundException($"Munka with id '{id}' does not exist");
        }

        return munka;
    }

    private static void ValidateAllapotValtas(MunkaAllapot jelenlegiAllapot, MunkaAllapot ujAllapot, string javitasiNaplo)
    {
        if (jelenlegiAllapot == ujAllapot)
        {
            if (ujAllapot == MunkaAllapot.Befejezett && string.IsNullOrWhiteSpace(javitasiNaplo))
            {
                throw new InvalidOperationException("Befejezett állapotú munkához kötelező a javítási napló kitöltése.");
            }

            return;
        }

        var ervenyesAtmenet = (jelenlegiAllapot, ujAllapot) switch
        {
            (MunkaAllapot.FelvettMunka, MunkaAllapot.ElvegzesAlatt) => true,
            (MunkaAllapot.ElvegzesAlatt, MunkaAllapot.Befejezett) => true,
            _ => false,
        };

        if (!ervenyesAtmenet)
        {
            throw new InvalidOperationException(
                $"Érvénytelen állapotátmenet: '{jelenlegiAllapot}' -> '{ujAllapot}'.");
        }

        if (ujAllapot == MunkaAllapot.Befejezett && string.IsNullOrWhiteSpace(javitasiNaplo))
        {
            throw new InvalidOperationException("A munka befejezéséhez kötelező kitölteni a javítási naplót.");
        }
    }
}