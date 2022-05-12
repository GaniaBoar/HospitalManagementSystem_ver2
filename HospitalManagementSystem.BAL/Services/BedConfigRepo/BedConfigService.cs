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
        public class BedConfigService : IBedConfigService, IDisposable
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
                    BedConfig bed = (BedConfig)await Get(id);

                    _context.BedConfig.Remove(bed);
                    var result = await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



            public async Task<bool> Edit(int? id, BedConfig bed, CancellationToken ct = default)
            {
                BedConfig data = (BedConfig)await Get(id);

                try
                {
                    data.Description = bed.Description;
                    data.Status = bed.Status;
                   
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
                    return await _context.BedConfig.ToListAsync();
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
                    var result = await _context.BedConfig.FindAsync(id);

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

            public async Task<bool> Post(BedConfig bed, CancellationToken ct = default)
            {
                try
                {
                    await _context.BedConfig.AddAsync(bed, ct);
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
