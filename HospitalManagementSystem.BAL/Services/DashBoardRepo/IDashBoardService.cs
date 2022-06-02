using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.DashBoardRepo
{
    public interface IDashBoardService
    {
        Task<DashboardCount> Get(CancellationToken ct = default);

    }
}
