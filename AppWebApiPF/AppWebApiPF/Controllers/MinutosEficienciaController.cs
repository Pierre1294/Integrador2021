using AppWebApiPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class MinutosEficienciaController : ApiController
    {
        [HttpGet]
        [Route("api/MinEficiencia")]
        public IEnumerable<MinutosEficiencia> Get()
        {
            IList<MinutosEficiencia> getcorte = new List<MinutosEficiencia>();
            DataSet ds = SqlComandos.EjecutarSel(@"select a.ctraba,rtrim(a.dappat)+' '+rtrim(a.dapmat)+' '+rtrim(a.dnombr) nomape,isnull(a.clinea,'')clinea,d.cbihor,
                                                    round(sum(qminpr)/datediff(mi,d.hinici,d.hfinal),2) as qefici,
                                                    case when b.hmarca01>d.hinici and b.hmarca01<d.hfinal then datediff(mi,b.hmarca01,d.hfinal) 
			                                                    when b.hmarca02<d.hfinal and b.hmarca02>d.hinici and b.hmarca01<d.hinici then datediff(mi,d.hinici,b.hmarca02) 
			                                                    when b.hmarca01>d.hinici and b.hmarca01>d.hfinal then 0
			                                                    when b.hmarca02<d.hfinal and b.hmarca02<=d.hinici then 0
			                                                    when b.hmarca01<d.hfinal and b.hmarca02 is null and c.cbihor=6 then 0 			
			                                                    else datediff(mi,d.hinici,d.hfinal) end qmindi
                                                    from bdpla..mtraba00 a
                                                    inner join bdpla..pasidi00 b on a.ctraba=b.ctraba and b.fmarca='2019-07-01'
                                                    left join pticke00 c on a.ctraba=c.ctraba and b.fmarca=c.fproce 
                                                    left join tbihor00 d on d.cbihor=c.cbihor
                                                    where csecci='19' and svigen='S' and straba='A' and ctptra='O' and clnprd='01' and ccargo in('53','54','12') 
                                                    group by a.ctraba,a.dappat,a.dapmat,a.dnombr,a.clinea,d.cbihor,d.hinici,d.hfinal,nminbh01,b.hmarca01,b.hmarca02,c.cbihor
                                                    order by a.clinea,d.cbihor,a.ctraba");

            MinutosEficiencia muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new MinutosEficiencia();
                muestra.ctraba = ds.Tables[0].Rows[i]["ctraba"].ToString().Trim();
                muestra.nomape = ds.Tables[0].Rows[i]["nomape"].ToString().Trim();
                muestra.clinea = ds.Tables[0].Rows[i]["clinea"].ToString().Trim();
                muestra.cbihor = Int32.Parse(ds.Tables[0].Rows[i]["cbihor"].ToString().Trim());
                muestra.qefici = double.Parse(ds.Tables[0].Rows[i]["qefici"].ToString().Trim());
                muestra.qmindi = Int32.Parse(ds.Tables[0].Rows[i]["qmindi"].ToString());      

                getcorte.Add(muestra);
            }
            return getcorte;
        }

        [HttpGet]
        [Route("api/EficienciaBihorarioBloque")]
        public IEnumerable<EficienciaBihorario> GetEficiencia(string cbloqu)
        {
            IList<EficienciaBihorario> geteficiencia = new List<EficienciaBihorario>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_dat_eficiencia_bloque_linea_diario",param);

            EficienciaBihorario muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new EficienciaBihorario();
                muestra.ctraba = dt.Rows[i]["ctraba"].ToString().Trim();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.fproce = DateTime.Parse(dt.Rows[i]["fproce"].ToString());
                muestra.qmindi01 = Int32.Parse(dt.Rows[i]["qmindi01"].ToString());
                muestra.qmindi02 = Int32.Parse(dt.Rows[i]["qmindi02"].ToString());
                muestra.qmindi03 = Int32.Parse(dt.Rows[i]["qmindi03"].ToString());
                muestra.qmindi04 = Int32.Parse(dt.Rows[i]["qmindi04"].ToString());
                muestra.qmindi05 = Int32.Parse(dt.Rows[i]["qmindi05"].ToString());
                muestra.qmindi06 = Int32.Parse(dt.Rows[i]["qmindi06"].ToString());
                muestra.pefici01 = Int32.Parse(dt.Rows[i]["pefici01"].ToString());
                muestra.pefici02 = Int32.Parse(dt.Rows[i]["pefici02"].ToString());
                muestra.pefici03 = Int32.Parse(dt.Rows[i]["pefici03"].ToString());
                muestra.pefici04 = Int32.Parse(dt.Rows[i]["pefici04"].ToString());
                muestra.pefici05 = Int32.Parse(dt.Rows[i]["pefici05"].ToString());
                muestra.pefici06 = Int32.Parse(dt.Rows[i]["pefici06"].ToString());                
                muestra.tqmindi = Int32.Parse(dt.Rows[i]["tqmindi"].ToString());
                muestra.tpefici = Int32.Parse(dt.Rows[i]["tpefici"].ToString());

                geteficiencia.Add(muestra);
            }
            return geteficiencia;
        }

        [HttpGet]
        [Route("api/EficienciaSemanalBloque")]
        public IEnumerable<EficienciaSemanal> GetEficienciaSemanal(string cbloqu)
        {
            IList<EficienciaSemanal> geteficiencia = new List<EficienciaSemanal>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_dat_eficiencia_bloque_linea_semanal", param);

            EficienciaSemanal muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new EficienciaSemanal();
                muestra.ctraba = dt.Rows[i]["ctraba"].ToString().Trim();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.fproce01 = dt.Rows[i]["fproce01"].ToString().Trim();
                muestra.fproce02 = dt.Rows[i]["fproce02"].ToString().Trim();
                muestra.fproce03 = dt.Rows[i]["fproce03"].ToString().Trim();
                muestra.fproce04 = dt.Rows[i]["fproce04"].ToString().Trim();
                muestra.fproce05 = dt.Rows[i]["fproce05"].ToString().Trim();
                muestra.fproce06 = dt.Rows[i]["fproce06"].ToString().Trim();
                muestra.fproce07 = dt.Rows[i]["fproce07"].ToString().Trim();
                muestra.fproce08 = dt.Rows[i]["fproce08"].ToString().Trim();
                muestra.fproce09 = dt.Rows[i]["fproce09"].ToString().Trim();
                muestra.fproce10 = dt.Rows[i]["fproce10"].ToString().Trim();
                muestra.fproce11 = dt.Rows[i]["fproce11"].ToString().Trim();
                muestra.fproce12 = dt.Rows[i]["fproce12"].ToString().Trim();
                muestra.fproce13 = dt.Rows[i]["fproce13"].ToString().Trim();
                muestra.fproce14 = dt.Rows[i]["fproce14"].ToString().Trim();
                muestra.fproce15 = dt.Rows[i]["fproce15"].ToString().Trim();
                muestra.fproce16 = dt.Rows[i]["fproce16"].ToString().Trim();
                muestra.nseman01 = dt.Rows[i]["nseman01"].ToString().Trim();
                muestra.nseman02 = dt.Rows[i]["nseman02"].ToString().Trim();
                muestra.nseman03 = dt.Rows[i]["nseman03"].ToString().Trim();
                muestra.nseman04 = dt.Rows[i]["nseman04"].ToString().Trim();
                muestra.nseman05 = dt.Rows[i]["nseman05"].ToString().Trim();
                muestra.nseman06 = dt.Rows[i]["nseman06"].ToString().Trim();
                muestra.nseman07 = dt.Rows[i]["nseman07"].ToString().Trim();
                muestra.nseman08 = dt.Rows[i]["nseman08"].ToString().Trim();
                muestra.nseman09 = dt.Rows[i]["nseman09"].ToString().Trim();
                muestra.nseman10 = dt.Rows[i]["nseman10"].ToString().Trim();
                muestra.nseman11 = dt.Rows[i]["nseman11"].ToString().Trim();
                muestra.nseman12 = dt.Rows[i]["nseman12"].ToString().Trim();
                muestra.nseman13 = dt.Rows[i]["nseman13"].ToString().Trim();
                muestra.nseman14 = dt.Rows[i]["nseman14"].ToString().Trim();
                muestra.nseman15 = dt.Rows[i]["nseman15"].ToString().Trim();
                muestra.nseman16 = dt.Rows[i]["nseman16"].ToString().Trim();
                muestra.pefici01 = Int32.Parse(dt.Rows[i]["pefici01"].ToString());
                muestra.pefici02 = Int32.Parse(dt.Rows[i]["pefici02"].ToString());
                muestra.pefici03 = Int32.Parse(dt.Rows[i]["pefici03"].ToString());
                muestra.pefici04 = Int32.Parse(dt.Rows[i]["pefici04"].ToString());
                muestra.pefici05 = Int32.Parse(dt.Rows[i]["pefici05"].ToString());
                muestra.pefici06 = Int32.Parse(dt.Rows[i]["pefici06"].ToString());
                muestra.pefici07 = Int32.Parse(dt.Rows[i]["pefici07"].ToString());
                muestra.pefici08 = Int32.Parse(dt.Rows[i]["pefici08"].ToString());
                muestra.pefici09 = Int32.Parse(dt.Rows[i]["pefici09"].ToString());
                muestra.pefici10 = Int32.Parse(dt.Rows[i]["pefici10"].ToString());
                muestra.pefici11 = Int32.Parse(dt.Rows[i]["pefici11"].ToString());
                muestra.pefici12 = Int32.Parse(dt.Rows[i]["pefici12"].ToString());
                muestra.pefici13 = Int32.Parse(dt.Rows[i]["pefici13"].ToString());
                muestra.pefici14 = Int32.Parse(dt.Rows[i]["pefici14"].ToString());
                muestra.pefici15 = Int32.Parse(dt.Rows[i]["pefici15"].ToString());
                muestra.pefici16 = Int32.Parse(dt.Rows[i]["pefici16"].ToString());

                geteficiencia.Add(muestra);
            }
            return geteficiencia;
        }

        [HttpGet]
        [Route("api/EficienciaErrorSemanalBloque")]
        public IEnumerable<EficienciaErrorSemanal> GetEficienciaErrorSemanal(string cbloqu)
        {
            IList<EficienciaErrorSemanal> geteficienciaerror = new List<EficienciaErrorSemanal>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_dat_eficiencia_error_bloque_linea_semanal", param);

            EficienciaErrorSemanal muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new EficienciaErrorSemanal();
                muestra.ctraba = dt.Rows[i]["ctraba"].ToString().Trim();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.fproce01 = dt.Rows[i]["fproce01"].ToString().Trim();
                muestra.fproce02 = dt.Rows[i]["fproce02"].ToString().Trim();
                muestra.fproce03 = dt.Rows[i]["fproce03"].ToString().Trim();
                muestra.fproce04 = dt.Rows[i]["fproce04"].ToString().Trim();
                muestra.fproce05 = dt.Rows[i]["fproce05"].ToString().Trim();
                muestra.fproce06 = dt.Rows[i]["fproce06"].ToString().Trim();
                muestra.fproce07 = dt.Rows[i]["fproce07"].ToString().Trim();
                muestra.fproce08 = dt.Rows[i]["fproce08"].ToString().Trim();
                muestra.fproce09 = dt.Rows[i]["fproce09"].ToString().Trim();
                muestra.fproce10 = dt.Rows[i]["fproce10"].ToString().Trim();
                muestra.fproce11 = dt.Rows[i]["fproce11"].ToString().Trim();
                muestra.fproce12 = dt.Rows[i]["fproce12"].ToString().Trim();
                muestra.fproce13 = dt.Rows[i]["fproce13"].ToString().Trim();
                muestra.fproce14 = dt.Rows[i]["fproce14"].ToString().Trim();
                muestra.fproce15 = dt.Rows[i]["fproce15"].ToString().Trim();
                muestra.fproce16 = dt.Rows[i]["fproce16"].ToString().Trim();
                muestra.nseman01 = dt.Rows[i]["nseman01"].ToString().Trim();
                muestra.nseman02 = dt.Rows[i]["nseman02"].ToString().Trim();
                muestra.nseman03 = dt.Rows[i]["nseman03"].ToString().Trim();
                muestra.nseman04 = dt.Rows[i]["nseman04"].ToString().Trim();
                muestra.nseman05 = dt.Rows[i]["nseman05"].ToString().Trim();
                muestra.nseman06 = dt.Rows[i]["nseman06"].ToString().Trim();
                muestra.nseman07 = dt.Rows[i]["nseman07"].ToString().Trim();
                muestra.nseman08 = dt.Rows[i]["nseman08"].ToString().Trim();
                muestra.nseman09 = dt.Rows[i]["nseman09"].ToString().Trim();
                muestra.nseman10 = dt.Rows[i]["nseman10"].ToString().Trim();
                muestra.nseman11 = dt.Rows[i]["nseman11"].ToString().Trim();
                muestra.nseman12 = dt.Rows[i]["nseman12"].ToString().Trim();
                muestra.nseman13 = dt.Rows[i]["nseman13"].ToString().Trim();
                muestra.nseman14 = dt.Rows[i]["nseman14"].ToString().Trim();
                muestra.nseman15 = dt.Rows[i]["nseman15"].ToString().Trim();
                muestra.nseman16 = dt.Rows[i]["nseman16"].ToString().Trim();
                muestra.pefici01 = Int32.Parse(dt.Rows[i]["pefici01"].ToString());
                muestra.pefici02 = Int32.Parse(dt.Rows[i]["pefici02"].ToString());
                muestra.pefici03 = Int32.Parse(dt.Rows[i]["pefici03"].ToString());
                muestra.pefici04 = Int32.Parse(dt.Rows[i]["pefici04"].ToString());
                muestra.pefici05 = Int32.Parse(dt.Rows[i]["pefici05"].ToString());
                muestra.pefici06 = Int32.Parse(dt.Rows[i]["pefici06"].ToString());
                muestra.pefici07 = Int32.Parse(dt.Rows[i]["pefici07"].ToString());
                muestra.pefici08 = Int32.Parse(dt.Rows[i]["pefici08"].ToString());
                muestra.pefici09 = Int32.Parse(dt.Rows[i]["pefici09"].ToString());
                muestra.pefici10 = Int32.Parse(dt.Rows[i]["pefici10"].ToString());
                muestra.pefici11 = Int32.Parse(dt.Rows[i]["pefici11"].ToString());
                muestra.pefici12 = Int32.Parse(dt.Rows[i]["pefici12"].ToString());
                muestra.pefici13 = Int32.Parse(dt.Rows[i]["pefici13"].ToString());
                muestra.pefici14 = Int32.Parse(dt.Rows[i]["pefici14"].ToString());
                muestra.pefici15 = Int32.Parse(dt.Rows[i]["pefici15"].ToString());
                muestra.pefici16 = Int32.Parse(dt.Rows[i]["pefici16"].ToString());
                muestra.paudit01 = Int32.Parse(dt.Rows[i]["paudit01"].ToString());
                muestra.paudit02 = Int32.Parse(dt.Rows[i]["paudit02"].ToString());
                muestra.paudit03 = Int32.Parse(dt.Rows[i]["paudit03"].ToString());
                muestra.paudit04 = Int32.Parse(dt.Rows[i]["paudit04"].ToString());
                muestra.paudit05 = Int32.Parse(dt.Rows[i]["paudit05"].ToString());
                muestra.paudit06 = Int32.Parse(dt.Rows[i]["paudit06"].ToString());
                muestra.paudit07 = Int32.Parse(dt.Rows[i]["paudit07"].ToString());
                muestra.paudit08 = Int32.Parse(dt.Rows[i]["paudit08"].ToString());
                muestra.paudit09 = Int32.Parse(dt.Rows[i]["paudit09"].ToString());
                muestra.paudit10 = Int32.Parse(dt.Rows[i]["paudit10"].ToString());
                muestra.paudit11 = Int32.Parse(dt.Rows[i]["paudit11"].ToString());
                muestra.paudit12 = Int32.Parse(dt.Rows[i]["paudit12"].ToString());
                muestra.paudit13 = Int32.Parse(dt.Rows[i]["paudit13"].ToString());
                muestra.paudit14 = Int32.Parse(dt.Rows[i]["paudit14"].ToString());
                muestra.paudit15 = Int32.Parse(dt.Rows[i]["paudit15"].ToString());
                muestra.paudit16 = Int32.Parse(dt.Rows[i]["paudit16"].ToString());

                geteficienciaerror.Add(muestra);
            }
            return geteficienciaerror;
        }

        [HttpGet]
        [Route("api/DashboardEficiencia")]
        public IEnumerable<DashboardEficiencia> GetDashboardEficiencia(string cbloqu)
        {
            IList<DashboardEficiencia> getdashboardeficiencia = new List<DashboardEficiencia>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_dashboard_eficiencia_costura", param);

            DashboardEficiencia muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new DashboardEficiencia();
                muestra.fprdia = dt.Rows[i]["fprdia"].ToString();
                muestra.pefici = Int32.Parse(dt.Rows[i]["pefici"].ToString());
                muestra.perror = Int32.Parse(dt.Rows[i]["perror"].ToString());

                getdashboardeficiencia.Add(muestra);
            }
            return getdashboardeficiencia;
        }

        [HttpGet]
        [Route("api/DashboardEficienciaLinea")]
        public IEnumerable<DashboardEficienciaLinea> GetDashboardEficienciaLinea(string cbloqu)
        {
            IList<DashboardEficienciaLinea> getdashboardeficiencialinea = new List<DashboardEficienciaLinea>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_dashboard_eficiencia_costura_linea", param);

            DashboardEficienciaLinea muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new DashboardEficienciaLinea();
                muestra.fprdia = dt.Rows[i]["fprdia"].ToString();
                muestra.pefici01 = Int32.Parse(dt.Rows[i]["pefici01"].ToString());
                muestra.pefici02 = Int32.Parse(dt.Rows[i]["pefici02"].ToString());
                muestra.linea01 = dt.Rows[i]["linea01"].ToString();
                muestra.linea02 = dt.Rows[i]["linea02"].ToString();

                getdashboardeficiencialinea.Add(muestra);
            }
            return getdashboardeficiencialinea;
        }

        [HttpGet]
        [Route("api/EficienciaDestajeroDetalle")]
        public IEnumerable<EficienciaDestajeroDetalle> GetEficienciaDestajeroDetalle(string cbloqu,string ctraba,string fproce)
        {
            IList<EficienciaDestajeroDetalle> getefidetalle = new List<EficienciaDestajeroDetalle>();
            DataSet ds = SqlComandos.EjecutarSel(@"select a.clinea as cbloqu,a.clinea01 as clinea,a.ctraba,a.nordpr,a.copera,b.dopera,a.cbihor,round((sum(a.qminpr)/c.qmindi)*100,0) as pefici,qmindi,sum(a.qprend)qprend,sum(a.qminpr)qminpr,sum(a.qmixpr)qmixpr 
                                                    from pticke00 a
                                                    left join tsecop00 b on a.copera=b.copera
                                                    left join (select cbihor,datediff(mi,hinici,hfinal)qmindi from tbihor00) c on a.cbihor=c.cbihor 
                                                    where clinea='"+cbloqu+"' and a.ctraba='"+ctraba+"' and fproce='"+ fproce + "' and sticke='T' group by a.clinea,a.clinea01,a.ctraba,a.nordpr,b.dopera,a.copera,a.cbihor,c.qmindi,a.clinea,a.clinea01 order by a.clinea,a.clinea01,a.ctraba,a.cbihor,a.nordpr");

            EficienciaDestajeroDetalle muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new EficienciaDestajeroDetalle();
                muestra.cbloqu = ds.Tables[0].Rows[i]["cbloqu"].ToString().Trim();
                muestra.clinea = ds.Tables[0].Rows[i]["clinea"].ToString().Trim();
                muestra.ctraba = ds.Tables[0].Rows[i]["ctraba"].ToString().Trim();
                muestra.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString().Trim();
                muestra.copera = ds.Tables[0].Rows[i]["copera"].ToString().Trim();
                muestra.dopera = ds.Tables[0].Rows[i]["dopera"].ToString().Trim();
                muestra.cbihor = ds.Tables[0].Rows[i]["cbihor"].ToString().Trim();
                muestra.pefici = double.Parse(ds.Tables[0].Rows[i]["pefici"].ToString().Trim());
                muestra.qmindi = Int32.Parse(ds.Tables[0].Rows[i]["qmindi"].ToString());
                muestra.qprend = Int32.Parse(ds.Tables[0].Rows[i]["qprend"].ToString());
                muestra.qminpr = double.Parse(ds.Tables[0].Rows[i]["qminpr"].ToString().Trim());
                muestra.qmixpr = double.Parse(ds.Tables[0].Rows[i]["qmixpr"].ToString().Trim());

                getefidetalle.Add(muestra);
            }
            return getefidetalle;
        }

        [HttpGet]
        [Route("api/AuditoriaDefectos")]
        public IEnumerable<AuditoriaDefectos> GetAuditoriaDefectos(string cbloqu)
        {
            IList<AuditoriaDefectos> geteficienciaerror = new List<AuditoriaDefectos>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@wclinea", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_auditoria_defectos", param);

            AuditoriaDefectos muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new AuditoriaDefectos();
                muestra.ctraba = dt.Rows[i]["ctraba"].ToString().Trim();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.fproce01 = dt.Rows[i]["fproce01"].ToString().Trim();
                muestra.fproce02 = dt.Rows[i]["fproce02"].ToString().Trim();
                muestra.fproce03 = dt.Rows[i]["fproce03"].ToString().Trim();
                muestra.fproce04 = dt.Rows[i]["fproce04"].ToString().Trim();
                muestra.fproce05 = dt.Rows[i]["fproce05"].ToString().Trim();
                muestra.fproce06 = dt.Rows[i]["fproce06"].ToString().Trim();
                muestra.fproce07 = dt.Rows[i]["fproce07"].ToString().Trim();
                muestra.nmuest01 = Int32.Parse(dt.Rows[i]["nmuest01"].ToString().Trim());
                muestra.nmuest02 = Int32.Parse(dt.Rows[i]["nmuest02"].ToString().Trim());
                muestra.nmuest03 = Int32.Parse(dt.Rows[i]["nmuest03"].ToString().Trim());
                muestra.nmuest04 = Int32.Parse(dt.Rows[i]["nmuest04"].ToString().Trim());
                muestra.nmuest05 = Int32.Parse(dt.Rows[i]["nmuest05"].ToString().Trim());
                muestra.nmuest06 = Int32.Parse(dt.Rows[i]["nmuest06"].ToString().Trim());
                muestra.nmuest07 = Int32.Parse(dt.Rows[i]["nmuest01"].ToString().Trim());
                muestra.ndefec01 = dt.Rows[i]["ndefec01"].ToString().Trim();
                muestra.ndefec02 = dt.Rows[i]["ndefec02"].ToString().Trim();
                muestra.ndefec03 = dt.Rows[i]["ndefec03"].ToString().Trim();
                muestra.ndefec04 = dt.Rows[i]["ndefec04"].ToString().Trim();
                muestra.ndefec05 = dt.Rows[i]["ndefec05"].ToString().Trim();
                muestra.ndefec06 = dt.Rows[i]["ndefec06"].ToString().Trim();
                muestra.ndefec07 = dt.Rows[i]["ndefec07"].ToString().Trim();
                muestra.pordef01 = Int32.Parse(dt.Rows[i]["pordef01"].ToString().Trim());
                muestra.pordef02 = Int32.Parse(dt.Rows[i]["pordef02"].ToString().Trim());
                muestra.pordef03 = Int32.Parse(dt.Rows[i]["pordef03"].ToString().Trim());
                muestra.pordef04 = Int32.Parse(dt.Rows[i]["pordef04"].ToString().Trim());
                muestra.pordef05 = Int32.Parse(dt.Rows[i]["pordef05"].ToString().Trim());
                muestra.pordef06 = Int32.Parse(dt.Rows[i]["pordef06"].ToString().Trim());
                muestra.pordef07 = Int32.Parse(dt.Rows[i]["pordef07"].ToString().Trim());
                //muestra.pordef01 = Int32.Parse(dt.Rows[i]["qaudia01"].ToString().Trim());
                //muestra.pordef02 = Int32.Parse(dt.Rows[i]["qaudia02"].ToString().Trim());
                //muestra.pordef03 = Int32.Parse(dt.Rows[i]["qaudia03"].ToString().Trim());
                //muestra.pordef04 = Int32.Parse(dt.Rows[i]["qaudia04"].ToString().Trim());
                //muestra.pordef05 = Int32.Parse(dt.Rows[i]["qaudia05"].ToString().Trim());
                //muestra.pordef06 = Int32.Parse(dt.Rows[i]["qaudia06"].ToString().Trim());
                //muestra.pordef07 = Int32.Parse(dt.Rows[i]["qaudia07"].ToString().Trim());
                //muestra.pordef01 = Int32.Parse(dt.Rows[i]["qaudir01"].ToString().Trim());
                //muestra.pordef02 = Int32.Parse(dt.Rows[i]["qaudir02"].ToString().Trim());
                //muestra.pordef03 = Int32.Parse(dt.Rows[i]["qaudir03"].ToString().Trim());
                //muestra.pordef04 = Int32.Parse(dt.Rows[i]["qaudir04"].ToString().Trim());
                //muestra.pordef05 = Int32.Parse(dt.Rows[i]["qaudir05"].ToString().Trim());
                //muestra.pordef06 = Int32.Parse(dt.Rows[i]["qaudir06"].ToString().Trim());
                //muestra.pordef07 = Int32.Parse(dt.Rows[i]["qaudir07"].ToString().Trim());

                geteficienciaerror.Add(muestra);
            }
            return geteficienciaerror;
        }

        [HttpGet]
        [Route("api/ReporteAuditoriaDefectos")]
        public IEnumerable<ReporteAuditoriaDefectos> GetReporteAuditoriaDefectos(string cbloqu)
        {
            IList<ReporteAuditoriaDefectos> geteficienciaerror = new List<ReporteAuditoriaDefectos>();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@wclinea", SqlDbType.Char);
            param[0].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_reporte_auditoria_defectos", param);

            ReporteAuditoriaDefectos muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new ReporteAuditoriaDefectos();
                muestra.ctraba = dt.Rows[i]["ctraba"].ToString().Trim();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.fproce01 = dt.Rows[i]["fproce01"].ToString().Trim();
                muestra.fproce02 = dt.Rows[i]["fproce02"].ToString().Trim();
                muestra.fproce03 = dt.Rows[i]["fproce03"].ToString().Trim();
                muestra.fproce04 = dt.Rows[i]["fproce04"].ToString().Trim();
                muestra.fproce05 = dt.Rows[i]["fproce05"].ToString().Trim();
                muestra.fproce06 = dt.Rows[i]["fproce06"].ToString().Trim();
                muestra.fproce07 = dt.Rows[i]["fproce07"].ToString().Trim();
                muestra.nmuest01 = Int32.Parse(dt.Rows[i]["nmuest01"].ToString().Trim());
                muestra.nmuest02 = Int32.Parse(dt.Rows[i]["nmuest02"].ToString().Trim());
                muestra.nmuest03 = Int32.Parse(dt.Rows[i]["nmuest03"].ToString().Trim());
                muestra.nmuest04 = Int32.Parse(dt.Rows[i]["nmuest04"].ToString().Trim());
                muestra.nmuest05 = Int32.Parse(dt.Rows[i]["nmuest05"].ToString().Trim());
                muestra.nmuest06 = Int32.Parse(dt.Rows[i]["nmuest06"].ToString().Trim());
                muestra.nmuest07 = Int32.Parse(dt.Rows[i]["nmuest01"].ToString().Trim());
                muestra.ndefec01 = dt.Rows[i]["ndefec01"].ToString().Trim();
                muestra.ndefec02 = dt.Rows[i]["ndefec02"].ToString().Trim();
                muestra.ndefec03 = dt.Rows[i]["ndefec03"].ToString().Trim();
                muestra.ndefec04 = dt.Rows[i]["ndefec04"].ToString().Trim();
                muestra.ndefec05 = dt.Rows[i]["ndefec05"].ToString().Trim();
                muestra.ndefec06 = dt.Rows[i]["ndefec06"].ToString().Trim();
                muestra.ndefec07 = dt.Rows[i]["ndefec07"].ToString().Trim();
                muestra.pordef01 = Int32.Parse(dt.Rows[i]["pordef01"].ToString().Trim());
                muestra.pordef02 = Int32.Parse(dt.Rows[i]["pordef02"].ToString().Trim());
                muestra.pordef03 = Int32.Parse(dt.Rows[i]["pordef03"].ToString().Trim());
                muestra.pordef04 = Int32.Parse(dt.Rows[i]["pordef04"].ToString().Trim());
                muestra.pordef05 = Int32.Parse(dt.Rows[i]["pordef05"].ToString().Trim());
                muestra.pordef06 = Int32.Parse(dt.Rows[i]["pordef06"].ToString().Trim());
                muestra.pordef07 = Int32.Parse(dt.Rows[i]["pordef07"].ToString().Trim());
                muestra.qaudia01 = Int32.Parse(dt.Rows[i]["qaudia01"].ToString().Trim());
                muestra.qaudia02 = Int32.Parse(dt.Rows[i]["qaudia02"].ToString().Trim());
                muestra.qaudia03 = Int32.Parse(dt.Rows[i]["qaudia03"].ToString().Trim());
                muestra.qaudia04 = Int32.Parse(dt.Rows[i]["qaudia04"].ToString().Trim());
                muestra.qaudia05 = Int32.Parse(dt.Rows[i]["qaudia05"].ToString().Trim());
                muestra.qaudia06 = Int32.Parse(dt.Rows[i]["qaudia06"].ToString().Trim());
                muestra.qaudia07 = Int32.Parse(dt.Rows[i]["qaudia07"].ToString().Trim());
                muestra.qaudir01 = Int32.Parse(dt.Rows[i]["qaudir01"].ToString().Trim());
                muestra.qaudir02 = Int32.Parse(dt.Rows[i]["qaudir02"].ToString().Trim());
                muestra.qaudir03 = Int32.Parse(dt.Rows[i]["qaudir03"].ToString().Trim());
                muestra.qaudir04 = Int32.Parse(dt.Rows[i]["qaudir04"].ToString().Trim());
                muestra.qaudir05 = Int32.Parse(dt.Rows[i]["qaudir05"].ToString().Trim());
                muestra.qaudir06 = Int32.Parse(dt.Rows[i]["qaudir06"].ToString().Trim());
                muestra.qaudir07 = Int32.Parse(dt.Rows[i]["qaudir07"].ToString().Trim());

                geteficienciaerror.Add(muestra);
            }
            return geteficienciaerror;
        }

        [HttpGet]
        [Route("api/ProgramaCosturaBloqueLinea")]
        public IEnumerable<ProgramaCostura> GetProgramaCosturaBloqueLinea(string cbloque,DateTime wfecini, DateTime wfecfin)
        {
            IList<ProgramaCostura> getprogramacostura = new List<ProgramaCostura>();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@cbloque", SqlDbType.Char);
            param[0].Value = cbloque;
            param[1] = new SqlParameter("@wfecini", SqlDbType.DateTime);
            param[1].Value = wfecini;
            param[2] = new SqlParameter("@wfecfin", SqlDbType.DateTime);
            param[2].Value = wfecfin;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_programa_costura_bloque_linea", param);

            ProgramaCostura muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new ProgramaCostura();
                muestra.nordpr = dt.Rows[i]["nordpr"].ToString().Trim();
                muestra.cbloqu = dt.Rows[i]["cbloqu"].ToString().Trim();
                muestra.clinea = dt.Rows[i]["clinea"].ToString().Trim();
                muestra.ccarub = dt.Rows[i]["ccarub"].ToString().Trim();
                muestra.dcarub = dt.Rows[i]["dcarub"].ToString().Trim();
                muestra.modali = dt.Rows[i]["modali"].ToString().Trim();
                muestra.dclien = dt.Rows[i]["dclien"].ToString().Trim();
                muestra.fproce01 = dt.Rows[i]["fproce01"].ToString().Trim();
                muestra.fproce02 = dt.Rows[i]["fproce02"].ToString().Trim();
                muestra.fproce03 = dt.Rows[i]["fproce03"].ToString().Trim();
                muestra.fproce04 = dt.Rows[i]["fproce04"].ToString().Trim();
                muestra.fproce05 = dt.Rows[i]["fproce05"].ToString().Trim();
                muestra.fproce06 = dt.Rows[i]["fproce06"].ToString().Trim();
                muestra.fproce07 = dt.Rows[i]["fproce07"].ToString().Trim();
                muestra.fproce08 = dt.Rows[i]["fproce08"].ToString().Trim();
                muestra.fproce09 = dt.Rows[i]["fproce09"].ToString().Trim();
                muestra.fproce10 = dt.Rows[i]["fproce10"].ToString().Trim();
                muestra.fproce11 = dt.Rows[i]["fproce11"].ToString().Trim();
                muestra.fproce12 = dt.Rows[i]["fproce12"].ToString().Trim();
                muestra.fproce13 = dt.Rows[i]["fproce13"].ToString().Trim();
                muestra.fproce14 = dt.Rows[i]["fproce14"].ToString().Trim();
                muestra.fproce15 = dt.Rows[i]["fproce15"].ToString().Trim();
                muestra.qprogr01 = Int32.Parse(dt.Rows[i]["qprogr01"].ToString());
                muestra.qprogr02 = Int32.Parse(dt.Rows[i]["qprogr02"].ToString());
                muestra.qprogr03 = Int32.Parse(dt.Rows[i]["qprogr03"].ToString());
                muestra.qprogr04 = Int32.Parse(dt.Rows[i]["qprogr04"].ToString());
                muestra.qprogr05 = Int32.Parse(dt.Rows[i]["qprogr05"].ToString());
                muestra.qprogr06 = Int32.Parse(dt.Rows[i]["qprogr06"].ToString());
                muestra.qprogr07 = Int32.Parse(dt.Rows[i]["qprogr07"].ToString());
                muestra.qprogr08 = Int32.Parse(dt.Rows[i]["qprogr08"].ToString());
                muestra.qprogr09 = Int32.Parse(dt.Rows[i]["qprogr09"].ToString());
                muestra.qprogr10 = Int32.Parse(dt.Rows[i]["qprogr10"].ToString());
                muestra.qprogr11 = Int32.Parse(dt.Rows[i]["qprogr11"].ToString());
                muestra.qprogr12 = Int32.Parse(dt.Rows[i]["qprogr12"].ToString());
                muestra.qprogr13 = Int32.Parse(dt.Rows[i]["qprogr13"].ToString());
                muestra.qprogr14 = Int32.Parse(dt.Rows[i]["qprogr14"].ToString());
                muestra.qprogr15 = Int32.Parse(dt.Rows[i]["qprogr15"].ToString());

                getprogramacostura.Add(muestra);
            }
            return getprogramacostura;
        }

        [HttpGet]
        [Route("api/IngresoPrendasCostura")]
        public IEnumerable<IngresoPrendasCostura> GetIngresoPrendasCostura(string ctipos, DateTime wfecini, DateTime wfecfin,string nordpr,string sbloqc)
        {
            IList<IngresoPrendasCostura> getprogramacostura = new List<IngresoPrendasCostura>();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@wctipos", SqlDbType.Char);
            param[0].Value = ctipos;
            param[1] = new SqlParameter("@wfinici", SqlDbType.DateTime);
            param[1].Value = wfecini;
            param[2] = new SqlParameter("@wffinal", SqlDbType.DateTime);
            param[2].Value = wfecfin;
            param[3] = new SqlParameter("@wnordpr", SqlDbType.Char);
            param[3].Value = nordpr;
            param[4] = new SqlParameter("@wsbloqc", SqlDbType.Char);
            param[4].Value = sbloqc;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_ingreso_prendas_costura", param);

            IngresoPrendasCostura muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new IngresoPrendasCostura();
                muestra.dtraba = dt.Rows[i]["dtraba"].ToString().Trim();
                muestra.nordpr = dt.Rows[i]["nordpr"].ToString().Trim();
                muestra.nordct = dt.Rows[i]["nordct"].ToString().Trim();
                muestra.dcarub = dt.Rows[i]["dcarub"].ToString().Trim();
                muestra.dclien = dt.Rows[i]["dclien"].ToString().Trim();
                muestra.qprend01 = Int32.Parse(dt.Rows[i]["qprend01"].ToString());
                muestra.qprend02 = Int32.Parse(dt.Rows[i]["qprend02"].ToString());
                muestra.qprend03 = Int32.Parse(dt.Rows[i]["qprend03"].ToString());
                muestra.qprend04 = Int32.Parse(dt.Rows[i]["qprend04"].ToString());
                muestra.qprend05 = Int32.Parse(dt.Rows[i]["qprend05"].ToString());
                muestra.qprend06 = Int32.Parse(dt.Rows[i]["pqrend06"].ToString());
                muestra.qprend07 = Int32.Parse(dt.Rows[i]["qprend07"].ToString());
                muestra.qprend08 = Int32.Parse(dt.Rows[i]["qprend08"].ToString());
                muestra.qprend09 = Int32.Parse(dt.Rows[i]["qprend09"].ToString());
                muestra.qprend10 = Int32.Parse(dt.Rows[i]["qprend10"].ToString());
                muestra.qprend11 = Int32.Parse(dt.Rows[i]["qprend11"].ToString());
                muestra.qprend12 = Int32.Parse(dt.Rows[i]["qprend12"].ToString());
                muestra.qprend13 = Int32.Parse(dt.Rows[i]["qprend13"].ToString());
                muestra.qpreop = Int32.Parse(dt.Rows[i]["qpreop"].ToString());
                muestra.qpreac = Int32.Parse(dt.Rows[i]["qpreac"].ToString());
                muestra.qsaldo = Int32.Parse(dt.Rows[i]["qsaldo"].ToString());

                getprogramacostura.Add(muestra);
            }
            return getprogramacostura;
        }

        [HttpGet]
        [Route("api/BalanceLineaCostura")]
        public IEnumerable<BalanceLineaCostura> GetBalanceLineaCostura(string nordpr, string cbloqu)
        {
            IList<BalanceLineaCostura> getprogramacostura = new List<BalanceLineaCostura>();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@nordpr", SqlDbType.Char);
            param[0].Value = nordpr;
            param[1] = new SqlParameter("@cbloqu", SqlDbType.Char);
            param[1].Value = cbloqu;
            DataTable dt = SqlComandos.ExecuteDataTable("sp_balance_linea_costura", param);

            BalanceLineaCostura muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new BalanceLineaCostura();
                muestra.dbloqu = dt.Rows[i]["dbloqu"].ToString().Trim();
                muestra.copera = dt.Rows[i]["copera"].ToString().Trim();
                muestra.dopera = dt.Rows[i]["dopera"].ToString().Trim();
                muestra.ctpope = dt.Rows[i]["ctpope"].ToString().Trim();
                muestra.cmaqui = dt.Rows[i]["cmaqui"].ToString().Trim();
                muestra.qmixpr = double.Parse(dt.Rows[i]["qmixpr"].ToString());
                muestra.qprxho = Int32.Parse(dt.Rows[i]["qprxho"].ToString());
                muestra.qprdav = Int32.Parse(dt.Rows[i]["qprdav"].ToString());
                muestra.qavanc = Int32.Parse(dt.Rows[i]["qavanc"].ToString());
                muestra.pavanc = Int32.Parse(dt.Rows[i]["pavanc"].ToString());
                muestra.minava = Int32.Parse(dt.Rows[i]["minava"].ToString());
                muestra.minxpr = Int32.Parse(dt.Rows[i]["minxpr"].ToString());

                getprogramacostura.Add(muestra);
            }
            return getprogramacostura;
        }

        [HttpGet]
        [Route("api/GetOpProgCostura")]
        public IEnumerable<procos00> GetOpProgCostura(string cbloqu)
        {
            IList<procos00> getprocos= new List<procos00>();
            DataSet ds = SqlComandos.EjecutarSel(@"select top 30 b.nordpr,b.ccarub,b.dcarub,d.drzsoc dclien,d.nivaql
                                                    from  
                                                     pcoxes00 b 
                                                    inner join pdgorp00 c on b.nordpr=c.nordpr
                                                    left join mclien00 d on c.cclien=d.cclien
                                                     group by b.nordpr,b.ccarub,b.dcarub,d.drzsoc,d.nivaql");

            procos00 muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new procos00();
                muestra.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString().Trim();
                muestra.ccarub = ds.Tables[0].Rows[i]["ccarub"].ToString().Trim();
                muestra.dcarub = ds.Tables[0].Rows[i]["dcarub"].ToString().Trim();
                muestra.dclien = ds.Tables[0].Rows[i]["dclien"].ToString().Trim();
                muestra.nivaql = ds.Tables[0].Rows[i]["nivaql"].ToString().Trim();

                getprocos.Add(muestra);
            }
            return getprocos;
        }


        [HttpGet]                
        [Route("api/PlanDespacho")]
        public IEnumerable<PlanDespacho> GetPlanDespacho()
        {
            IList<PlanDespacho> getprogramacostura = new List<PlanDespacho>();
            SqlParameter[] param = new SqlParameter[0];

            DataTable dt = SqlComandos.ExecuteDataTable("sp_plan_despacho", param);

            PlanDespacho muestra = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                muestra = new PlanDespacho();                
                muestra.nordpr = dt.Rows[i]["nordpr"].ToString().Trim();
                muestra.nordco = dt.Rows[i]["nordco"].ToString().Trim();
                muestra.dclien = dt.Rows[i]["drzsoc"].ToString().Trim();
                muestra.qitems = Int32.Parse(dt.Rows[i]["qitems"].ToString());
                muestra.fembac = DateTime.Parse(dt.Rows[i]["fembac"].ToString());
                muestra.fembar = DateTime.Parse(dt.Rows[i]["fembar"].ToString());
                

                getprogramacostura.Add(muestra);
            }
            return getprogramacostura;
        }

    }
}

