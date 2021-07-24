using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.SfKanban.XForms;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using System;
using AppPFashions.Models;
using System.Linq;

namespace AppPFashions.ViewModels
{
    //public class ViewModel
    //{
    //    public List<DashboardEficiencia> Data { get; set; }

    //    public ViewModel()
    //    {
    //        Data = new List<DashboardEficiencia>()
    //        {
    //            new DashboardEficiencia { fprdia = "David", pefici = 180 },
    //            new DashboardEficiencia { fprdia = "Michael",  pefici= 170 },
    //            new DashboardEficiencia { fprdia = "Steve", pefici = 160 },
    //            new DashboardEficiencia { fprdia = "Joel", pefici = 182 }
    //        };
    //    }
    //}

    [Preserve(AllMembers = true)]
    public class SfKanbanViewModel
    {
        List<taudit00> auditcp;
        public ObservableCollection<KanbanModel> Cards { get; set; }

        public List<object> IList;

        public SfKanbanViewModel()
        {


            Cards = new ObservableCollection<KanbanModel>();

            IList = new List<object>() { "Open", "Test", "Close", "InProgress" };


                //using (var data = new DataAccess())
                //{
                Cards = new ObservableCollection<KanbanModel>();

           
                    auditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "19" && x.status == "D" && x.sreaud == "N").OrderBy(x => x.faudit).ToList();
                    TimeSpan ndiascp;
                    TimeSpan ndiascf;
                    foreach (var recordcp in auditcp)
                    {
                        ndiascp = DateTime.Now - recordcp.faudit;
                        //ldtraba = App.baseDatos.GetList<mtraba00>(false).Where(x => x.ctraba == recordcp.ctraba).FirstOrDefault();
                        Cards.Add(new KanbanModel()
                        {
                            ID = 1,
                            Title = recordcp.nordpr,
                            Description = recordcp.dopera,
                            //Nordpr = recordcp.nordpr,
                            //Clinea = recordcp.clinea,
                            //Dclien = recordcp.dclien,
                            //Dopera = recordcp.dopera,
                            //Ndiast = ndiascp.Days + " día(s)",                        
                            ImageURL = "circulo_navy.png",
                            Category = "cproceso",
                            ColorKey = "Navy",
                            //Rating = recordcp.ndefec,
                            //Dtraba = ldtraba.ctraba +" - "+ldtraba.dtraba,
                            //Careas = recordcp.careas,
                            //Faudit = recordcp.faudit,
                            //Nsecue = recordcp.nsecue,
                        });
                    }


                
     

        }

        public void Init()
        {
        }
    }
}
