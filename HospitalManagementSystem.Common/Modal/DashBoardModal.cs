using HospitalManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HospitalManagementSystem.Common.Modal
{
    public class DashBoardModal
    {
        [NotMapped]
        public Medicines Medicines { get; set; }
        [NotMapped]
        public PatientRegistration patientRegistration { get; set; }
    }
    public class DashboardCount
    {
        public int Medicine { get; set; }
        public int Patient { get; set; }
        public int Stock { get; set; }
        public int Bed { get; set; }
    }
}
