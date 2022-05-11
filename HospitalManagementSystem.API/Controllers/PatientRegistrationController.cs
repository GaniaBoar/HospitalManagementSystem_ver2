using HospitalManagementSystem.BAL.Services.PatientRepo;
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
    [Authorize]
    public class PatientRegistrationController : ControllerBase
    {
        readonly IPatientRegistrationService _patientRegistrationService;
        public PatientRegistrationController(IPatientRegistrationService patientRegistrationService)
        {
            _patientRegistrationService = patientRegistrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PatientRegistration patientRegistration)
        {
            var result = await _patientRegistrationService.Post(patientRegistration);
            return Ok(result);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, PatientRegistration patientRegistration)

        {

            var result = await _patientRegistrationService.Edit(id, patientRegistration);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct = default)
        {
            return Ok(await _patientRegistrationService.Get(ct));
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id, CancellationToken ct = default)
        {
            if (id == null)
                return BadRequest(nameof(id));
            var result = await _patientRegistrationService.Get(id, ct);
            return Ok(result);
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete(int? id, CancellationToken ct = default)
        {
            return Ok(await _patientRegistrationService.Delete(id, ct));
        }
    }
}
