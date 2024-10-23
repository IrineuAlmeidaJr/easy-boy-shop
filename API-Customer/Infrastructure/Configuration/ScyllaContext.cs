﻿using Cassandra;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration;

public class ScyllaContext : IScyllaContext
{
    private readonly ISession _session;

    public ISession GetSession() => _session;

    public ScyllaContext(IConfiguration configuration)
    {
        var contactPoints = configuration
            .GetSection("Scylla:ContactPoints")
            .Get<string[]>();

        var port = configuration.GetValue<int>("Scylla:Port");

        var keyspace = configuration.GetValue<string>("Scylla:Keyspace");

        var cluster = Cluster.Builder()
            .AddContactPoints(contactPoints)
            .WithPort(port)
            .WithDefaultKeyspace(keyspace)
            .Build();

        _session = cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();

        CreateTables();
    }

    private async void CreateTables()
    {
        var createPacingQuery = @"CREATE TABLE IF NOT EXISTS customer (
                            id UUID PRIMARY KEY,
                            created_at TIMESTAMP,
                            updated_at TIMESTAMP,
                            name VARCHAR,
                            email VARCHAR,
                            cpf VARCHAR,
                            bith_date TIMESTAMP,
                            register_date TIMESTAMP
                            )";

        await _session.ExecuteAsync(new SimpleStatement(createPacingQuery));
    }

}