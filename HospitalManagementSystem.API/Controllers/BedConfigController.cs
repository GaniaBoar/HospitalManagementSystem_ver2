using HospitalManagementSystem.BAL.Services.BedNoRepo;
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

    public class BedConfigController : ControllerBase
    {
        readonly IBedConfiService _bedService;
        public BedConfigController(IBedConfiService bedService)
        {
            _bedService = bedService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(BedConfiguration bedConfiguration)
        {
            var result = await _bedService.Post(bedConfiguration);
            return Ok(result);
        }


        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, BedConfiguration bed)

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
