using HospitalManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedConfigRepo
{
    public interface IBedAllocationRepo
    {
        Task<bool> Post(BedAllocation bedallocation, CancellationToken ct = default);
        Task<object> Get(CancellationToken ct = default);
        Task<object> Get(int? id, CancellationToken ct = default);
        Task<bool> Edit(int? id, BedAllocation bedallocation, CancellationToken ct = default);
        Task<bool> Delete(int? id, CancellationToken ct = default);
    }
}
