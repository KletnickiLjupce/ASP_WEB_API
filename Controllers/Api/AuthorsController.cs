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
using API_HM.DAL;
using API_HM.Models;
using API_HM.ViewModels;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace API_HM.Controllers.Api
{
    public class AuthorsController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Authors
        [HttpGet]
        [Route("api/authors")]
        public IEnumerable<AuthorViewModel> GetAuthors()
        {
            var authorsData = (from authors in db.Authors

                               select new AuthorViewModel
                               {
                                   Id = authors.AuthorId,
                                   Name = authors.Name
                               }
                ).ToList();
            return authorsData;
        }

        // GET: api/authors/2
        [HttpGet]
        [Route("api/authors/{id}")]
        public IEnumerable<AuthorViewModel> GetAuthorsById(int id)
        {
            var authorsData = (from authors in db.Authors
                               where authors.AuthorId == id
                               select new AuthorViewModel
                               {
                                   Id = authors.AuthorId,
                                   Name = authors.Name
                               }
                ).ToList();
            return authorsData;
        }

        [HttpGet]
        [Route("api/authors/{id}/articles")]
        public IEnumerable<ArticleViewModel> GetArticlesFromAuthorIdAndFilterByTitle(int id, string title="", string level="" , string publishedDate_dd_mm_yyyy = "")
        {
            if (title != "" )
            {
                if(publishedDate_dd_mm_yyyy != "")
                {
                    var inputPublishedDate = DateTime.Parse(publishedDate_dd_mm_yyyy);
                    var aricleData = (from article in db.Articles
                                      join author in db.Authors
                                      on article.AuthorId equals author.AuthorId
                                      join nomenArticleLevels in db.nomenArticleLevels
                                      on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                      where (article.AuthorId == id) && (article.Title.Contains(title)) && (DbFunctions.TruncateTime(article.PublishedDate) == inputPublishedDate.Date)
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
                                      where (article.AuthorId == id) && (article.Title.Contains(title))
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
            if(level != "")
            {
                var aricleData = (from article in db.Articles
                                  join author in db.Authors
                                  on article.AuthorId equals author.AuthorId
                                  join nomenArticleLevels in db.nomenArticleLevels
                                  on article.AuthorId equals nomenArticleLevels.NomenArticleLevelId
                                  where (article.AuthorId == id) && (article.NomenArticleLevel.Name == level)
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
                                  where (article.AuthorId == id) 
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

        // POST: api/Authors
        [HttpPost]
        [Route("api/authors")]
        public async Task<IHttpActionResult> PostAuthor(Author author)
        {
            var request = Request;
            var headers = request.Headers;
            var username = headers.GetValues("username").First();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userdata = (
                from users in db.Users 
                join nomenUserRole in db.NomenUserRoles
                on users.NomenUserRoleId equals nomenUserRole.NomenUserRoleId

                where( users.NomenUserRole.Name == "admin") && (users.Name == username)
                select new UserViewModel
                {
                }
                ).ToList();
            // user founnd and is Admin 
            if(userdata.Count == 1)
            {
                db.Authors.Add(author);
                await db.SaveChangesAsync();
                return Ok("Author Created Succesfully");
            }
            else
            {
                return BadRequest("Username not existing or is not admin, cant create author");
            }   
        }

        // DELETE: api/Authors/5
        [HttpDelete, Route("api/authors/{id}")]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            Author author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            await db.SaveChangesAsync();

            return Ok(author);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.AuthorId == id) > 0;
        }
    }
}