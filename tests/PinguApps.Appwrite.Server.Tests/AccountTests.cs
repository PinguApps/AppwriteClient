﻿using System.Net;
using Microsoft.Extensions.DependencyInjection;
using PinguApps.Appwrite.Server.Servers;
using PinguApps.Appwrite.Shared.Requests;
using PinguApps.Appwrite.Tests.Shared;
using Refit;
using RichardSzalay.MockHttp;

namespace PinguApps.Appwrite.Server.Tests;
public class AccountTests
{
    private readonly MockHttpMessageHandler _mockHttp;
    private readonly IAppwriteServer _appwriteServer;

    public AccountTests()
    {
        _mockHttp = new MockHttpMessageHandler();
        var services = new ServiceCollection();

        services.AddAppwriteServer(Constants.ProjectId, Constants.ApiKey, Constants.Endpoint, new RefitSettings
        {
            HttpMessageHandlerFactory = () => _mockHttp
        });

        var serviceProvider = services.BuildServiceProvider();

        _appwriteServer = serviceProvider.GetRequiredService<IAppwriteServer>();
    }

    [Fact]
    public async Task Create_ShouldReturnSuccess_WhenApiCallSucceeds()
    {
        // Arrange
        var request = new CreateAccountRequest()
        {
            Email = "email@example.com",
            Password = "password",
            Name = "name"
        };

        _mockHttp.Expect(HttpMethod.Post, $"{Constants.Endpoint}/account")
            .ExpectedHeaders()
            .WithJsonContent(request)
            .Respond(Constants.AppJson, Constants.UserResponse);

        // Act
        var result = await _appwriteServer.Account.Create(request);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task Create_ShouldHandleException_WhenApiCallFails()
    {
        // Arrange
        var request = new CreateAccountRequest()
        {
            Email = "email@example.com",
            Password = "password",
            Name = "name"
        };

        _mockHttp.Expect(HttpMethod.Post, $"{Constants.Endpoint}/account")
            .ExpectedHeaders()
            .WithJsonContent(request)
            .Respond(HttpStatusCode.BadRequest, Constants.AppJson, Constants.AppwriteError);

        // Act
        var result = await _appwriteServer.Account.Create(request);

        // Assert
        Assert.True(result.IsError);
        Assert.True(result.IsAppwriteError);
    }
}

public static class AccountTestsExtensions
{
    public static MockedRequest ExpectedHeaders(this MockedRequest request)
    {
        return request
            .WithHeaders("x-appwrite-project", Constants.ProjectId)
            .WithHeaders("x-appwrite-key", Constants.ApiKey)
            .WithHeaders("x-sdk-name", Constants.SdkName)
            .WithHeaders("x-sdk-platform", "server")
            .WithHeaders("x-sdk-language", Constants.SdkLanguage)
            .WithHeaders("x-sdk-version", Constants.SdkVersion)
            .WithHeaders("x-appwrite-response-format", Constants.AppwriteResponseFormat);
    }
}
