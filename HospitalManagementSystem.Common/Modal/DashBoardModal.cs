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
}
