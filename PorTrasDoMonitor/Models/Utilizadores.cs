using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PorTrasDoMonitor.Models
{
    public class Utilizadores
    {
        //construtor da class
        public Utilizadores()
        {
            this.ListaComentarios = new HashSet<Comentarios>();
            this.ListaNoticias = new HashSet<Noticias>();
        }

        //atributos da class
        //ID
        [Key]
        public int ID { get; set; }
        //Nome do utilizador
        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório!")]
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
           ErrorMessage = "O {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula, seguida de minúsculas...")]
        public string Nome { get; set; }



        //Data nascimento
        //data no formato ano/mes/dia
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name ="Data de Nascimento")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public DateTime DataNascimento { get; set; }



        //Descricao do utilizador

        [RegularExpression("^[a-zA-Z0-9_.,áãàâÃÀÁÂÔÒÓÕòóôõÉÈÊéèêíìîÌÍÎúùûçÇ!-.? ]*", ErrorMessage = "O {0} tem caracteres inválidos!")]
        public string Descricao { get; set; }


        public string Avatar { get; set; }
        //atributo auxiliar para relacionar a tabela dos utilizadores Asp.Net com a tabela utilizadores
        public string Username { get; set; }

        //Complementaçao dos relacionamentos de um utilizador com multiplos comentarios e escrever multiplas noticias
        public virtual ICollection<Comentarios> ListaComentarios { get; set; }
        public virtual ICollection<Noticias> ListaNoticias { get; set; }
    }
}