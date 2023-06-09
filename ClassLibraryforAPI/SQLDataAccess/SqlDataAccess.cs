﻿using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DemoAPIClassLibrary.SQLDataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(string storedProcedure,
                                            U parameters,
                                            string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
        return rows.ToList();
    }

    public Task SaveData<T>(string storedProcedure,
                                         T parameters,
                                 string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);

        return connection.ExecuteAsync(storedProcedure, parameters,
                                        commandType: CommandType.StoredProcedure);//ok without await on the async
                                                                                  //because it is the caller that waits for the result


    }
}

