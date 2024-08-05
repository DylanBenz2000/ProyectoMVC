using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoMVC.Models.Tablas
{
    public class TablaUsuarioVistaModelo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? Edad { get; set; }
    }
}