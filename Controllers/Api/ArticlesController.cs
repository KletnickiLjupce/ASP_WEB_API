using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using API_HM.DAL;
using API_HM.Models;
using Newtonsoft.Json;
using API_HM.ViewModels;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;

namespace API_HM.Controllers.Api
{
    public class ArticlesController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/articles
        [HttpGet]
        [Route("api/articles")]
        public IEnumerable<ArticleViewModel> GetArticles(string title = "", string level = "",  string datePublishedAfter_dd_mm_yyyy = "")
        {
            if (title != "")
            {
                if (level != "")
                {
                    if (datePublishedAfter_dd_mm_yyyy != "") 
                    {
                        var inputPublishedDate = DateTime.Parse(datePublishedAfter_dd_mm_yyyy);
                        var aricleData = (from article in db.Articles
                                          join author in db.Authors
                                          on article.AuthorId equals author.AuthorId
                                          join nomenArticleLevels in db.nomenArticleLevels
                                          on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                          where article.Title.Contains(title) && (article.NomenArticleLevel.Name == level) && (DbFunctions.TruncateTime(article.PublishedDate) > inputPublishedDate.Date)
                                          select new ArticleViewModel
                                          {
                                              Id = article.Id,
                                              Title = article.Title,
                                              PublishedDate = article.PublishedDate,
                                              Author = author.Name,
                                              Level = article.NomenArticleLevel.Name
                                          }).ToList();
                        return aricleData;
                    }
                    else
                    {
                        var aricleData = (from article in db.Articles
                                          join author in db.Authors
                                          on article.AuthorId equals author.AuthorId
                                          join nomenArticleLevels in db.nomenArticleLevels
                                          on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                          where article.Title.Contains(title) && (article.NomenArticleLevel.Name == level)
                                          select new ArticleViewModel
                                          {
                                              Id = article.Id,
                                              Title = article.Title,
                                              PublishedDate = article.PublishedDate,
                                              Author = author.Name,
                                              Level = article.NomenArticleLevel.Name
                                          }).ToList();
                        return aricleData;
                    }          
                }
                else
                {
                    var aricleData = (from article in db.Articles
                                      join author in db.Authors
                                      on article.AuthorId equals author.AuthorId
                                      join nomenArticleLevels in db.nomenArticleLevels
                                      on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                      where article.Title.Contains(title)
                                      select new ArticleViewModel
                                      {
                                          Id = article.Id,
                                          Title = article.Title,
                                          PublishedDate = article.PublishedDate,
                                          Author = author.Name,
                                          Level = nomenArticleLevels.Name
                                      }).ToList();
                    return aricleData;
                }
            }
            else
            {
                var aricleData = (from article in db.Articles
                                  join author in db.Authors
                                  on article.AuthorId equals author.AuthorId
                                  join nomenArticleLevels in db.nomenArticleLevels
                                  on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                  select new ArticleViewModel
                                  {
                                      Id = article.Id,
                                      Title = article.Title,
                                      PublishedDate = article.PublishedDate,
                                      Author = author.Name,
                                      Level = nomenArticleLevels.Name
                                  }).ToList();
                return aricleData;
            }
        }

        // PUT: api/articles/5
        [HttpPut]
        [Route("api/articles/{id}")]
        public async Task<IHttpActionResult> PutArticle(string id, Article article)
        {
            var request = Request;
            var headers = request.Headers;
            var username = headers.GetValues("username").First();

            //user validation
            //var user = new User();
            var userdata = (from user in db.Users
                            where user.UserName == username
                            select new UserViewModel

                            {
                                Username = user.Name

                            }).ToList();

            if (userdata.Count == 1)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != article.Id)
                {
                    return BadRequest();
                }
                db.Entry(article).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("Article Updated");
            }
            return BadRequest("User not authorized to update article");
        }

        // POST: api/Articles
        [HttpPost]
        [Route("api/articles")]
        public async Task<IHttpActionResult> PostArticle(Article article)
        {
            var request = Request;
            var headers = request.Headers;
            var username = headers.GetValues("username").First();

            //user validation
            //var user = new User();
            var userdata = (from user in db.Users
                            where user.UserName == username
                            select new UserViewModel

                            {
                                Username = user.Name

                            }).ToList();

            if (userdata.Count == 1)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.Articles.Add(article);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (ArticleExists(article.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("Article Created");
            }
            return BadRequest("User not authorized ");
        }

        // DELETE: api/Articles/abcd
        [ResponseType(typeof(Article))]
        [HttpDelete, Route("api/articles/{id}")]
        public async Task<IHttpActionResult> DeleteArticle(string id)
        {
            var request = Request;
            var headers = request.Headers;
            var username = headers.GetValues("username").First();

            var userdata = (from user in db.Users
                            where user.UserName == username
                            select new UserViewModel
                            {
                                Username = user.Name

                            }).ToList();
            // check if username exist
            if (userdata.Count == 1)
            {
                Article article = await db.Articles.FindAsync(id);
                if (article == null)
                {
                    return NotFound();
                }
                // check if current user UserName is same as the author of the article
                if (article.Author.Name == userdata[0].Username)
                {
                    db.Articles.Remove(article);
                    await db.SaveChangesAsync();

                    return Ok("Record Deleted");
                }
                else
                {
                    return BadRequest("This user is not the author of the article");
                }
            }
            return BadRequest("username is not valid");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(string id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}