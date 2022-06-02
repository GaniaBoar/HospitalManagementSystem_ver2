using HospitalManagementSystem.BAL.Services.MedicineRepo;
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

    public class MedicineController : ControllerBase
    {
        readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct=default)
        {
            return Ok(await _medicineService.Get(ct));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id, CancellationToken ct = default)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _medicineService.Get(id,ct);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(Medicines medicines)
        {
            var result = await _medicineService.SaveAsync(medicines);
            return Ok(result);
        }


        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, Medicines medicines)

        {

            var result = await _medicineService.Edit(id, medicines);
            return Ok(result);
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(int? id,CancellationToken ct=default)
        {
            return Ok(await _medicineService.Delete(id,ct));
        }
    }
}
