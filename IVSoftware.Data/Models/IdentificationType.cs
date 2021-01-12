﻿using IVSoftware.Data.Models.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class IdentificationType : BaseModel<int>
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}