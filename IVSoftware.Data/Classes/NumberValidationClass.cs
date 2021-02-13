using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Classes
{
    public class NumberValidationClass : ValidationAttribute
    {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            try    
            {
                if(value != null)
                {
                    float data = 0f;
                    bool result = float.TryParse(value.ToString(), out data);
                    if (result) return ValidationResult.Success;
                }
            }
            catch (Exception)
            {
                
            }

            return new ValidationResult("Este campo es numérico");
        }
    }
}
