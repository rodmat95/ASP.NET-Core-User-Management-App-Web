using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsuarioLogin_MVC.Models
{
    public class UsuarioDTO
    {
        public string Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }
        public bool Confirmado { get; set; }
        public string Token { get; set; }
    }
}