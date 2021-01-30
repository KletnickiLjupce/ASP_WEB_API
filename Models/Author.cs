using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API_HM.Models
{
    public class Author
    {

        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }


    }
}