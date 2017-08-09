using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Models
{
    [Table("MR0003_COMPANY")]
    public class MR0003_COMPANY
    {
        public MR0003_COMPANY()
        {

        }

        [Key]
        [MaxLength(50)]
        public Guid MR0003_PK { get; set; }
        public string MR0003_Name { get; set; }
        [NotMapped]
        public string ActionMode { get; set; }
    }
}
