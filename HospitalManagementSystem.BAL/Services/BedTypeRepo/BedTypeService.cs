using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;

using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedRepo
{
    public class BedTypeService: IBedTypeService, IDisposable
    {
        readonly AppDbContext _context;
        public BedTypeService(AppDbContext appDbContext)
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
                BedType bedType = (BedType)await Get(id);

                _context.BedType.Remove(bedType);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public async Task<bool> Edit(int? id, BedType bedType, CancellationToken ct = default)
        {
            BedType data = (BedType)await Get(id);

            try
            {
                data.Name = bedType.Name;
                data.Price = bedType.Price;
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
                return await _context.BedType.ToListAsync();
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
                var result = await _context.BedType.FindAsync(id);

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

        public async Task<bool> Post(BedType bedType, CancellationToken ct = default)
        {
            try
            {
                await _context.BedType.AddAsync(bedType, ct);
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



