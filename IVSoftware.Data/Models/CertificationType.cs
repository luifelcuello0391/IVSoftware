﻿using IVSoftware.Data.Models.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class CertificationType : BaseModel<int>
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
}