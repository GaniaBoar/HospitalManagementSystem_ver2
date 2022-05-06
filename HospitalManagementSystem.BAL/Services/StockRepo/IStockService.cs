using HospitalManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.StockRepo
{
    public interface IStockService
    {
        Task<bool> Post(Stock stock, CancellationToken ct = default);
        Task<object> Get(CancellationToken ct = default);
        Task<object> Get(int? id, CancellationToken ct = default);
        Task<bool> Edit(int? id, Stock stock, CancellationToken ct = default);
        Task<bool> Delete(int? id, CancellationToken ct = default);
    }
}
