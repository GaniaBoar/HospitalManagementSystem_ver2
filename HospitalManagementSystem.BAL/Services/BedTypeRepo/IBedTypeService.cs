using HospitalManagementSystem.Common.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.BedRepo
{
    public interface IBedTypeService
    {
        Task<bool> Post(BedType bedType, CancellationToken ct = default);
        Task<object> Get(CancellationToken ct = default);
        Task<object> Get(int? id, CancellationToken ct = default);
        Task<bool> Edit(int? id, BedType bedType, CancellationToken ct = default);
        Task<bool> Delete(int? id, CancellationToken ct = default);
    }
}
