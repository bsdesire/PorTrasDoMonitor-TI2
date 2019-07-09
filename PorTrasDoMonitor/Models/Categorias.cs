using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PorTrasDoMonitor.Models
{
    public class Categorias
    {
        //Construtor da class
        public Categorias()
        {
            ListaNoticias = new HashSet<Noticias>();
        }
        //ID
        [Key]
        public int ID { get; set; }

        //Atributos da class Categorias
        //Nome de uma categoria
        [RegularExpression("[A-ZÍÉÂÁ]*[a-záéíóúàèìòùâêîôûäëïöüãõç -]*", ErrorMessage = "A {0} só pode conter letras.")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string TipoCategoria { get; set; }
        
        //complementar a informaçao do relacionamento muitos para muitos entre categorias e noticias
        public virtual ICollection<Noticias> ListaNoticias { get; set; }

    }
}