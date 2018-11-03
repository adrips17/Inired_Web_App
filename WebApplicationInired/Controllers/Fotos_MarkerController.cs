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
    public class Fotos_MarkerController : Controller
    {
        private IniredAdrianEntities db = new IniredAdrianEntities();

        // GET: Fotos_Marker
        public ActionResult Index()
        {
            var fotos_Marker = db.Fotos_Marker.Include(f => f.Marker);
            return View(fotos_Marker.ToList());
        }

        // GET: Fotos_Marker/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos_Marker fotos_Marker = db.Fotos_Marker.Find(id);
            if (fotos_Marker == null)
            {
                return HttpNotFound();
            }
            return View(fotos_Marker);
        }

        // GET: Fotos_Marker/Create
        public ActionResult Create()
        {
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre");
            return View();
        }

        // POST: Fotos_Marker/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "imagen,id_marker")] Fotos_Marker fotos_Marker)
        {
            if (ModelState.IsValid)
            {
                db.Fotos_Marker.Add(fotos_Marker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", fotos_Marker.id_marker);
            return View(fotos_Marker);
        }

        // GET: Fotos_Marker/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos_Marker fotos_Marker = db.Fotos_Marker.Find(id);
            if (fotos_Marker == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", fotos_Marker.id_marker);
            return View(fotos_Marker);
        }

        // POST: Fotos_Marker/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "imagen,id_marker")] Fotos_Marker fotos_Marker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotos_Marker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", fotos_Marker.id_marker);
            return View(fotos_Marker);
        }

        // GET: Fotos_Marker/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos_Marker fotos_Marker = db.Fotos_Marker.Find(id);
            if (fotos_Marker == null)
            {
                return HttpNotFound();
            }
            return View(fotos_Marker);
        }

        // POST: Fotos_Marker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Fotos_Marker fotos_Marker = db.Fotos_Marker.Find(id);
            db.Fotos_Marker.Remove(fotos_Marker);
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

        public string ImagenesMarkers(string pathRecibido, string nombreRecibido)
        {
            Fotos_Marker ft = new Fotos_Marker();
            var num = Int32.Parse(nombreRecibido);
            var ur = "C:/Users/Adrian/Pictures/";
            var pathCompleto = ur + "" + pathRecibido;
            ft.id_marker = num;
            ft.imagen = pathCompleto;
            db.Fotos_Marker.Add(ft);
            db.SaveChanges();

            Session["imagePath"] = pathCompleto;

            return "OK";
        }
    }
}
