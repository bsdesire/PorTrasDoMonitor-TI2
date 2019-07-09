using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using PorTrasDoMonitor.Models;

namespace PorTrasDoMonitor.Controllers
{

    public class ComentariosController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        
  
        /// <summary>
        /// Metodo que permite escrever um comentario e enviar resposta para o servidor
        /// </summary>
        /// <param name="comentario">Comentario</param>
        /// <returns></returns>
        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilizador,Admin,Jornalista")]
        public ActionResult Create([Bind(Include = "Descricao,NoticiasFK")] Comentarios comentario)
        {
            comentario.ID = db.Comentarios.Max(c=>c.ID) + 1;
            //pesquisar o id do autor do comentario
           var utilizador = db.Utilizadores.Where(u => u.Username == User.Identity.Name).FirstOrDefault().ID; ;
            //definir o autor do comentario
            comentario.UtilizadorFK = utilizador;
            comentario.Data = System.DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    //adiciona um comentario a base de dados
                    db.Comentarios.Add(comentario);
                    // efectua comit na base de dados
                    db.SaveChanges();
                    //redireciona para os detalhes da noticia
                    return RedirectToAction("Details", "Noticias", new { id = comentario.NoticiasFK });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possivel concratizar a operação.");
            }
          
         
            return View(comentario);
        }

   
        /// <summary>
        /// MEtodo que permite apagar comentario, envia resposta ao servidor
        /// </summary>
        /// <param name="f">Formulario</param>
        /// <returns></returns>
        // POST: Comentarios/Delete/5
        [Authorize(Roles = "Admin,Jornalista,Utilizador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection f)
        {
            //pesquisar o comentario pelo campo comentario.ID do formulário
            Comentarios comentarios = db.Comentarios.Find(Int32.Parse(f["comentario.ID"]));
            //remover da base de dados
            db.Comentarios.Remove(comentarios);
            //efectuar comit na base de dados
            db.SaveChanges();
            //redirecionar para os detalhes da noticia
            return RedirectToAction("Details","Noticias",new { id = comentarios.NoticiasFK });
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
