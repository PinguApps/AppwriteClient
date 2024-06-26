﻿using Microsoft.Extensions.Configuration;
using PinguApps.Appwrite.Client;
using PinguApps.Appwrite.Server.Servers;

namespace PinguApps.Appwrite.Playground;
internal class App
{
    private readonly IAppwriteClient _client;
    private readonly IAppwriteServer _server;
    private readonly string? _session;

    public App(IAppwriteClient client, IAppwriteServer server, IConfiguration config)
    {
        _client = client;
        _server = server;
        _session = config.GetValue<string>("Session");
    }

    public async Task Run(string[] args)
    {
        _client.SetSession(_session);

        var result = await _client.Account.Get();

        result.Result.Switch(
            account => Console.WriteLine(account.Email),
            appwriteError => Console.WriteLine(appwriteError.Message),
            internalError => Console.WriteLine(internalError.Message)
        );

        //var request = new CreateAccountRequest
        //{
        //    Email = "test2@example.com",
        //    Password = "ThisIsMyPassword",
        //    Name = "Two Names"
        //};

        //var result = await _server.Account.Create(request);
    }
}
