using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.CreateFundos;
using CaseItau.Application.Fundos.DeleteFundos;
using CaseItau.Application.Fundos.EditFundos;
using CaseItau.Application.Fundos.GetAllFundos;
using CaseItau.Application.Fundos.GetFundos;
using CaseItau.Application.Fundos.UpdatePatrimonioCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Controllers.Fundos;

[ApiController]
[Route("api/fundo")]
public class FundosController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> SearchFundosById(string id, CancellationToken cancellationToken)
    {
        var query = new GetFundosByCodeQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet()]
    public async Task<IActionResult> SearchAlldFundos(CancellationToken cancellationToken)
    {
        var query = new GetAllFundosQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFundos(
        CreateFundosRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateFundosCommand(
            request.Codigo,
            request.Nome,
            request.Cnpj,
            request.TipoFundo,
            request.Patrimonio);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(SearchFundosById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditFundos(
        string id,
        EditFundosRequest request,
        CancellationToken cancellationToken)
    {
        var command = new EditFundosCommand(
            id,
            request.Nome,
            request.Cnpj,
            request.TipoFundo);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("{id}/patrimonio")]
    public async Task<IActionResult> UpdatePatrimonio(
        string id,
        UpdatePatrimonioRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePatrimonioCommand(
            id,
            request.Value,
            request.Operation);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Delete(string codigo, CancellationToken cancellationToken)
    {
        var command = new DeleteFundosCommand(codigo);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}