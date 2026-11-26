using AutoSzerelo.Api.Mappers;
using AutoSzerelo.Api.Services;
using AutoSzerelo.Contracts.Ugyfelek;
using AutoSzerelo.Contracts.Munkak;
using Microsoft.AspNetCore.Mvc;

namespace AutoSzerelo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UgyfelController : ControllerBase
{
    private readonly ILogger<UgyfelController> _logger;
    private readonly IUgyfelService _ugyfelService;
    private readonly IMunkaService _munkaService;

    public UgyfelController(ILogger<UgyfelController> logger, IUgyfelService ugyfelService, IMunkaService munkaService)
    {
        _logger = logger;
        _ugyfelService = ugyfelService;
        _munkaService = munkaService;
    }

    [HttpGet]
    public async Task<ActionResult<GetUgyfelekResponse>> GetUgyfelek()
    {
        _logger.LogInformation("Get Ugyfelek request received");
        var ugyfelek = await _ugyfelService.GetUgyfelekAsync();
        var response = new GetUgyfelekResponse
        {
            Ugyfelek = ugyfelek.ToDto(),
        };

        _logger.LogInformation("Get Ugyfelek request succeeded with response {@Response}", response);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUgyfelResponse>> GetUgyfel(Guid id)
    {
        var ugyfel = await _ugyfelService.GetUgyfelAsync(id);

        if (ugyfel is not null)
        {
            return Ok(
                new GetUgyfelResponse
                {
                    Ugyfel = ugyfel.ToDto(),
                });
        }

        return NotFound();
    }

    [HttpGet("{id}/munkak")]
    public async Task<ActionResult<GetMunkakResponse>> GetUgyfelMunkai(Guid id)
    {
        var munkak = await _munkaService.GetUgyfelMunkaiAsync(id);
        var aktivMunkakSzama = await _munkaService.GetAktivMunkakSzamaAsync();
        return Ok(new GetMunkakResponse
        {
            Munkak = munkak.ToDto(munka => _munkaService.CalculateBecsultMunkaora(munka, aktivMunkakSzama)),
        });
    }

    [HttpPost]
    public async Task<ActionResult<AddUgyfelResponse>> AddUgyfel(AddUgyfelRequest request)
    {
        _logger.LogInformation("Add Ugyfel request received: {@Request}", request);

        var id = await _ugyfelService.AddUgyfelAsync(request.ToUgyfelEntity());

        var response = new AddUgyfelResponse
        {
            Id = id,
        };

        _logger.LogInformation("Add Ugyfel request succeeded with response {@Response}", response);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUgyfel(Guid id, [FromBody] UpdateUgyfelRequest request)
    {
        await _ugyfelService.UpdateUgyfelAsync(id, request.ToUgyfelEntity());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUgyfel(Guid id)
    {
        await _ugyfelService.DeleteUgyfelAsync(id);
        return NoContent();
    }
}