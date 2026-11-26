using AutoSzerelo.Api.Mappers;
using AutoSzerelo.Api.Services;
using AutoSzerelo.Contracts.Munkak;
using Microsoft.AspNetCore.Mvc;

namespace AutoSzerelo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MunkaController : ControllerBase
{
    private readonly ILogger<MunkaController> _logger;
    private readonly IMunkaService _munkaService;

    public MunkaController(ILogger<MunkaController> logger, IMunkaService munkaService)
    {
        _logger = logger;
        _munkaService = munkaService;
    }

    [HttpGet]
    public async Task<ActionResult<GetMunkakResponse>> GetMunkak()
    {
        _logger.LogInformation("Get Munkak request received");
        var munkak = await _munkaService.GetMunkakAsync();
        var aktivMunkakSzama = await _munkaService.GetAktivMunkakSzamaAsync();

        var response = new GetMunkakResponse
        {
            Munkak = munkak.ToDto(munka => _munkaService.CalculateBecsultMunkaora(munka, aktivMunkakSzama)),
        };

        _logger.LogInformation("Get Munkak request succeeded with response {@Response}", response);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMunkaResponse>> GetMunka(Guid id)
    {
        var munka = await _munkaService.GetMunkaWithUgyfelAsync(id);
        if (munka is null)
        {
            return NotFound();
        }

        var aktivMunkakSzama = await _munkaService.GetAktivMunkakSzamaAsync();

        return Ok(new GetMunkaResponse
        {
            Munka = munka.ToDto(_munkaService.CalculateBecsultMunkaora(munka, aktivMunkakSzama)),
        });
    }

    [HttpPost]
    public async Task<ActionResult<AddMunkaResponse>> AddMunka(AddMunkaRequest request)
    {
        _logger.LogInformation("Add Munka request received: {@Request}", request);

        var id = await _munkaService.AddMunkaAsync(request.ToMunkaEntity());

        var response = new AddMunkaResponse
        {
            Id = id,
        };

        _logger.LogInformation("Add Munka request succeeded with response {@Response}", response);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMunka(Guid id, [FromBody] UpdateMunkaRequest request)
    {
        await _munkaService.UpdateMunkaAsync(id, request.ToMunkaEntity());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMunka(Guid id)
    {
        await _munkaService.DeleteMunkaAsync(id);
        return NoContent();
    }
}