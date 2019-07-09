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
using PorTrasDoMonitor.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace PorTrasDoMonitor.Controllers
{

    [Authorize(Roles = "Utilizador,Admin,Jornalista")]
    public class NoticiasController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //******************************************************************************
        /// <summary>
        /// Metodo Inicial para devolver uma lista de noticias dependendo da categoria
        /// se a categoria for null devolve todas as noticias
        /// se a categoria nao for null devolve todas as noticias pertencentes aquela categoria
        /// </summary>
        /// <param name="categoria">Representa a categoria filtrada</param>
        /// <param name="page">Representa o numero da pagina</param>
        /// <returns>Uma lista de Noticias</returns>
        /// ********************************************************************************
        [AllowAnonymous]//apesar de existir restriçoes de acesso um utilizador anonimo consegue vizualizar noticias
        // GET: Noticias
        public ActionResult Index(string categoria,int? page)
        {
            Session["categoria"] = categoria;
            string pesquisa = (string)Session["categoria"];
            var pageNumber = page ?? 1;
            var pageSize = 6;
            if (categoria == null)
            {
                return View(db.Noticias.OrderByDescending(x=>x.ID).ToPagedList(pageNumber,pageSize));
            }
            else
            {
                 return View(db.Noticias.Where(c => c.ListaCategorias.Any(cc => cc.TipoCategoria == pesquisa)).OrderByDescending(x => x.ID).ToPagedList(pageNumber, pageSize));
            }
        }
        //*****************************************************************************************************
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(int? page,string search=null)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;
            if (search == null)
            {
                return View(db.Noticias.OrderByDescending(x => x.ID).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View("Index",db.Noticias.OrderByDescending(x=>x.ID).Where(cc=>cc.Titulo.Contains(search)).ToPagedList(pageNumber,pageSize));
            }
        }
        //*****************************************************************************************************
        /// <summary>
        /// Metodo que para cabeçalho dinamico
        /// </summary>
        /// <returns>Uma partial view que depois e chamado no layout para um cabeçalho dinamico</returns>        
        [AllowAnonymous]//apesar de existir restriçoes de acesso um utilizador anonimo tem acesso ao cabeçalho de categorias
        public ActionResult Categorias()
        {
            return PartialView(db.Categorias.ToList());
        }
        //*************************************************************************
        /// <summary>
        /// Metodo que permite vizualizar os detalhes de uma noticia
        /// </summary>
        /// <param name="id">id da noticia</param>
        /// <returns>Os detalhes de uma noticia</returns>
        /// *************************************************************************

        [AllowAnonymous]//Apesar de existir restriçoes os utilizadores anonimos podem vizualizar noticias
        // GET: Noticias/Details/5
        public ActionResult Details(int? id)
        {
            //int? indica que o id pode ser null
            //se for null
            if (id == null)
            {
                //Redireciona para a Home page
                return RedirectToAction("Index", "Noticias");
            }
            //pesquisar pela noticia
            Noticias noticia = db.Noticias.Find(id);
            //se a nao existir a noticia
            if (noticia == null)
            {
                //Redireciona para a Home page
                return RedirectToAction("Index", "Noticias");
            }
            return View(noticia);
        }
        //***********************************************************

        [HttpGet]
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult Listagem()
        {
            return View(db.Noticias.ToList());
        }
        //*******************************************************************************************
        /// <summary>
        /// Metodo para apresentar na view campos para criar uma noticia
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Jornalista")]
        // GET: Noticias/Create
        public ActionResult Create()
        {
            //variavel auxiliar que representa todas as categorias
            var categorias = db.Categorias.ToList();
            //modelo view model para receber dados de uma noticia e um array de checkbox para inserir na noticia
            var model = new CreateNoticiaViewModel();
            //definir que o array de categorias tem dimensao de quantas categorias existe na base de dados
            model.IdsCategorias = new int[db.Categorias.Count()];
            //representa todas as categorias que existem na base de dados
            model.ListaCategorias = categorias;
            //apresenta a view para criar uma noticia
            return View(model);
        }



        // POST: Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">View model que contem todos os dados para criar uma noticia</param>
        /// <param name="capaUpload">Ficheiro para a capa da noticia</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Jornalista")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNoticiaViewModel model,HttpPostedFileBase capaUpload)
        {
            //Criar uma noticia
            var noticia = new Noticias();
            //Definir o id da Noticia
            noticia.ID = db.Noticias.Max(c => c.ID) + 1;
            //Definir a data da Noticia
            model.Data=DateTime.Now;
            //Definir campos auxiliares para o nome da Capa da noticia e para guardar no disco a Imagem da Noticia
            string nomeCapa = "Noticia" + noticia.ID + ".jpg";
            string caminhoNoticia = "";
            //verificação se foi inserido um ficheiro
            if (capaUpload != null)
            {
                //Verificar se o ficheiro é uma imagem
                if (capaUpload.ContentType.Contains("image"))
                {
                    //Definir o nome da Capa
                    model.Capa = nomeCapa;
                    //guardar na variavel auxiliar o caminho para onde guardar a imagem
                    caminhoNoticia = Path.Combine(Server.MapPath("~/imagens/Noticias/"), nomeCapa);
                }
          
            }
            //ficheiro nao inserido
            else
            {
                model.IdsCategorias = new int[db.Categorias.Count()];
                model.ListaCategorias = db.Categorias.ToList();
                //gerar mensagem de erro para indicar o utilizador que nao foi inserida nenhuma imagem
                ModelState.AddModelError("", "Não foi inserida uma imagem");
                //redirecionar o utilizador para a View
                return View(model);
            }
            //Preencher os dados do view model para a noticia
            noticia.Titulo = model.Titulo;
            noticia.Descricao = model.Descricao;
            noticia.Conteudo = model.Conteudo;
            noticia.Capa = model.Capa;
            noticia.Data = model.Data;
            //variavel que representa o utilizador logado
            var idUtilizador = db.Utilizadores.Where(d => d.Username == User.Identity.Name).FirstOrDefault();
            //inserir na noticia o autor do utilizador que está logado
            noticia.UtilizadorFK = idUtilizador.ID;
            //percorrer o array das checkbox's do view model
            if (model.IdsCategorias == null)
            {
                model.IdsCategorias = new int[db.Categorias.Count()];
                model.ListaCategorias = db.Categorias.ToList();
                //gerar mensagem de erro para indicar o utilizador que nao foi inserida nenhuma imagem
                ModelState.AddModelError("", "Categorias de Preenchiemento Obrigatório!");
                //redirecionar o utilizador para a View
                return View(model);
            }
            foreach (var idCategoria in model.IdsCategorias.ToList()){
                //variavel que representa o id da categoria
              var categoria=db.Categorias.Find(idCategoria);
                //Adicionar à noticia a categoria
                noticia.ListaCategorias.Add(categoria);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //adiciona uma noticia à base de dados
                    db.Noticias.Add(noticia);
                    //efecta um comit na base de dados
                    db.SaveChanges();
                    //guardar a imagem no servidor
                    capaUpload.SaveAs(caminhoNoticia);
                    //redireciona o utilizador para a pagina de inicio
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                //Falta por uma pagina de erro personalizada
                ModelState.AddModelError("", "Ocorreu um erro na criação da Noticia" + noticia.ID + ".");
                
                return RedirectToAction("Index");
            }
          

            return View(noticia);
        }

        //*************************************************************************************
        /// <summary>
        /// Metodo para representar todas as categorias em checkbox
        /// </summary>
        /// <returns>Uma partial view que contem todas as Categorias em checkbox's</returns>
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult ListaCategorias()
        {
            return PartialView(db.Categorias.ToList());
        }
        //**************************************************************************************

        /// <summary>
        /// apresentar na view os dados de uma noticia para eventual uma edição
        /// </summary>
        /// <param name="id">identificador da noticia</param>
        /// <returns>View</returns>
        [Authorize(Roles = "Admin,Jornalista")]
        // GET: Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            //como int? indica que id pode ser null
            //se o id for null
            if (id == null)
            {
                //Rediriciona para Home page
                return RedirectToAction("Index");
            }
            //encontrar uma noticia com o id
            Noticias noticia = db.Noticias.Find(id);
           //verificar se foi possivel encontrar a noticia com id
            if (noticia == null)
            {
                //Nao existe Noticia com o id respetivo
                //Redireciona para home page
                return RedirectToAction("Index");
            }
            //variavel auxiliar que representa todas as Categorias da base de dados
            var categorias = db.Categorias.ToList();
            //View model que permite receber dados de uma noticia e os valores da checkbox com os id's das categorias
            var model = new CreateNoticiaViewModel();
            //iterador auxiliar
            var i = 0;
            //Definir o tamanho do array das categorias
            model.IdsCategorias = new int[noticia.ListaCategorias.Count()];
            //preencher o array
            foreach (var categoriaID in noticia.ListaCategorias)
            {
                model.IdsCategorias[i] = categoriaID.ID;
                i++;
            }
            //variavel de sessao guardar o id da noticia a alterar
            Session["noticiaID"] = noticia.ID;
            //preencher os dados do view model
            model.ListaCategorias = categorias;
            model.Data = noticia.Data;
            model.Capa = noticia.Capa;
            model.Titulo = noticia.Titulo;
            model.Descricao = noticia.Descricao;
            model.Conteudo = noticia.Conteudo;
            //retornar a view
            return View(model);
        }
        //*************************************************************************************************

       
        /// <summary>
        /// concretiza a ediçao dos dados de uma noticia
        /// </summary>
        /// <param name="formulario">Formulario da pagina</param>
        /// <param name="capaUpload"> ficheiro</param>
        /// <returns></returns>
        // POST: Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult Edit(FormCollection formulario,HttpPostedFileBase capaUpload)
        {
           //como o metodo recebe um formulário
           //preencher os dados consoante os campos do formulário
           // pesquisar pela noticia consoante a variavel de sessao noticiaID => esta variavel é preenchida no get do Metodo Edit
            Noticias noticia = db.Noticias.Find((int)Session["noticiaID"]);
            //preencher os campos da noticia com os campos do formulário
            noticia.Titulo = formulario["Titulo"];
            noticia.Conteudo = formulario["Conteudo"];
            noticia.Descricao = formulario["Descricao"];
            //Preencher a data com o dia em que a noticia foi alterada
            noticia.Data = System.DateTime.Now;
            //preencher o autor com o utilizador que está logado
            var idUtilizador = db.Utilizadores.Where(d => d.Username == User.Identity.Name).FirstOrDefault();
            noticia.UtilizadorFK = idUtilizador.ID;
            //criar uma lista auxiliar
            ICollection <Categorias> listaCat = new List<Categorias> { };
            //variaveis auxiliares para tratamento da Capa
         
            string caminhoNoticia = "";
            string nomeAntigo = formulario["Capa"];
            //verificação se foi inserido algum ficheiro
            if (capaUpload != null)
            {
                string nomeCapa = "Noticia" + noticia.ID + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + Path.GetExtension(capaUpload.FileName).ToLower();
                //verificar se o ficheiro inserido é uma imagem
                if (capaUpload.ContentType.Contains("image"))
                {
                    noticia.Capa = nomeCapa;
                    caminhoNoticia = Path.Combine(Server.MapPath("~/imagens/Noticias/"), nomeCapa);
                    capaUpload.SaveAs(Path.Combine(Server.MapPath("~/imagens/Noticias/"), nomeCapa));

                    //Apagar no disco a imagem
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/imagens/Noticias/"), nomeAntigo));
                }
            }
            //ficheiro nao inserido
            else
            {
                //definir a capa com o valor do formulario
                noticia.Capa = formulario["Capa"];
            }
            //se alguma checkbox estiver preenchida
            if (formulario["idsCategorias"] != null)
            {
                //se no formulario existem checkbox's preenchidas,estes valores tem que ser separados
                var valoresCheckbox = formulario["idsCategorias"].Split(',');
                //percorrer o array de valores de checkbox
                foreach (var categoria in db.Categorias.ToList())
                {
                    //se o array do formulario tiver o id da categoria
                    if (valoresCheckbox.Contains(categoria.ID.ToString()))
                    {
                        //adicionar na lista auxiliar a categoria
                        listaCat.Add(categoria);
                        // se a categoria nao pertenca aquela noticia
                        if (!categoria.ListaNoticias.Contains(noticia))
                        {
                            //adicionar na categoria a noticia
                            categoria.ListaNoticias.Add(noticia);
                        }
                    }

                    //Verificar os valores que nao estao a true na checkbox
                    else
                    {
                        //Verificar se a categoria pertence aquela noticia
                        if (categoria.ListaNoticias.Contains(noticia))
                        {
                            //como nao esta a true remover na categoria a noticia
                            categoria.ListaNoticias.Remove(noticia);
                        }
                    }
                }
                //prencher a noticia com a lista auxiliar
                noticia.ListaCategorias = listaCat;
            }
            //caso nao foi preenchida nenhuma das checkbox's
            else
            {
                //mensagem de erro a indicar ao utilizador que as checkbox's sao de preenchimento obrigatorio
                ModelState.AddModelError("", "Categorias de Preenchimento Obrigatório!");
                //criar um view model
                CreateNoticiaViewModel model = new CreateNoticiaViewModel { };
                //preencher o view model com os dados da noticia
                model.Capa = noticia.Capa;
                model.Conteudo = noticia.Conteudo;
                model.Descricao = noticia.Descricao;
                model.Titulo = noticia.Titulo;
                model.Data = System.DateTime.Now;
                model.ListaCategorias = db.Categorias.ToList();
                model.IdsCategorias = new int[db.Categorias.Count()];
                //redirecionar para a view com a mensagem de erro
                return View(model);

            }
            try
            {
                if (ModelState.IsValid)
                {
                    //guardar os dados da Noticia
                    db.Entry(noticia).State = EntityState.Modified;
                    //efetuar o commit
                    db.SaveChanges();
                    //enviar os dados para a pagina inicial
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Não foi possivel concratizar a operação.");
            }
           
            return View(noticia);
        }
        //*****************************************************************************************
        /// <summary>
        /// Apresenta na view os dados de uma noticia para eventual, eliminação
        /// </summary>
        /// <param name="id">identificador da noticia</param>
        /// <returns></returns>
        // GET: Noticias/Delete/5
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult Delete(int? id)
        {
            //como int? significa que o id pode ser null
            //verificar se o id é null
            if (id == null)
            {
                //redireciona para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
            //pesquisar pela noticia
            Noticias noticia = db.Noticias.Find(id);
            //caso nao foi encontrada a noticia
            if (noticia == null)
            {
                //Redireciona para a pagina inicial
                return RedirectToAction("Index", "Noticias");
            }
            return View(noticia);
        }
        //**************************************************************************************************************************
        /// <summary>
        /// Metodo para eliminar a noticia da base de dados
        /// </summary>
        /// <param name="id">identificador da noticia</param>
        /// <returns></returns>
        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Jornalista")]
        public ActionResult DeleteNewMethod(int id)
        {
            //pesquisa pela noticia
            Noticias noticia = db.Noticias.Find(id);
            ICollection<Categorias> listaAuxiliar = new List<Categorias> { };
          
            noticia.ListaCategorias = listaAuxiliar;
            try
            {
                //remover categorias associada a noticia
                foreach (var categoria in noticia.ListaCategorias.ToList())
                {
                    categoria.ListaNoticias.Remove(noticia);
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //apagar comentarios associado a noticia
                foreach(var comentario in noticia.ListaComentarios.ToList())
                {
                    db.Comentarios.Remove(comentario);
                    db.Entry(comentario).State = EntityState.Deleted;
                    db.SaveChanges();
                }
               
                //Apagar no disco a imagem
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/imagens/Noticias/"), noticia.Capa));
                db.Entry(noticia).State = EntityState.Deleted;
                db.Noticias.Remove(noticia);
                db.SaveChanges();
                //redireciona para a pagina inicial
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possivel concratizar a operação.");
            }
            return View(noticia);
          
        }
        //***********************************************************************
        /// <summary>
        /// Metodo para apresentar uma view com dados relativos ao projeto
        /// </summary>
        /// <returns></returns>
       [HttpGet]
       [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        //***********************************************************************
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
