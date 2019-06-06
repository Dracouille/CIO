﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.CIO.CRM.Tools;
using Tool.CIO.CRM.Connect;
using Tool.CIO.Log;

namespace Tool.CIO.CRM.Tools
{
    public class LanceRequete
    {

        //Var
        ListMember listMember { set;get;}
        ListNOC listNOC { set;get;}
        ListCommission listCom { set;get;}
        ListFederation listFed { set;get;}

        string sReqNoc = "accounts?savedQuery=f45ac9ad-6a80-e911-a967-000d3a441bb5";
        string sReqMem = "ioc_roles?savedQuery=e4c58ddc-6880-e911-a967-000d3a441bb5";
        string sReqCommission = "ioc_roles?savedQuery=08e051c2-6980-e911-a967-000d3a441bb5";
        string sReqFédé = "accounts?savedQuery=28accd52-6a80-e911-a967-000d3a441bb5";

        #region ReqAsync
        //Lance la requete pour recupere les NOC
        public async Task<ListNOC> LanceNOC(DisplayErr Err, ConnectionCRM Co)
        {
            ReqNOC Reqnoc = new ReqNOC();

            try
            {
                listNOC = await Reqnoc.GetNOC(Co, sReqNoc);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return listNOC;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListMember> LanceMember(DisplayErr Err, ConnectionCRM Co)
        {
            ReqMember ReqMem = new ReqMember();

            try
            {
                listMember = await ReqMem.GetMember(Co, sReqMem);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return listMember;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListCommission> LanceCommission(DisplayErr Err, ConnectionCRM Co)
        {
            ReqCommission ReqCom = new ReqCommission();

            try
            {
                listCom = await ReqCom.GetCommission(Co, sReqCommission);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return listCom;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListFederation> LanceFederation(DisplayErr Err, ConnectionCRM Co)
        {
            ReqFederation ReqFede = new ReqFederation();

            try
            {
                listFed = await ReqFede.GetFederation(Co, sReqFédé);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return listFed;
        }
        #endregion

        #region Accesseur Async

        public ListMember GetResMember(DisplayErr Err, ConnectionCRM Co)
        {
            try
            {
                Task.WaitAll(Task.Run(async () => await LanceMember(Err, Co)));
            }
            catch (System.Exception ex) {
                Err.DisplayException(ex);
            }
            return (listMember);
        }

        public ListNOC GetResNOC(DisplayErr Err, ConnectionCRM Co)
        {
            try
            {
                Task.WaitAll(Task.Run(async () => await LanceNOC(Err, Co)));
            }
            catch (System.Exception ex)
            {
                Err.DisplayException(ex);
            }
            return (listNOC);
        }

        public ListCommission GetResCommi(DisplayErr Err, ConnectionCRM Co)
        {
            try
            {
                Task.WaitAll(Task.Run(async () => await LanceCommission(Err, Co)));
            }
            catch (System.Exception ex)
            {
                Err.DisplayException(ex);
            }
            return (listCom);
        }

        public ListFederation GetResFede(DisplayErr Err, ConnectionCRM Co)
        {
            try
            {
                Task.WaitAll(Task.Run(async () => await LanceFederation(Err, Co)));
            }
            catch (System.Exception ex)
            {
                Err.DisplayException(ex);
            }
            return (listFed);
        }

        #endregion
    }
}
