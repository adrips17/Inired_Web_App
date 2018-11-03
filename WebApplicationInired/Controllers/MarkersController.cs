using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationInired.Models;

namespace WebApplicationInired.Controllers
{
    public class MarkersController : Controller
    {
        private IniredAdrianEntities db = new IniredAdrianEntities();
        

        // GET: Markers
        public ActionResult Principal()
        {
            ViewBag.lugares = db.Marker.ToList();
            return View();
        }

        public ActionResult CreateMarker()
        {
            return View();
        }

        // GET: Markers/Details/5
        public ActionResult Details()
        {
           
            return View();
        }

        // GET: Markers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_marker,nombre,lat,lon,ubicacion,descripcion,nota,comentarios_marker,fotos_marker")] Marker marker)
        {
            if (ModelState.IsValid)
            {
                db.Marker.Add(marker);
                db.SaveChanges();
                return RedirectToAction("Principal");
            }

            return View(marker);
        }

        // GET: Markers/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marker marker = db.Marker.Find(id);
            if (marker == null)
            {
                return HttpNotFound();
            }
            return View(marker);
        }

        // POST: Markers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_marker,nombre,lat,lon,ubicacion,descripcion,nota,comentarios_marker,fotos_marker")] Marker marker)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(marker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Principal");
            }
            return View(marker);
        }

        // GET: Markers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marker marker = db.Marker.Find(id);
            if (marker == null)
            {
                return HttpNotFound();
            }
            return View(marker);
        }

        // POST: Markers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marker marker = db.Marker.Find(id);
            db.Marker.Remove(marker);
            db.SaveChanges();
            return RedirectToAction("Principal");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //METODO QUE RECOGE LOS MARKERS
        public JsonResult GetAllLocationById(int id)
        {
            var data = db.Marker.Where(x => x.id_marker == id).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllImages(int id)
        {
            var n = db.Marker.Where(x => x.id_marker == id).Select(s => s.nombre).SingleOrDefault();
            var im = db.Images.Where(x => x.nombre_marker == n).Select(s => s.imagen).ToList();
            ViewBag.imagenesMarker = im;
            return Json(im, JsonRequestBehavior.AllowGet);
        }

        public int CrearNuevoMarker(string nombreRecibido, string ubicacionRecibida,
            string notaRecibida, string latRecibida, string lonRecibida,
            string descripcionRecibida)
        {

            Marker MarkObject = new Marker();

            var not = Int32.Parse(notaRecibida);
            var lo = float.Parse(lonRecibida, CultureInfo.InvariantCulture);
            var la = float.Parse(latRecibida, CultureInfo.InvariantCulture);



            MarkObject.nombre = nombreRecibido;
            MarkObject.ubicacion = ubicacionRecibida;
            MarkObject.nota = not;
            MarkObject.descripcion = descripcionRecibida;
            MarkObject.lat = la;
            MarkObject.lon = lo;

            db.Marker.Add(MarkObject);
            db.SaveChanges();


            var id = db.Marker.Where(x => x.nombre == nombreRecibido).Select(s => s.id_marker).SingleOrDefault();
           
            return id;
        }
    }
}
