﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioSKD
{
    public class Cuenta
    {
        #region atributos
        private String nombre_usuario;
        private String contrasena;
        private List<Rol> roles;
        #endregion

        #region propiedades
        public String Nombre_usuario
        {
            get { return nombre_usuario; }
            set { nombre_usuario = value; }
        }

        public String Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }
        #endregion

        #region constructores
         public Cuenta()
        {
            nombre_usuario = "";
            contrasena = "";
        }

         public Cuenta(String elNombreUsuario, String laContrasena)
        {
            nombre_usuario = elNombreUsuario;
            contrasena = laContrasena;
        }
         public Cuenta(String elNombreUsuario, String laContrasena,List<Rol> listaRoles)
         {
             nombre_usuario = elNombreUsuario;
             contrasena = laContrasena;
             roles = listaRoles;
         }

        #endregion
    }
}
