using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.DashBoardRepo
{
    public class DashBoardService : IDashBoardService, IDisposable
    {
        readonly AppDbContext _context;
        public DashBoardService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Dispose()
        {
            _context.Dispose();
        }




        public async Task<object> Get(CancellationToken ct = default)
        {
            try
            {
                return await _context.Medicines.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<object> IDashBoardService.Get(Medicines medicines, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
