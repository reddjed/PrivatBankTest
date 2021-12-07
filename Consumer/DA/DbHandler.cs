using Consumer.Common;
using Consumer.DTO;
using Consumer.Messages;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.DA
{
    public class DbHandler
    {
        private readonly string _connectionString = "Data Source=DESKTOP-M9TU62F;Initial Catalog = PrivatDb; Integrated Security = True";
        private readonly SqlConnection _connection;
        public DbHandler()
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SqlException happened" + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception happened" + ex.Message);
                throw ex;
            }

        }
        public async Task<ExecutionRes> GetById_spAsync(int id)
        {
            var data = await _connection.QueryAsync<Response_GetByIdDTO>("spGetById_Request",
                                       new { Id = id },
                                       commandType: CommandType.StoredProcedure);
            return data.Any()
                ? ExecutionRes<Response_GetByIdDTO>.CreateSuccessResult(data.First())
                : ExecutionRes.CreateErrorResult($"Didnt found request by id = {id}");
        }
        public async Task<ExecutionRes> SaveRequest_spAsync(string clientId, string currency, string status, decimal amount, string departmentAddress)
        {
           
            var data = await _connection.QueryAsync<Response_InsertRequestDTO>("spINSERT_Request",
                                       new
                                       {
                                           ClientId = clientId,
                                           Currency = currency,
                                           Status = status,
                                           Amount = amount,
                                           DepartmentAddress = departmentAddress
                                       },
                                     

                                           commandType: CommandType.StoredProcedure) ;
           
            return data.Any()
                ? ExecutionRes<Response_InsertRequestDTO>.CreateSuccessResult(data.First())
                : ExecutionRes.CreateErrorResult($"");

        }
        public void Close() => _connection.Close();
    }
}
