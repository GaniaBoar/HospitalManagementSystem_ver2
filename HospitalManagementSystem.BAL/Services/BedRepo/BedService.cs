using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedRepo
{
    public class BedService: IBedService, IDisposable
    {
        readonly AppDbContext _context;
        public BedService(AppDbContext appDbContext)
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
                Bed bed = (Bed)await Get(id);

                _context.Bed.Remove(bed);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public async Task<bool> Edit(int? id, Bed bed, CancellationToken ct = default)
        {
            Bed data = (Bed)await Get(id);

            try
            {
                data.Type = bed.Type;
                data.NumberOfBed = bed.NumberOfBed;
                data.Description = bed.Description;
                data.Charges = bed.Charges;
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
                return await _context.Bed.ToListAsync();
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
                var result = await _context.Bed.FindAsync(id);

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

        public async Task<bool> Post(Bed bed, CancellationToken ct = default)
        {
            try
            {
                await _context.Bed.AddAsync(bed, ct);
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
