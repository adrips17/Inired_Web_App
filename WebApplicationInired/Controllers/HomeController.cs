using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationInired.Models;
using System.Net;
using System.Net.Mail;

namespace WebApplicationInired.Controllers
{
    public class HomeController : Controller
    {
        IniredAdrianEntities db = new IniredAdrianEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string RegistrarUsuarios(string nombreRecibido, string usuarioRecibido, string correoRecibido,
           string contraseñaRecibida, string apellidosRecibidos, DateTime fecha_nacimientoRecibida,
           string sexoRecibido)
        {

            Usuario UsuObject = new Usuario();

            //List<string> ListCorreos = db.Usuario.Where(w => w.correo.Length > 1).Select(s => s.correo).ToList();
            //Session["correos"] = ListCorreos;


            UsuObject.nombre = nombreRecibido;
            UsuObject.usuario1 = usuarioRecibido;
            UsuObject.correo = correoRecibido;
            UsuObject.password = contraseñaRecibida;
            UsuObject.fecha_nacimiento = fecha_nacimientoRecibida;
            UsuObject.apellidos = apellidosRecibidos;
            UsuObject.sexo = sexoRecibido;
            //UsuObject.foto_usuario = rutaRecibida;


            db.Usuario.Add(UsuObject);
            db.SaveChanges();




            //Login[] ArrLogin = db.Login.ToArray();

            return "OK";
        }

        public string EnviarCorreoConContraseña(string correoRecuperacionRecibido)
        {
            Usuario UsuObject = new Usuario();

            var contraseñaDeCorreo = db.Usuario.Where(x => x.correo == correoRecuperacionRecibido).Select(s => s.password).SingleOrDefault();

            try
            {

                //SmtpClient mySmtpClient = new SmtpClient("avsasmtp.aguasdevalencia.es");
                SmtpClient mySmtpClient = new SmtpClient("smtp.1and1.com");

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = true;
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("apoveda@inired.com", "Adrianpoveda_17");
                //System.Net.NetworkCredential basicAuthenticationInfo = new NetworkCredential();
                mySmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress from = new MailAddress("apoveda@inired.com", "Correo recuperación contraseña");
                MailAddress to = new MailAddress("" + correoRecuperacionRecibido + "", "recibido");
                //MailAddress from = new MailAddress("nfrigola@inired.com", "TestFromName");
                //MailAddress to = new MailAddress("nfrigola@inired.com", "TestToName");

                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                //MailAddress replyto = new MailAddress("nfrigola@inired.com");
                //myMail.ReplyToList.Add(replyto);

                // set subject and encoding
                myMail.Subject = "Recuperación de contraseña";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "Tu contraseña es: " + contraseñaDeCorreo;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "OK";
        }

        public string IniciarSesion(string usuarioRecibido, string contraseñaRecibida)
        {

            Usuario userObject = new Usuario();
            //Lista con todos los usuarios
            List<string> usuariosRegistrados = db.Usuario.Where(s => s.usuario1.Length > 1).Select(x => x.usuario1).ToList();
            Session["list_usu"] = usuariosRegistrados.ToList();
            Session["user"] = usuarioRecibido;

            
            if (usuariosRegistrados.Contains(usuarioRecibido))
            {
                //Recogemos contraseña del usuario
                var contra = db.Usuario.Where(x => x.usuario1 == usuarioRecibido).Select(s => s.password).SingleOrDefault();
                Session["contra"] = contra;
                var nombreUsu = db.Usuario.Where(x => x.usuario1 == usuarioRecibido).Select(s => s.nombre).SingleOrDefault();
                Session["nombre"] = nombreUsu;
                var apellidosUsu = db.Usuario.Where(x => x.usuario1 == usuarioRecibido).Select(s => s.apellidos).SingleOrDefault();
                Session["apellidos"] = apellidosUsu;
                var usuarioUsu = db.Usuario.Where(x => x.usuario1 == usuarioRecibido).Select(s => s.usuario1).SingleOrDefault();
                Session["usuarioUsu"] = usuarioUsu;


                //Si coinciden las contraseñas
                if (contra == contraseñaRecibida)
                {
                    //Inicia Sesión
                    
                    return "OK";
                }
                else
                {
                    return "ERROR";

                }
            }
            return "ERROR";


        }

    }
}