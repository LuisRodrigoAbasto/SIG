using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SIG
{
    class Conexion
    {
        string Cn = "Data Source=ABASTO\\SQLEXPRESS; Initial Catalog=SIG; Integrated Security=true";
        string Cnx = "Data Source=ABASTO\\SQLEXPRESS; Initial Catalog=CIACOM; Integrated Security=true";
        public DataTable mostrarDatoSig(string sql)
        {
            DataTable DtResultado = new DataTable();
            SqlConnection SqlCon = new SqlConnection(Cn);
            SqlDataAdapter SqlDat = new SqlDataAdapter(sql,SqlCon);
            SqlDat.Fill(DtResultado);
            SqlCon.Close();
            return DtResultado;
        }
        public DataTable mostrarDatoTabla(string sql)
        {
            DataTable DtResultadox = new DataTable();
            SqlConnection SqlConx = new SqlConnection(Cnx);
            SqlDataAdapter SqlDatx = new SqlDataAdapter(sql, SqlConx);
            SqlDatx.Fill(DtResultadox);
            SqlConx.Close();
            return DtResultadox;
        }
    }

}
