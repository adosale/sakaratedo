﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using DatosSKD.Modulo1;
using DominioSKD;
using ExcepcionesSKD;
using LogicaNegociosSKD.Modulo2;

namespace LogicaNegociosSKD.Modulo1
{
    public class logicaLogin
    {
        
        MailMessage Mail = new MailMessage();
        SmtpClient SMTP = new SmtpClient();
        /// <summary>
        /// Procedimiento para el envio de correo para el reestablecimiento de la contraseña
        /// </summary>
        /// <param name="CorreoOrigen">Correo emitente</param>
        /// <param name="Clave">contraseña del correo emitente</param>
        /// <param name="Destino">Correo destino</param>
        /// <param name="Mensaje">Mensaje a enviar</param>
        /// <returns>True si se envio, false si ocurrio un error</returns>
        public Boolean EnviarCorreo(String Destino)
        {
            try
            {
                String idUser = validarCorreo(Destino);
                if (idUser==null)
                    throw new Exception(RecursosLogicaModulo1.Mensaje_Error_CorreoNoRegistrado);
                
                DateTime tiempoActual = DateTime.Now;
                String DireccionHTTP = "http://localhost:" + RecursosLogicaModulo1.puertoSAKARATEDO +
                RecursosLogicaModulo1.direccionM1_RestablecerContraseña+
                RecursosLogicaModulo1.variableRestablecerHTTP+
                AlgoritmoDeEncriptacion.EncriptarCadenaDeCaracteres(idUser,RecursosLogicaModulo2.claveDES)+
                RecursosLogicaModulo1.variableFechaHTTP +
                AlgoritmoDeEncriptacion.EncriptarCadenaDeCaracteres
                (tiempoActual.Date.ToString(), RecursosLogicaModulo2.claveDES);

                
                String mensajeDireccion = "<br>" +  DireccionHTTP + "</br>";
                Mail.From = new MailAddress(RecursosLogicaModulo1.cuentaCorreoSAKARATEDO);
                Mail.To.Add(new MailAddress(Destino));
                Mail.Body = RecursosLogicaModulo1.mensajeSAKARATEDO+mensajeDireccion;
                Mail.BodyEncoding = System.Text.Encoding.UTF8;
                Mail.IsBodyHtml = true;//si queremos que se envie como codigo HTML
                Mail.Subject =RecursosLogicaModulo1.asuntoSAKARATEDO ;
                Mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //Envia una copia de correo a sakaratedo@
                Mail.Bcc.Add(RecursosLogicaModulo1.cuentaCorreoSAKARATEDO);
                //configuracion SMTP
                SMTP.Host = RecursosLogicaModulo1.hostSAKARATEDO;
                SMTP.Port = int.Parse(RecursosLogicaModulo1.puertoEnvioSAKARATEDO);
                SMTP.Credentials = new NetworkCredential(RecursosLogicaModulo1.cuentaCorreoSAKARATEDO,RecursosLogicaModulo1.cuentaClaveSAKARATEDO);
                SMTP.EnableSsl = true;
                SMTP.Send(Mail);
                return true;

            }
            catch (Exception e)
            {
                Console.Write("Erro encontrado aqui:");
                Console.Write(e.StackTrace);
                Console.Write(e.ToString());
                Console.Write(e.Data.ToString());
                Console.Write(e.Message);
                throw  e;
            }
        }

        public String validarCorreo(string Destino)
        {
            String respuesta;
            try
            {
               respuesta= DatosSKD.Modulo1.BDLogin.ValidarCorreoUsuario(Destino);
            }
            catch (Exception e)
            {
                throw e;
            }
            return respuesta;
        }

        /// <summary>
        /// Aqui se conectara a la base de datos para ver si el usuario y contraseña son correctos y estanen el sistema
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="contraseña">contraseña</param>
        /// <returns>retorna los valores significativos del usuario a utilizar en el sistema</returns>
        public string[] iniciarSesion(string usuario, string contraseña)
        {
            try
            {
                Cuenta user= BDLogin.ObtenerUsuario(usuario);
                string[] respuesta = new string[6];
               string hashClave =AlgoritmoDeEncriptacion.hash(contraseña);
               if (hashClave == user.Contrasena && usuario!="" && contraseña!="")//en la Bd debe estar guardado en hash CAMBIAR ESTO!!!
               {
                   respuesta[0] = user.Id_usuario.ToString();
                   respuesta[1] = user.Nombre_usuario;
                   respuesta[4] = user.Imagen;
                   respuesta[5] = user.NombreDePila;
                   string rolesConcat = "";
                   string split= RecursosLogicaModulo1.splitRoles;
                   int cantRoles=user.Roles.Count;
                   int contador=0;
                   DateTime fechaRol= new DateTime(1900,1,1);
                   foreach (Rol rol in user.Roles)
                   {
                       contador++;
                       //se intenta quitar la palabra Admin del rol si es que la tiene
                       string[] sinAdmin = rol.Nombre.Split(' ');
                       string elRol = sinAdmin[sinAdmin.Length - 1];
                       if(contador==cantRoles)
                           split="";
                      // if(elRol==RecursosLogicaModulo1.rolAtleta ) verificar si es menor de edad
                       rolesConcat = rolesConcat + elRol + split;
                       int d = DateTime.Compare(fechaRol, rol.Fecha_creacion);
                       if (DateTime.Compare(fechaRol, rol.Fecha_creacion) == -1)
                       {
                           fechaRol = rol.Fecha_creacion;
                           respuesta[3] = elRol;
                       }

                   }
                   respuesta[2] = rolesConcat;
                   if (rolesConcat != "")
                       return respuesta;
                   else
                       return null;
                   //ingresó a sistema
               }

                    return null;
               
            }
            catch (Exception e)
            {
                throw new ExcepcionesSKD.Modulo1.InicioSesionException(RecursosLogicaModulo1.Codigo_Error_InicioSesion,
                    RecursosLogicaModulo1.Mensaje_Error_InicioSesion, e);
            }
        }



      
    }
}