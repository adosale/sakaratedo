﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DominioSKD;

namespace DatosSKD.Modulo1
{
    class BDLogin
    {
        public static Cuenta ObtenerUsuario(string nombre_usuario)
        {
            BDConexion laConexion;
            List<Parametro> parametros;
            Parametro elParametro = new Parametro();

            try
            {
                laConexion = new BDConexion();
                parametros = new List<Parametro>();
                Cuenta laCuenta = new Cuenta();
                Rol losRoles = new Rol();

                elParametro = new Parametro(RecursosBDModulo1.AliasNombreUsuario,SqlDbType.VarChar,nombre_usuario,false);


                parametros.Add(elParametro);

                DataTable dt = laConexion.EjecutarStoredProcedureTuplas(
                               RecursosBDModulo1.ConsultarNombreUsuarioContrasena, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    laCuenta.Id_usuario = int.Parse(row[RecursosBDModulo1.AliasIdUsuario].ToString());
                    laCuenta.Nombre_usuario = row[RecursosBDModulo1.AliasNombreUsuario].ToString();
                    laCuenta.Contrasena = row[RecursosBDModulo1.AliasContrasena].ToString();          
                }

               DataTable dt1 = laConexion.EjecutarStoredProcedureTuplas(
               RecursosBDModulo1.ConsultarRolesUsuario, parametros);

               foreach (DataRow row in dt1.Rows)
               {
                    losRoles.Id_rol =int.Parse(row[RecursosBDModulo1.AliasIdRol].ToString());
                    losRoles.Nombre = row[RecursosBDModulo1.AliasNombreRol].ToString();
                    laCuenta.Roles.Add(losRoles);
               }
                return laCuenta;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
