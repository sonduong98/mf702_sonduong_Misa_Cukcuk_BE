using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DataLayer
{
    public partial class MisaContext 
    {
        public static string connectionString;

        public IDbConnection _dbConnection;

        public MisaContext()
        {
            _dbConnection = new SqlConnection(connectionString);
        }
        public async Task<IEnumerable<TEntity>> GetData<TEntity>()
        {
            string className = typeof(TEntity).Name;
            var sql = $"SELECT * FROM {className}";
            var entities = await _dbConnection.QueryAsync<TEntity>(sql);
            return entities;
        }

        public IEnumerable<TEntity> GetData<TEntity>(string commandText)
        {
            string className = typeof(TEntity).Name;
            var sql = commandText;
            var entities = _dbConnection.Query<TEntity>(sql);
            return entities;
        }
        public TEntity GetById<TEntity>(object id)
        {
            string className = typeof(TEntity).Name;
            var sql = $"SELECT * FROM {className} WHERE {className}Id = '{id.ToString()}'";
            return _dbConnection.Query<TEntity>(sql).FirstOrDefault();
        }
        public async Task<int> Insert<TEntity>(TEntity entity)
        {
            string className = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties();
            var parameters = new DynamicParameters();
            var sqlPropetyBuider = string.Empty;
            var sqlPropetyParamBuider = string.Empty;
            foreach (var propety in properties)
            {
                var propertyName = propety.Name;
                var propertyValue = propety.GetValue(entity);
                parameters.Add($"@{propertyName}", propertyValue);

                sqlPropetyBuider += $",{propertyName}";
                sqlPropetyParamBuider += $",@{propertyName}";
            }
            var sql = $"INSERT INTO {className}({sqlPropetyBuider.Substring(1)}) VALUES ({sqlPropetyParamBuider.Substring(1)})";
            var effectRows = await _dbConnection.ExecuteAsync(sql, parameters);
            return effectRows;
        }
        public async Task<int> Update<TEntity>(TEntity entity)
        {
            string className = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties();
            var parameters = new DynamicParameters();
            var sqlPropetyBuider = string.Empty;
            var sqlPropetyParamBuider = string.Empty;
            foreach (var propety in properties)
            {
                var propertyName = propety.Name;
                var propertyValue = propety.GetValue(entity);
                parameters.Add($"@{propertyName}", propertyValue);
                if(propertyName != "EmployeeId")
                    sqlPropetyBuider += $",{propertyName}=@{propertyName}";
                sqlPropetyParamBuider += $",@{propertyName}";
            }
            var sql = $"Update  {className} SET {sqlPropetyBuider.Substring(1)} where EmployeeId={properties[0].GetValue(entity)}";
            var effectRows = await _dbConnection.ExecuteAsync(sql, parameters);
            return effectRows;
        }
        public async Task<int> Delete<TEntity>(int id)
        {
            string className = typeof(TEntity).Name;
           
            
            var sql = $"delete from  {className} where EmployeeId={id}";
            var effectRows = await _dbConnection.ExecuteAsync(sql);
            return effectRows;
        }

    }
}
