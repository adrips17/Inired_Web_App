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
    public class ComentariosController : Controller
    {
        private IniredAdrianEntities db = new IniredAdrianEntities();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Marker).Include(c => c.Usuario);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre");
            ViewBag.id_user = new SelectList(db.Usuario, "usuario1", "password");
            return View();
        }

        // POST: Comentarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comentario,id_user,id_marker")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", comentarios.id_marker);
            ViewBag.id_user = new SelectList(db.Usuario, "usuario1", "password", comentarios.id_user);
            return View(comentarios);
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", comentarios.id_marker);
            ViewBag.id_user = new SelectList(db.Usuario, "usuario1", "password", comentarios.id_user);
            return View(comentarios);
        }

        // POST: Comentarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comentario,id_user,id_marker")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_marker = new SelectList(db.Marker, "id_marker", "nombre", comentarios.id_marker);
            ViewBag.id_user = new SelectList(db.Usuario, "usuario1", "password", comentarios.id_user);
            return View(comentarios);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentarios);
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

        public void EnviarComentario(string usRecibido, int markRecibido, string comentRecibido)
        {
            Comentarios c = new Comentarios();
            Marker m = new Marker();

            c.comentario = comentRecibido;
            c.id_marker = markRecibido;
            c.id_user = usRecibido;

            db.Comentarios.Add(c);
            db.SaveChanges();

            //var nMarker = db.Marker.Where(x => x.id_marker == markRecibido).Select(s => s.id_marker);
            //if(markRecibido.ToString() == nMarker.ToString())
            //{

            //}

        }
    }
}
