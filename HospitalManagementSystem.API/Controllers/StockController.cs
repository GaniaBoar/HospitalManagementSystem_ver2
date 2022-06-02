using HospitalManagementSystem.BAL.Services.StockRepo;
using HospitalManagementSystem.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StockController : ControllerBase
    {
        readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Stock stock)
        {
            var result = await _stockService.Post(stock);
            return Ok(result);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, Stock stock)

        {

            var result = await _stockService.Edit(id, stock);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct = default)
        {
            return Ok(await _stockService.Get(ct));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id, CancellationToken ct = default)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _stockService.Get(id, ct);
            return Ok(result);
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(int? id, CancellationToken ct = default)
        {
            return Ok(await _stockService.Delete(id, ct));
        }
    
}
}
