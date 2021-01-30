using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_HM.ViewModels
{
    public class ArticleViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Level { get; set; }

        public string Author { get; set; }



    }
}