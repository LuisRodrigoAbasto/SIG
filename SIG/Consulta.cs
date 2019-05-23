using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SIG
{
    class Consulta
    {
        Conexion Cn = new Conexion();
        public DataTable buscarStringSucursal( string dato)
        {
            return Cn.mostrarDatoSig("select *from SUCURSAL where nombre like '%" + dato + "%'");

        }
        public DataTable mostrarIntSucursal(int dato)
        {
            return Cn.mostrarDatoSig("select * from SUCURSAL where OBJECTID = " + dato);

        }
        public DataTable buscarStringAvenida(string dato)
        {
            return Cn.mostrarDatoSig("select *from AVENIDAS where Nombre like '%" + dato + "%'");

        }
        public DataTable mostrarIntAvenidad(int dato)
        {
            return Cn.mostrarDatoSig("select * from AVENIDAS where OBJECTID = " + dato);
        }
        public DataTable buscarStringCliente(string dato)
        {
            return Cn.mostrarDatoTabla("select *from cliente where nombre like '%" + dato + "%'");

        }
        public DataTable mostrarIntCliente(int dato)
        {
            return Cn.mostrarDatoSig("select * from CLIENTE where OBJECTID = " + dato);

        }
    }
}
