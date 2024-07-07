﻿using Moq;
using Moq.Protected;
using PinguApps.Appwrite.Client.Handlers;
using PinguApps.Appwrite.Shared.Tests;

namespace PinguApps.Appwrite.Client.Tests.Handlers;
public class HeaderHandlerTests
{
    [Fact]
    public async Task SendAsync_AddsRequiredHeaders()
    {
        // Arrange
        var mockInnerHandler = new Mock<HttpMessageHandler>();
        mockInnerHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage())
            .Verifiable();

        var headerHandler = new HeaderHandler(Constants.ProjectId)
        {
            InnerHandler = mockInnerHandler.Object
        };
        var httpClient = new HttpClient(headerHandler);

        // Act
        await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://test.com"));

        // Assert
        mockInnerHandler.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Headers.Contains("x-appwrite-project") &&
                req.Headers.GetValues("x-appwrite-project").Contains(Constants.ProjectId)),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}
