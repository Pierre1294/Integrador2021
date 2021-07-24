using AppPFashions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AppPFashions.ViewModels
{
    public class AuditoriasViewModel 
    {
        public ObservableCollection<Auditorias> ListaAuditorias { get; set; }

        public AuditoriasViewModel()
        {
            ListaAuditorias = new ObservableCollection<Auditorias>();
            LoadAuditorias();

        }

        private void LoadAuditorias()
        {
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "corte.png",
                AuditoriaName = "Corte",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "costurap.png",
                AuditoriaName = "Costura Proceso",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "costuraf.png",
                AuditoriaName = "Costura Final",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "servicios.png",
                AuditoriaName = "Servicios",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "bordado.png",
                AuditoriaName = "Bordado",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "estampado.png",
                AuditoriaName = "Estampado",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "transfer.png",
                AuditoriaName = "Transfer",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "acabadoi.png",
                AuditoriaName = "Ingreso Acabado",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "acabadop.png",
                AuditoriaName = "Acabado Proceso",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "acabadov.png",
                AuditoriaName = "Acabado Vaporizado",

            });
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "acabadof.png",
                AuditoriaName = "Acabado Final",

            });
        }  

    }
}
