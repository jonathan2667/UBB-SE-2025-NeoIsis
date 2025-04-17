using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Data.Interfaces
{
    public interface IDatabaseHelper
    {
        DataTable ExecuteReader(string commandText, SqlParameter[] parameters);
        int ExecuteNonQuery(string commandText, SqlParameter[] parameters);

        T ExecuteScalar<T>(string commandText, SqlParameter[]? parameters = null);
    }
}
