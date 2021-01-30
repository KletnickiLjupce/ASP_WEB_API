using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HM.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }


        [ForeignKey("NomenUserRole")]
        public int? NomenUserRoleId { get; set; }

        public virtual NomenUserRole NomenUserRole { get; set; }
    }
}