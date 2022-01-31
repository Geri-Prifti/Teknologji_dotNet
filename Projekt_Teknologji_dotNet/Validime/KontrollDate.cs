using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_Teknologji_dotNet.Validime
{
    public class KontrollDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime ReservationDate = Convert.ToDateTime(value);
                if(ReservationDate < DateTime.Now.Date)
                {
                    return new ValidationResult("Data e rezervimit nuk mund te vendoset ne nje dite te kaluar!");
                }
            }
            return ValidationResult.Success;
        }
    }
}