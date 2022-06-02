using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
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




        public async Task<DashboardCount> Get(CancellationToken ct = default)
        {
            try
            {
                DashboardCount count = new DashboardCount();
                count.Medicine=await _context.Medicines.CountAsync();
                count.Patient = await _context.PatientRegistration.CountAsync();
                count.Bed = await _context.BedConfiguration.CountAsync();
                count.Stock = await _context.Stock.CountAsync();
                return count; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
