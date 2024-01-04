using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsuarioLogin_MVC.Models
{
    public class EmailDTO
    {
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
    }
}