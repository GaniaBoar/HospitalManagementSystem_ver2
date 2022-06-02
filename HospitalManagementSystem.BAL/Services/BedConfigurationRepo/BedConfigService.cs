using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedNoRepo
{
    public class BedConfigService : IBedConfiService, IDisposable
    {
        readonly AppDbContext _context;
        public BedConfigService(AppDbContext appDbContext)
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
                BedConfiguration bed = (BedConfiguration)await Get(id);

                _context.BedConfiguration.Remove(bed);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Edit(int? id, BedConfiguration bed, CancellationToken ct = default)
        {
            BedConfiguration data = (BedConfiguration)await Get(id);

            try
            {
                data.Number = bed.Number;
                data.Description = bed.Description;
                data.BedType = await _context.BedType.FindAsync(bed.BedType.Id);
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
                return await _context.BedConfiguration
                    .Include(a=>a.BedType)
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
                var result = await _context.BedConfiguration
                    .Include(a => a.BedType)
                    .SingleOrDefaultAsync(a => a.Id == id);

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

        public async Task<bool> Post(BedConfiguration bedConfiguration, CancellationToken ct = default)
        {
            try
            {
                bedConfiguration.BedType = await _context.BedType.FindAsync(bedConfiguration.BedType.Id);
                await _context.BedConfiguration.AddAsync(bedConfiguration, ct);
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
