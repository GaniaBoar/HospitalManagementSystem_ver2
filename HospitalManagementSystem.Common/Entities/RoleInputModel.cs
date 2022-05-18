
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Common.Entities
{
    public class RolesInputModel
    {
   
        public string Id { get; set; }

        [Required(ErrorMessage = "RoleName is required")]
        public string Name { get; set; }

   
       
    }

}
