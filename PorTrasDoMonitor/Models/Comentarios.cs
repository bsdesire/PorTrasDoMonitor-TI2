using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PorTrasDoMonitor.Models
{
    public class Comentarios
    {

        //construtor da class comentarios
        public Comentarios()
        {

        }

        //ID
        [Key]
        public int ID { get; set; }

        //Atributos da class comentarios
        [RegularExpression("^[a-zA-Z0-9_.,áãàâÃÀÁÂÔÒÓÕòóôõÉÈÊéèêíìîÌÍÎúùûçÇ!-.? ]*", ErrorMessage = "O {0} tem caracteres inválidos!")]
        public string Descricao { get; set; }

        //chaves forasteiras Utilizadores e Noticias
        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }

  
        public DateTime Data { get; set; }

        //foreign key Noticias
        [ForeignKey("Noticia")]
        public int NoticiasFK { get; set; }
        public virtual Noticias Noticia { get; set; }
    }
}