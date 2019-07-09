using PorTrasDoMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PorTrasDoMonitor.ViewModels
{
    public class CreateNoticiaViewModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        //Titulo de uma Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Titulo { get; set; }
        //Capa da Noticia
       
        public string Capa { get; set; }

        //Paragrafo a negrito da Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Descricao { get; set; }
        //Conteudo da Noticia
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Conteudo { get; set; }

        public int[] IdsCategorias { get; set; }

        public IEnumerable<Categorias> ListaCategorias { get; set; }
    }
}