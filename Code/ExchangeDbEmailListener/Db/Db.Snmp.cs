using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeDbEmailListener.Db
{
    public partial class Db
    {
        public class Snmp
        {
            #region Константы

            public const string sp = "sk_snmp_scanner";

            #endregion

            public static void SaveExchangeItem(string message)
            {
                SqlParameter pSysInfo = new SqlParameter() { ParameterName = "sys_info", Value = message, DbType = DbType.AnsiString };
                SqlParameter pExchangeType = new SqlParameter() { ParameterName = "exchange_type", Value = "email", DbType = DbType.AnsiString };

                ExecuteStoredProcedure(Snmp.sp, "insExchange", pSysInfo, pExchangeType);
            }
        }
    }
}
