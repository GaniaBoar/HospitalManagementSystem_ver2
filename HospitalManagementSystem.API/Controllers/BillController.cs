//using HospitalManagementSystem.BAL.Services.BillRepo;
//using HospitalManagementSystem.Common.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading;
//using System.Threading.Tasks;

//namespace HospitalManagementSystem.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BillController : ControllerBase
//    {
//        readonly IBillService _billService;
//        public BillController(IBillService billService)
//        {
//            _billService = billService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post(Bill bill)
//        {
//            var result = await _billService.Post(bill);
//            return Ok(result);
//        }

//        [HttpPut("{id?}")]
//        public async Task<IActionResult> Put(int? id, Bill bill)

//        {

//            var result = await _billService.Edit(id, bill);
//            return Ok(result);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Get(CancellationToken ct = default)
//        {
//            return Ok(await _billService.Get(ct));
//        }

//        [HttpGet("{id?}")]
//        public async Task<IActionResult> Get(int? id, CancellationToken ct = default)
//        {
//            if (id == null)
//                return BadRequest(nameof(id));
//            var result = await _billService.Get(id, ct);
//            return Ok(result);
//        }

//        [HttpDelete("{id?}")]
//        public async Task<IActionResult> Delete(int? id, CancellationToken ct = default)
//        {
//            return Ok(await _billService.Delete(id, ct));
//        }
//    }
//}
