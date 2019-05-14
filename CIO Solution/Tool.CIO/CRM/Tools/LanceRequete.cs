using System;
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
        //ConnectionCRM Co = new ConnectionCRM("core.service.acc@olimpico.biz", "LS1setup!!", "https://iocdwtest2.crm4.dynamics.com/", "0ce3de8a-9ee5-4dd6-8a3f-2b9a208634cc", "https://localhost", "Z139u@]Ns/?JvYJWm3H2ZiLWJQ+=ee6Y");
        //DisplayErr Err = new DisplayErr();

        //Lance la requete pour recupere les NOC
        public async Task<ListNOC> LanceNOC(DisplayErr Err, ConnectionCRM Co)
        {
            ListNOC ListNoc = new ListNOC();
            ReqNOC Reqnoc = new ReqNOC();

            try
            {
                ListNoc = await Reqnoc.GetNOC(Co);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return ListNoc;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListMember> LanceMember(DisplayErr Err, ConnectionCRM Co)
        {
            ReqMember ReqMem = new ReqMember();
            ListMember ListMember = new ListMember();

            try
            {
                ListMember = await ReqMem.GetMember(Co);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return ListMember;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListCommission> LanceCommission(DisplayErr Err, ConnectionCRM Co)
        {
            ListCommission ListCom = new ListCommission();
            ReqCommission ReqCom = new ReqCommission();

            try
            {
                ListCom = await ReqCom.GetCommission(Co);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return ListCom;
        }

        //Lance la requete pour recupere les Member
        public async Task<ListFederation> LanceFederation(DisplayErr Err, ConnectionCRM Co)
        {
            ListFederation ListFede = new ListFederation();
            ReqFederation ReqFede = new ReqFederation();

            try
            {
                ListFede = await ReqFede.GetFederation(Co);
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }
            return ListFede;
        }
    }
}
