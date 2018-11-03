using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationInired.Models;

namespace WebApplicationInired.Controllers
{
    public class ComentsController : Controller
    {
        private IniredAdrianEntities db = new IniredAdrianEntities();

        // GET: Coments
        public ActionResult Index()
        {
            return View(db.Coments.ToList());
        }

        // GET: Coments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coments coments = db.Coments.Find(id);
            if (coments == null)
            {
                return HttpNotFound();
            }
            return View(coments);
        }

        // GET: Coments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comentario,id_user,nombreMarker")] Coments coments)
        {
            if (ModelState.IsValid)
            {
                db.Coments.Add(coments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coments);
        }

        // GET: Coments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coments coments = db.Coments.Find(id);
            if (coments == null)
            {
                return HttpNotFound();
            }
            return View(coments);
        }

        // POST: Coments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comentario,id_user,nombreMarker")] Coments coments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coments);
        }

        // GET: Coments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coments coments = db.Coments.Find(id);
            if (coments == null)
            {
                return HttpNotFound();
            }
            return View(coments);
        }

        // POST: Coments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Coments coments = db.Coments.Find(id);
            db.Coments.Remove(coments);
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

        public string EnviarComentario(string usuarioRecibido, string comentarioRecibido, string nombreMarkerRecibido)
        {
            Coments cm = new Coments();

            cm.comentario = comentarioRecibido;
            cm.id_user = usuarioRecibido;
            cm.nombreMarker = nombreMarkerRecibido;

            db.Coments.Add(cm);
            db.SaveChanges();

           // var id_mark = db.Marker.Where(x => x.nombre == nombreMarkerRecibido).Select(s => s.id_marker).SingleOrDefault();
            var comentariosUnSoloMarker = db.Coments.Where(x => x.nombreMarker == nombreMarkerRecibido).Select(s => s.comentario).ToList();
            ViewBag.comentariosPorMarker = comentariosUnSoloMarker;

            return "OK";
        }

        public JsonResult ComentariosPorMarker (string nombreRecibido)
        {
            var comentariosUnSoloMarker = db.Coments.Where(x => x.nombreMarker == nombreRecibido).Select(s => s.comentario).ToList();
            ViewBag.comentariosPorMarker = comentariosUnSoloMarker;
            Session["m"] = comentariosUnSoloMarker;

            return Json(comentariosUnSoloMarker, JsonRequestBehavior.AllowGet);
        }
    }
}
