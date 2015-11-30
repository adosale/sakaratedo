﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DominioSKD;
using LogicaNegociosSKD.Modulo7;
using templateApp.GUI.Master;

namespace templateApp.GUI.Modulo7
{
    public partial class M7_ListarHorariodePractica : System.Web.UI.Page
    {
        #region Atributos
        private List<Evento> laLista = new List<Evento>();
        private Horario horario;
        #endregion
        #region Page Load
        /// <summary>
        /// Método que se ejecuta al cargar la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ((SKD)Page.Master).IdModulo = "7";
            String detalleString = Request.QueryString["eventDetalle"];

            if (detalleString != null)
            {
                llenarModalInfo(int.Parse(detalleString));
            }

            #region Llenar Data Table con Eventos
            LogicaHorarioPractica logEvento = new LogicaHorarioPractica();

            if (!IsPostBack)
            {
                try
                {
                    laLista = logEvento.obtenerListaDePractica(int.Parse(Session[RecursosInterfazMaster.sessionUsuarioID].ToString()));

                    foreach (Evento evento in laLista)
                    {
                        this.laTabla.Text += M7_Recursos.AbrirTR;
                        this.laTabla.Text += M7_Recursos.AbrirTD + evento.Id_evento.ToString() + M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.AbrirTD + evento.Nombre.ToString() + M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.AbrirTD + evento.Horario.HoraInicio.ToString() + M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.AbrirTD + evento.Horario.HoraFin.ToString() + M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.AbrirTD + evento.Ubicacion.Ciudad.ToString() + M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.AbrirTD;
                        this.laTabla.Text += M7_Recursos.BotonInfoHorariodePractica + evento.Id_evento + M7_Recursos.BotonCerrar;
                        this.laTabla.Text += M7_Recursos.CerrarTD;
                        this.laTabla.Text += M7_Recursos.CerrarTR;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
            /// <summary>
            /// Método que llena el modal con la información del evento
            /// </summary>
            /// <param name="idEvento">Número entero que representa el ID del evento</param>
            protected void llenarModalInfo(int idEvento)
            {
                /*Evento evento = new Evento();
                LogicaHorarioPractica logica = new LogicaHorarioPractica();
                evento = logica.detalleEventoID(idEvento);*/
            }

        }
           
            #endregion

        #endregion
}