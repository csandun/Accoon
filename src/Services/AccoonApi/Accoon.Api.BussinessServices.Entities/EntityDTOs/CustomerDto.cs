using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Accoon.Api.BussinessServices.Entities.EntityDTOs
{
    public  class CustomerDto
    {        
        public long Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be long than 50 characters")]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
