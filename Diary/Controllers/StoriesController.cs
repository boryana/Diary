using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diary.Models;
using System.IO;
using System.Web.UI.WebControls;

using System.IO;


namespace Diary.Controllers
{
    public class StoriesController : Controller
    {
        private StoryDBContext db = new StoryDBContext();
        private string imgUrl;
        private int rating;
        // GET: Stories
        public ActionResult Index(string searchString)
        {
            //return View(db.Stories.ToList());

            var history = from b in db.Stories select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                history = history.Where(s => s.storyTitle.Contains(searchString));
            }
            return View(history);
        }

       

    //    public ActionResult Upload()
    //{
    //    foreach (string file in Request.Files)
    //    {
    //        var hpf = this.Request.Files[file];
    //        if (hpf.ContentLength == 0)
    //        {
    //            continue;
    //        }

    //        string savedFileName = Path.Combine(
    //            AppDomain.CurrentDomain.BaseDirectory, "~Content/Images");
    //            savedFileName = Path.Combine(savedFileName, Path.GetFileName(hpf.FileName));

    //        hpf.SaveAs(savedFileName);
    //    }

    //    return View("Index", "Stories");
    //}
        //public ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(file.FileName);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/Content/Images"), pic);
        //        // file is uploaded
        //        file.SaveAs(path);

        //        // save the image path path to the database or you can send image 
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();
        //        }

        //    }
        //    return RedirectToAction("Index", "Stories");
        //}

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "storyId,storyTitle,dateStory,rateOfStory,textStory,imageStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.imageStory = "C:\\temp\\Test.jpg";// this.imgUrl;
                story.rateOfStory = 5;
                story.storyTitle = Request.Form["storyTitle"];
                story.textStory = Request.Form["textStory"];
                story.dateStory = new DateTime(12,12,1999);//Convert.ToDateTime(Request.Form["dateStory"]);
                // db.Stories.Add(story);
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("About");
            }

            return View(story);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
           if ( file!=null && file.ContentLength>0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"),fileName);
                this.imgUrl = path;
                file.SaveAs(path);
                db.SaveChanges();
            }
            return RedirectToAction("Create");
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "storyId,storyTitle,dateStory,rateOfStory,textStory,imageStory")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
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
