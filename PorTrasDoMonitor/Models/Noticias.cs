using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PorTrasDoMonitor.Models
{
    public class Noticias
    {
        public Noticias()
        {
            this.ListaComentarios = new HashSet<Comentarios>();
          
            this.ListaCategorias = new HashSet<Categorias>();
        }

        //Primary Key
        [Key]
        public int ID { get; set; }






        //Atributos da class Noticias
        //Data em fortmato yyyyMMdd
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        //Titulo de uma Noticia
        [RegularExpression("^[a-zA-Z0-9_.,áãàâÃÀÁÂÔÒÓÕòóôõÉÈÊéèêíìîÌÍÎúùûçÇ!-.? ]*",ErrorMessage ="O {0} tem caracteres inválidos!")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Titulo { get; set; }

        //Capa da Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Capa { get; set; }



        //foreign key Utilizadores
        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }

        //Paragrafo a negrito da Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Descricao { get; set; }

        //Conteudo da Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Conteudo { get; set; }





        //complementar a informação do relacionamento de uma noticia com multiplos comentarios
        public virtual ICollection<Comentarios> ListaComentarios { get; set; }
        //complementar a informação do relacionamento de uma noticia com autores
    
        //complementar a informação do relacionamento de uma noticia várias Categorias
        public virtual ICollection<Categorias> ListaCategorias { get; set; }
    }
}