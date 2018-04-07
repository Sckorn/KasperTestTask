using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace SimpleDbConnector
{
    public class DataModel
    {
        private ConnectionPool connection_pool = null;

        public DataModel(String connection_string, String provider_name, int pool_size)
        {
            this.connection_pool = new ConnectionPool(connection_string, provider_name, pool_size);
        }
 
        public DataModel(ConnectionPool connection_pool)
        {
            this.connection_pool = connection_pool;
        }

        public String ExecProcValue(String procedureName, string[] procedureParameters, DbConnection connection = null)
        {
            var result = "";
            ConnectionPoolNode node = null;

            try
            {
                if (connection == null)
                {
                    node = connection_pool.GetNode();
                    connection = node.connection;
                }
                DbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                string query = ConstructProcQueryString(procedureName, procedureParameters, true);
                cmd.CommandText = query;

                try
                {
                    cmd.Prepare();
                    DataTable dt = new DataTable();

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.FieldCount > 0)
                        {
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                                dt.Columns.Add(dc);
                            }
                            object[] rowobject = new object[dr.FieldCount];
                            while (dr.Read())
                            {
                                dr.GetValues(rowobject);
                                dt.LoadDataRow(rowobject, true);
                            }
                        }
                    }

                    result = dt.Rows[0]["value"].ToString();

                }
                finally
                {
                    cmd.Dispose();
                }
            }
            finally
            {
                if (node != null)
                {
                    node.Free();
                }
            }
            return result;
        }

        public DataTable ExecProc(String procedureName, string[] procedureParameters, DbConnection connection = null, Boolean silent = false)
        {
            ConnectionPoolNode node = null;
            DataTable dt = null;

            try
            {
                if (connection == null)
                {
                    node = connection_pool.GetNode();
                    connection = node.connection;
                }

                DbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                string query = ConstructProcQueryString(procedureName, procedureParameters, false);
                cmd.CommandText = query;
                
                try
                {
                    cmd.Prepare();

                    dt = new DataTable();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.FieldCount > 0)
                        {
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                                dt.Columns.Add(dc);
                            }
                            object[] rowobject = new object[dr.FieldCount];
                            while (dr.Read())
                            {
                                dr.GetValues(rowobject);
                                dt.LoadDataRow(rowobject, true);
                            }
                        }
                    }
                }
                finally
                {
                    cmd.Dispose();
                }
            }
            finally
            {
                node.Free();
            }
            
            return dt;
        }

        private string ConstructProcQueryString(string procedureName, string[] procedureParameters, Boolean oneValue)
        {
            string result = string.Empty;

            if (procedureName != null && !procedureName.Equals(string.Empty))
            {
                if (!oneValue)
                {
                    result = "SELECT * FROM " + procedureName + "(";
                }
                else
                {
                    result = "SELECT " + procedureName + "(";
                }
                int i = 0;
                foreach (string param in procedureParameters)
                {
                    string paramRepresentation = string.Empty;
                    paramRepresentation += (param == null ? "null" : ("'" + param.ToString() + "'"));
                    result += ((i == 0) ? "" : ", ") + paramRepresentation;
                    i++;
                }
                result += ")";

                if (oneValue)
                {
                    result += " as value;";
                }
                else result += ";";
            }
            return result;

        }
    }
}
