using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class ModelValidationResult
    {
        [DataMember]
        public bool IsValid { get; set; }
        [DataMember]
        public List<ModelValidationError> ValidationErrors { get; set; }

        public ModelValidationResult()
        {
            this.ValidationErrors = new List<ModelValidationError>();
        }

        public override string ToString()
        {
            if (this.ValidationErrors != null && this.ValidationErrors.Count > 0)
            {
                StringBuilder errorBuilder = new StringBuilder();
                foreach(var item in this.ValidationErrors)
                {
                    errorBuilder.AppendFormat("{0}:{1}\r\n", item.PropertyName,item.ErrorMessage);
                }
                
            }
            return base.ToString();
        }
    }
}
