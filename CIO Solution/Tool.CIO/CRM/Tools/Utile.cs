using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public static class Utile
    {
        public static string ConvertStringSQL(string MaChaine)
        {
            if (string.IsNullOrEmpty(MaChaine))
            {
                return "";
            }
            else
            {
                return (MaChaine.Replace("'", "''"));
            }
        }

        //Forme la string en date et return en string(année)
        public static string SubDate(string DateComplet)
        {
            if (string.IsNullOrEmpty(DateComplet))
            {
                return "";
            }
            else
            {
                return DateComplet.Substring(0, 4);
            }
        }
    }
}
   
