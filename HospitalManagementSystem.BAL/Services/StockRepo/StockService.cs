
using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.StockRepo
{
    public class StockService: IStockService, IDisposable
    {
        readonly AppDbContext _context;
        public StockService(AppDbContext appDbContext)
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
                Stock stock = (Stock)await Get(id);

                _context.Stock.Remove(stock);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Edit(int? id, Stock stock, CancellationToken ct = default)
        {
            Stock data = (Stock)await Get(id);

            try
            {
                data.Name = stock.Name;
                data.Price = stock.Price;
                data.Description = stock.Description;
                data.Quantity = stock.Quantity;
                data.Status = stock.Status;
                data.Category = stock.Category;
                data.Supplier = stock.Supplier;
                data.PurchaseDate = stock.PurchaseDate;
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
                return await _context.Stock.ToListAsync();
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
                var result = await _context.Stock.FindAsync(id);

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

        public async Task<bool> Post(Stock stock , CancellationToken ct = default)
        {
            try
            {
                await _context.Stock.AddAsync(stock, ct);
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
