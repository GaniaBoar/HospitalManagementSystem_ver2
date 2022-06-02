using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedConfigRepo
{
    public class BedAllocationRepo : IBedAllocationRepo, IDisposable
    {
        readonly AppDbContext _context;
        public BedAllocationRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Delete(int? id, CancellationToken ct = default)
        {
            try
            {
                BedAllocation bedAllocation = (BedAllocation)await Get(id);

                _context.BedAllocation.Remove(bedAllocation);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Edit(int? id, BedAllocation bedAllocation, CancellationToken ct = default)
        {
            BedAllocation data = (BedAllocation)await Get(id);

            try
            {
                data.AllocatedOn = bedAllocation.AllocatedOn;
                data.AllocatedTill = bedAllocation.AllocatedTill;
                data.BedConfiguration = await _context.BedConfiguration.FindAsync(bedAllocation.BedConfiguration.Id);
                data.PatientRegistration = await _context.PatientRegistration.FindAsync(bedAllocation.PatientRegistration.Id);
                data.Status = bedAllocation.Status;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> Get(CancellationToken ct = default)
        {
            try
            {
                return await _context.BedAllocation
                    .Include(a => a.BedConfiguration)
                    .ThenInclude(a=>a.BedType)
                    .Include(a => a.PatientRegistration)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> Get(int? id, CancellationToken ct = default)
        {
            try
            {
                var result = await _context.BedAllocation.Include(a => a.BedConfiguration).
                    ThenInclude(a => a.BedType).Include(a => a.PatientRegistration).
                    SingleOrDefaultAsync(a => a.Id == id);

                if (result == null)
                {
                    throw new Exception("No records found.");
                }
                return result;

                


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Post(BedAllocation bedAllocation, CancellationToken ct = default)
        {
            try
            {
                bedAllocation.BedConfiguration = await _context.BedConfiguration.FindAsync(bedAllocation.BedConfiguration.Id);
                bedAllocation.PatientRegistration = await _context.PatientRegistration.FindAsync(bedAllocation.PatientRegistration.Id);
                await _context.BedAllocation.AddAsync(bedAllocation, ct);
                await _context.SaveChangesAsync(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
