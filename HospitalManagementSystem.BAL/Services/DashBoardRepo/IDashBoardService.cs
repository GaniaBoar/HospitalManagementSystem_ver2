using HospitalManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.DashBoardRepo
{
    public interface IDashBoardService
    {
        Task<object> Get(Medicines medicines, CancellationToken ct = default);
        Task<object> Get(CancellationToken ct = default);

    }
}
