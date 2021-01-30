using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HM.Models
{
    public class Article
    {
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }


        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }


        [ForeignKey("NomenArticleLevel")]
        public int? NomenArticleLevelId { get; set; }

        public virtual NomenArticleLevel NomenArticleLevel { get; set; }




    }
}