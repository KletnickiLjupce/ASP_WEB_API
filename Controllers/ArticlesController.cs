using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using API_HM.DAL;
using API_HM.Models;

namespace API_HM.Controllers
{
    public class ArticlesController : Controller
    {
        private APIContext db = new APIContext();

        // GET: Articles
        public async Task<ActionResult> Index()
        {
            var articles = db.Articles.Include(a => a.Author).Include(a => a.NomenArticleLevel);
            return View(await articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.NomenArticleLevelId = new SelectList(db.nomenArticleLevels, "NomenArticleLevelId", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,PublishedDate,AuthorId,NomenArticleLevelId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", article.AuthorId);
            ViewBag.NomenArticleLevelId = new SelectList(db.nomenArticleLevels, "NomenArticleLevelId", "Name", article.NomenArticleLevelId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", article.AuthorId);
            ViewBag.NomenArticleLevelId = new SelectList(db.nomenArticleLevels, "NomenArticleLevelId", "Name", article.NomenArticleLevelId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,PublishedDate,AuthorId,NomenArticleLevelId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", article.AuthorId);
            ViewBag.NomenArticleLevelId = new SelectList(db.nomenArticleLevels, "NomenArticleLevelId", "Name", article.NomenArticleLevelId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
