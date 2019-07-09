using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using PorTrasDoMonitor.Models;

namespace PorTrasDoMonitor.Controllers
{
    [Authorize(Roles = "Utilizador,Admin,Jornalista")]
    public class UtilizadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// apresenta na view os dados de um utilizador
        /// </summary>
        /// <param name="id">identificador do utilizador</param>
        /// <returns></returns>
        [Authorize(Roles = "Utilizador,Admin,Jornalista")]
        // GET: Utilizadores/Details/5
        public ActionResult Details(int? id)
        {
            //como int? pode ser null
            //verificar se o id é null
            if (id == null)
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index","Noticias");
            }
            //pesquisar pelo utilizador 
            Utilizadores utilizadores = db.Utilizadores.Find(id);
            //se o utilizador nao existir
            if (utilizadores == null)
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
            //verificar se o utilizador é o utilizador logado
            if (utilizadores.Username.Equals(User.Identity.Name)) { 
                return View(utilizadores);
            }
            //se nao for o utilizador logado
            else
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
        }
        //********************************************************************
        /// <summary>
        /// Metodo para Listar todos os utilizadores existentes
        /// </summary>
        /// <returns></returns>
        // GET: Utilizadores
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult Index()
        {
            return View(db.Utilizadores.ToList());
        }
        //********************************************************************

         //****************************************************************************
        /// <summary>
        /// apresenta na view os dados do utilizador, para eventual edição
        /// </summary>
        /// <param name="id">identificador do utilizador</param>
        /// <returns></returns>
        [Authorize(Roles = "Utilizador,Admin,Jornalista")]
        // GET: Utilizadores/Edit/5
        public ActionResult Edit(int? id)
        {
            //como int? indica que o id pode ser null
            //verificar se o id é null
            if (id == null)
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
            //pesquisar pelo utilizador 
            Utilizadores utilizadores = db.Utilizadores.Find(id);
           
            //se o utilizador nao existir
            if (utilizadores == null)
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }

            Session["UtilizadorID"] = utilizadores.ID;
            Session["UtilizadorAvatar"] = utilizadores.Avatar;
            //verificar se o utilizador é o utilizador logado
            if (utilizadores.Username.Equals(User.Identity.Name))
            {
                return View(utilizadores);
            }
            //se nao for o utilizador logado
            else
            {
                //redirecionar para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
        }
        //****************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilizadores"></param>
        /// <returns></returns>
        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Jornalista,Utilizador")]
        public ActionResult Edit([Bind(Include = "Nome,DataNascimento,Descricao")] Utilizadores utilizadores,HttpPostedFileBase avatarUpload)
        {
            utilizadores.Username = User.Identity.Name;
            Session["NomeUser"] = utilizadores.Nome;
            string caminhoNoticia = "";
            string nomeAntigo = Session["UtilizadorAvatar"].ToString();
            utilizadores.Avatar = nomeAntigo;
            if (avatarUpload != null) {
                string nomeAvatar = "Avatar" + utilizadores.ID + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + Path.GetExtension(avatarUpload.FileName).ToLower();
                
                if (avatarUpload.ContentType.Contains("image"))
                {
                        utilizadores.Avatar = nomeAvatar;
                        caminhoNoticia = Path.Combine(Server.MapPath("~/imagens/Noticias/"), nomeAvatar);
                        avatarUpload.SaveAs(Path.Combine(Server.MapPath("~/imagens/Avatares/"), nomeAvatar));
                    if (nomeAntigo != "default.jpg") { 
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/imagens/Avatares/"), nomeAntigo));
                    }
                }
            }
            try
            {
                if (utilizadores.DataNascimento >= System.DateTime.Now)
                {
                    ModelState.AddModelError("", "Data de Nascimento incorrecta!");
                    return View(utilizadores);
                }
                utilizadores.ID = (int)Session["UtilizadorID"];
                if (ModelState.IsValid)
                {
                    db.Entry(utilizadores).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Manage");
                }
            }
            catch (Exception)
            {


                ModelState.AddModelError("", "Não foi possivel concratizar a operação.");
            }
           
            return View(utilizadores);
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
