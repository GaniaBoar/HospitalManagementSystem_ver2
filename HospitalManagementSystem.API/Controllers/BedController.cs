using HospitalManagementSystem.BAL.Services.BedRepo;
using HospitalManagementSystem.Common.Entities;
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
    public class BedController : ControllerBase
    {
        readonly IBedService _bedService;
        public BedController(IBedService bedService)
        {
            _bedService = bedService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Bed bed)
        {
            var result = await _bedService.Post(bed);
            return Ok(result);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, Bed bed)

        {

            var result = await _bedService.Edit(id, bed);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct = default)
        {
            return Ok(await _bedService.Get(ct));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id, CancellationToken ct = default)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _bedService.Get(id, ct);
            return Ok(result);
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(int? id, CancellationToken ct = default)
        {
            return Ok(await _bedService.Delete(id, ct));
        }
    }
}
