using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HM.Models
{
    public class NomenUserRole
    {
        [Key]
        public int NomenUserRoleId { get; set; }

        public string Name { get; set; }

       
        public virtual ICollection<User> Users { get; set; }
    }
}