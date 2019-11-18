using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.MSSQL
{
    /// <summary>
    /// docs
    /// https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder?view=netframework-4.8
    /// </summary>
    public class SQLConnector
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseWindowsAuth { get; set; }
        public string Catalog { get; set; }

        private string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = Server;
                if (!String.IsNullOrEmpty(Catalog)) builder.InitialCatalog = Catalog;
                builder.IntegratedSecurity = false;
                builder.ConnectTimeout = 10;
                if (UseWindowsAuth)
                {
                    builder.IntegratedSecurity = true;
                }
                else
                {
                    builder.UserID = Username;
                    builder.Password = Password;
                }
                return builder.ConnectionString;
            }
        }

        public SQLConnector(string Server, string Username, string Password)
        {
            this.Server = Server;
            this.Username = Username;
            this.Password = Password;
            this.UseWindowsAuth = false;
        }

        public SQLConnector(string Server)
        {
            this.Server = Server;
            this.Username = "";
            this.Password = "";
            this.UseWindowsAuth = true;
        }

        public bool CheckConnection()
        {
            try
            {
                using (var conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return false;
        }

        private DataTable GetDataTalbe(string query)
        {
            DataTable db = new DataTable();
            try
            {
                using (var conn = new SqlConnection(this.ConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet("Data");
                    da.Fill(ds);
                    if (ds.Tables.Count == 1)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        Debug.WriteLine("Erro here");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public DataTable GetCatalogList()
        {
            string querry = "SELECT d.name AS [name] FROM sys.databases AS d " +
                "INNER JOIN sys.master_files AS m ON d.database_id = m.database_id " +
                "WHERE d.state_desc = 'ONLINE' AND m.type_desc = 'ROWS' " +
                "AND m.name not in ('master','tempdev','modeldev','MSDBData') " +
                "ORDER BY m.name;";
            return GetDataTalbe(querry);
        }

        public DataTable GetTableList()
        {
            string query = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE ='BASE TABLE' order by TABLE_NAME";
            return GetDataTalbe(query);
        }

        /// <summary>
        /// Trả về tên cột kiểu dữ liệu tương ứng
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetColumnList(string tableName)
        {
            string query = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
            return GetDataTalbe(query);
        }
    }
}
