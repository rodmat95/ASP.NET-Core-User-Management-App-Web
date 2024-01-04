using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsuarioLogin_MVC.Models;

using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace UsuarioLogin_MVC.Servicios
{
    public static class EmailServicio
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 587;

        private static string _NombreEnvia = "RodMat95";
        private static string _Correo = "rodmat0905@gmail.com";
        private static string _Clave = "cygvxcofxmtviumr";

        public static bool Enviar(EmailDTO emaildto)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(emaildto.Para));
                email.Subject = emaildto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emaildto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_Correo, _Clave);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}