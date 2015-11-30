﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominioSKD;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DatosSKD.Modulo1;

namespace DatosSKD.Modulo2
{
    public class BDRoles
    {
       public static List<Rol> ObtenerRolesDeSistema()
        {
            BDConexion laConexion;
            List<Parametro> parametros;
            Parametro elParametro = new Parametro();

            try
            {
                laConexion = new BDConexion();
                parametros = new List<Parametro>();
                List<Rol> losRoles=new List<Rol>();

                DataTable dt = laConexion.EjecutarStoredProcedureTuplas(
                               RecursosBDModulo2.ConsultarRolesSistema, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    Rol elRol = new Rol();
                    elRol.Id_rol = int.Parse(row[RecursosBDModulo2.AliasIdRol].ToString());
                    elRol.Nombre = row[RecursosBDModulo2.AliasNombreRol].ToString();
                    elRol.Descripcion = row[RecursosBDModulo2.AliasDescripcionRol].ToString();
                    losRoles.Add(elRol);
                }
               
                return losRoles;

            }
            catch (SqlException e)
            {
                throw new ExcepcionesSKD.ExceptionSKDConexionBD(RecursoGeneralBD.Codigo,
                    RecursoGeneralBD.Mensaje, e);
            }
            catch (FormatException e)
            {
                throw new ExcepcionesSKD.Modulo12.FormatoIncorrectoException(RecursosBDModulo1.Codigo_Error_Formato,
                     RecursosBDModulo1.Mensaje_Error_Formato, e);
            }
            catch (ExcepcionesSKD.ExceptionSKDConexionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcepcionesSKD.ExceptionSKD(RecursoGeneralBD.Mensaje_Generico_Error, e);
            }
        }

       public static bool EliminarRol(string idUsuario, string idRol)
       {
           BDConexion laConexion;
           List<Parametro> parametros;
           Parametro elParametro = new Parametro();

           try
           {
               laConexion = new BDConexion();
               parametros = new List<Parametro>();
               elParametro = new Parametro(RecursosBDModulo2.AliasIdRol, SqlDbType.VarChar, idRol, false);
               parametros.Add(elParametro); 
               elParametro = new Parametro(RecursosBDModulo2.aliasIdUsuario, SqlDbType.VarChar, idUsuario, false);
               parametros.Add(elParametro);
               laConexion.EjecutarStoredProcedureTuplas( RecursosBDModulo2.EliminarRolProcedure, parametros);
               return true;

           }
           catch (SqlException e)
           {
               throw new ExcepcionesSKD.ExceptionSKDConexionBD(RecursoGeneralBD.Codigo,
                   RecursoGeneralBD.Mensaje, e);
           }
           catch (ExcepcionesSKD.ExceptionSKDConexionBD e)
           {
               throw e;
           }
           catch (Exception e)
           {
               throw new ExcepcionesSKD.ExceptionSKD(RecursoGeneralBD.Mensaje_Generico_Error, e);
           }
       }
      
        public static bool AgregarRol(string idUsuario, string idRol)
       {
           BDConexion laConexion;
           List<Parametro> parametros;
           Parametro elParametro = new Parametro();

           try
           {
               laConexion = new BDConexion();
               parametros = new List<Parametro>();
               elParametro = new Parametro(RecursosBDModulo2.AliasIdRol, SqlDbType.VarChar, idRol, false);
               parametros.Add(elParametro);
               elParametro = new Parametro(RecursosBDModulo2.aliasIdUsuario, SqlDbType.VarChar, idUsuario, false);
               parametros.Add(elParametro);
               laConexion.EjecutarStoredProcedureTuplas(RecursosBDModulo2.AgregarRolProcedure, parametros);
               return true;

           }
           catch (SqlException e)
           {
               throw new ExcepcionesSKD.ExceptionSKDConexionBD(RecursoGeneralBD.Codigo,
                   RecursoGeneralBD.Mensaje, e);
           }
           catch (ExcepcionesSKD.ExceptionSKDConexionBD e)
           {
               throw e;
           }
           catch (Exception e)
           {
               throw new ExcepcionesSKD.ExceptionSKD(RecursoGeneralBD.Mensaje_Generico_Error, e);
           }

       }

        public static List<Rol> consultarRolesUsuario(string idUsuario)
        {
            BDConexion laConexion;
            List<Parametro> parametros;
            Parametro elParametro = new Parametro();

            try
            {
                laConexion = new BDConexion();
                parametros = new List<Parametro>();
                List<Rol> losRoles = new List<Rol>();
                elParametro = new Parametro(RecursosBDModulo2.aliasIdUsuario, SqlDbType.VarChar, idUsuario, false);
                parametros.Add(elParametro);
                DataTable dt= laConexion.EjecutarStoredProcedureTuplas(RecursosBDModulo2.ConsultarRolesUsuario, parametros);
                foreach (DataRow row in dt.Rows)
                {
                    Rol rol = new Rol();
                    rol.Id_rol = int.Parse(row[RecursosBDModulo2.AliasIdRol].ToString());
                    rol.Descripcion = row[RecursosBDModulo2.AliasDescripcionRol].ToString();
                    rol.Nombre = row[RecursosBDModulo2.AliasNombreRol].ToString();
                    rol.Fecha_creacion = (DateTime)row[RecursosBDModulo2.aliasFechaCreacion];
                    losRoles.Add(rol);

                }

                
                return losRoles;

            }
            catch (SqlException e)
            {
                throw new ExcepcionesSKD.ExceptionSKDConexionBD(RecursoGeneralBD.Codigo,
                    RecursoGeneralBD.Mensaje, e);
            }
            catch (FormatException e)
            {
                throw new ExcepcionesSKD.Modulo12.FormatoIncorrectoException(RecursosBDModulo1.Codigo_Error_Formato,
                     RecursosBDModulo1.Mensaje_Error_Formato, e);
            }
            catch (ExcepcionesSKD.ExceptionSKDConexionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcepcionesSKD.ExceptionSKD(RecursoGeneralBD.Mensaje_Generico_Error, e);
            }

        }

        /// <summary>
        /// Retorna un usuario por su ID sin su contraseña
        /// </summary>
        /// <param name="id_usuario">ID del usuario a consultar</param> 
        /// <returns>Cuenta de usuario sin contraseña</returns>
        public static Cuenta ObtenerUsuario(int id_usuario)
        {
            BDConexion laConexion;//COnsultar la persona
            BDConexion laConexion2;//Consultar los roles de la persona
            List<Parametro> parametros;
            Parametro elParametro = new Parametro();

            try
            {
                laConexion = new BDConexion();
                laConexion2 = new BDConexion();
                parametros = new List<Parametro>();
                Cuenta laCuenta = new Cuenta();

                elParametro = new Parametro(RecursosBDModulo1.AliasIdUsuario, SqlDbType.Int, id_usuario.ToString(), false);
                parametros.Add(elParametro);
                DataTable dt = laConexion.EjecutarStoredProcedureTuplas(
                               RecursosBDModulo2.ConsultarNombreUsuarioContrasena_ID, parametros);


                foreach (DataRow row in dt.Rows)
                {

                    laCuenta.Id_usuario = int.Parse(row[RecursosBDModulo1.AliasIdUsuario].ToString());
                    laCuenta.Nombre_usuario = row[RecursosBDModulo1.AliasNombreUsuario].ToString();
                    laCuenta.Imagen = row[RecursosBDModulo1.AliasImagen].ToString();
                    laCuenta.NombreDePila = row[RecursosBDModulo1.AliasNombreDePila].ToString();

                }

                DataTable dt1 = laConexion2.EjecutarStoredProcedureTuplas(
                RecursosBDModulo2.ConsultarRolesUsuario, parametros);
                List<Rol> listaRol = new List<Rol>();
                foreach (DataRow row in dt1.Rows)
                {

                    Rol elRol = new Rol();
                    elRol.Id_rol = int.Parse(row[RecursosBDModulo1.AliasIdRol].ToString());
                    elRol.Nombre = row[RecursosBDModulo1.AliasNombreRol].ToString();
                    elRol.Descripcion = row[RecursosBDModulo1.AliasDescripcionRol].ToString();
                    elRol.Fecha_creacion = (DateTime)row[RecursosBDModulo1.AliasFechaCreacion];
                    listaRol.Add(elRol);
                }

                laCuenta.Roles = listaRol;
                return laCuenta;

            }
            catch (SqlException e)
            {
                throw new ExcepcionesSKD.ExceptionSKDConexionBD(RecursoGeneralBD.Codigo,
                    RecursoGeneralBD.Mensaje, e);
            }
            catch (FormatException e)
            {
                throw new ExcepcionesSKD.Modulo12.FormatoIncorrectoException(RecursosBDModulo1.Codigo_Error_Formato,
                     RecursosBDModulo1.Mensaje_Error_Formato, e);
            }
            catch (ExcepcionesSKD.ExceptionSKDConexionBD e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcepcionesSKD.ExceptionSKD(RecursoGeneralBD.Mensaje_Generico_Error, e);
            }

        }
    
    
    }
}
