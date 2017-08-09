using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Models
{
    [Table("MR0001_USER_MSTR")]
    public class MR0001_USER_MSTR
    {
        public MR0001_USER_MSTR()
        {

        }

        [Key]
        [MaxLength(50)]
        public Guid MR0001_PK { get; set; }
        [Column(TypeName = "int")]
        public int MR0001_ID { get; set; }
        public string MR0001_Name { get; set; }
        public string MR0001_PassWord { get; set; }
        public string MR0001_Email { get; set; }
        public Guid MR0001_Company_RK { get; set; }
        [ForeignKey("MR0001_Company_RK")]
        public virtual MR0003_COMPANY MR0001_Company { get; set; }
        [NotMapped]
        public string ActionMode { get; set; }
    }
}
