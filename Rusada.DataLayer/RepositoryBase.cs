using Dapper;
using Rusada.ViewModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.DataLayer
{
   public class RepositoryBase:IRepositoryBase
    {

        protected IDbConnection _connection = null;

        public RepositoryBase(string connectionString)
        {
            /////For Connection string of Temp Database 
            //connectionString = "Server=.\\sqlexpress; AttachDbFileName=|DataDirectory|solution.mdf; Trusted_Connection=yes";
            _connection = new SqlConnection(connectionString);
        }

        public bool CanDispose { get; set; }



        protected List<T> Query<T>(string storedProcName)
        {
            return Query<T>(storedProcName, null);
        }
        protected List<T> Query<T>(string storedProcName, object parameters)
        {
            try
            {
                return _connection.Query<T>(storedProcName, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }


        protected List<T> QuerySQLText<T>(string sqlQuery)
        {
            return QuerySQLText<T>(sqlQuery, null);
        }
        protected List<T> QuerySQLText<T>(string storedProcName, object parameters)
        {
            try
            {
                return _connection.Query<T>(storedProcName, parameters, commandType: CommandType.Text).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }

        protected void Execute(string storedProcName)
        {
            Execute(storedProcName, null);
        }
        protected void Execute(string storedProcName, object parameters)
        {
            try
            {
                _connection.Execute(storedProcName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { throw; }
            finally
            {
                this.Dispose();
            }
        }

        protected void Execute(string storedProcName, object parameters, IDbTransaction transaction)
        {
            try
            {
                _connection.Execute(storedProcName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { throw; }
            finally
            {
                this.Dispose();
            }
        }

        protected object ExecuteScalar(string storedProcName)
        {
            return ExecuteScalar(storedProcName, null);
        }

        protected object ExecuteScalar(string storedProcName, object parameters)
        {
            try
            {
                return _connection.ExecuteScalar(storedProcName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { throw; }
            finally
            {
                this.Dispose();
            }
        }

        protected List<T> QueryTableView<T>(string query)
        {
            try
            {
                return _connection.Query<T>(query).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }



        #region IDisposable

        public void Dispose(bool force)
        {
            this.CanDispose = true;
            Dispose();
        }

        public void Dispose()
        {
            if (_connection != null && this.CanDispose)
                _connection.Dispose();
        }

        #endregion IDisposable

        #region Private Methods/Functions

        private string SQLValue(string strValue)
        {
            strValue = "'" + strValue.Replace("'", "''") + "'";
            return strValue;
        }

        #endregion Private Methods/Functions
    }
}
