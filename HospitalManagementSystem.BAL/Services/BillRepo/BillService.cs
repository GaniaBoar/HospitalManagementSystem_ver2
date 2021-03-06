using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BillRepo
{
    public class BillService : IBillService, IDisposable
    {
        readonly AppDbContext _context;
        public BillService(AppDbContext appDbContext)
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
                Bill bill = (Bill)await Get(id);

                _context.Bill.Remove(bill);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Edit(int? id, Bill bill, CancellationToken ct = default)
        {
            Bill data = (Bill)await Get(id);

            try
            {

                data.BillDate = bill.BillDate;
                data.TestName = bill.TestName;
                data.TestCharge = bill.TestCharge;
                data.Status = bill.Status;
                data.Total = bill.Total;
                data.BedAllocation = await _context.BedAllocation.FindAsync(bill.BedAllocation.Id);
                data.Medicines = await _context.Medicines.FindAsync(bill.Medicines.Id);
                data.PatientRegistration = await _context.PatientRegistration.FindAsync(bill.PatientRegistration.Id);
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
                return await _context.Bill.Include(a => a.BedAllocation).
                    ThenInclude(a => a.BedConfiguration).Include(a => a.Medicines).
                    Include(a => a.PatientRegistration).ToListAsync();
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
                var result = await _context.Bill.Include(a => a.BedAllocation).
                    ThenInclude(a => a.BedConfiguration).Include(a => a.Medicines).
                    Include(a => a.PatientRegistration).
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

        public async Task<bool> Post(Bill bill, CancellationToken ct = default)
        {
            try
            {
                bill.BedAllocation = await _context.BedAllocation.FindAsync(bill.BedAllocation.Id);
                bill.Medicines = await _context.Medicines.FindAsync(bill.Medicines.Id);
                bill.PatientRegistration = await _context.PatientRegistration.FindAsync(bill.PatientRegistration.Id);

                await _context.Bill.AddAsync(bill, ct);
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

