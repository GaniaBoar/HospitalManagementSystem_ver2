using HospitalManagementSystem.BAL.Services.DashBoardRepo;
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
    public class DashBoardController : ControllerBase
    {
        readonly IDashBoardService _dashBoardService;
        public DashBoardController( IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct = default)
        {
            return Ok(await _dashBoardService.Get(ct));
        }


    }
}
