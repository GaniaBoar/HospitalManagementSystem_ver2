
using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.MedicineRepo
{
    public class MedicineService : IMedicineService, IDisposable
    {
        readonly AppDbContext _context;
        public MedicineService(AppDbContext appDbContext)
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
                return await _context.Medicines.ToListAsync();
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
                var result = await _context.Medicines.FindAsync(id);
               
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
        public async Task<bool> Edit(int?id, Medicines medicines, CancellationToken ct = default)
        {
            Medicines data = (Medicines)await Get(id);
          
            try
            {
                data.Name = medicines.Name;
                data.Composition = medicines.Composition;
                data.Price = medicines.Price;
                data.Quantity = medicines.Quantity;
                data.BatchId = medicines.BatchId;
                data.Mfddate = medicines.Mfddate;
                data.Expdate = medicines.Expdate;
                data.Availability = medicines.Availability;
                data.CompanyName = medicines.CompanyName;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        public async Task<bool> SaveAsync(Medicines medicines, CancellationToken ct = default)
        {
            try
            {
                await _context.Medicines.AddAsync(medicines, ct);
                await _context.SaveChangesAsync(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int? id, CancellationToken ct = default)
        {
            try
            {
                Medicines medicine = (Medicines)await Get(id);
               
                _context.Medicines.Remove(medicine);
                var result=await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
