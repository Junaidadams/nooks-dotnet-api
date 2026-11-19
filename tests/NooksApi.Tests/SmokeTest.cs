using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NooksApi.Tests;

public class SmokeTest : IClassFixture<WebApplicationFactory<Program>>
{
    static SmokeTest()
    {
        // Skip EF Core migrations in tests — no live DB available
        Environment.SetEnvironmentVariable("NOOKS_SKIP_MIGRATIONS", "true");
    }

    private readonly HttpClient _client;

    public SmokeTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthLiveReturnsOk()
    {
        var response = await _client.GetAsync("/health/live");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
