﻿using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using EShop.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace EShopService.IntegrationTests.Controllers;

public class CreditCardControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    public CreditCardControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // pobranie dotychczasowej konfiguracji bazy danych
                    var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    //// usunięcie dotychczasowej konfiguracji bazy danych
                    services.Remove(dbContextOptions);

                    // Stworzenie nowej bazy danych
                    services
                        .AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDBForTest"));

                });
            });

        _client = _factory.CreateClient();
    }

    [Theory]
    [InlineData("349779658312797")]
    [InlineData("4024007165401778")]
    [InlineData("345470784783010")]
    [InlineData("4532289052809181")]
    [InlineData("5530016454538418")]
    [InlineData("5131208517986691")]
    [InlineData("3497-7965-8312-797")]
    [InlineData("4024-0071-6540-1778")]
    [InlineData("345-470-784-783-010")]
    [InlineData("3497 7965 8312 797")]
    [InlineData("4024 0071 6540 1778")]
    [InlineData("4532 2080 2150 4434")]
    public async Task Get_CheckCard_Successful(string CardNumber)
    {
        // arange
        var factory = new WebApplicationFactory<Program>();
        HttpClient client = factory.CreateClient();

        // act
        var response = await client.GetAsync($"api/CreditCard?cardNumber={CardNumber}");

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("111111111111111111111111111111111111111111")]
    [InlineData("2222222222222222222222222222222222222222222")]
    public async Task Get_CheckCard_Unsuccessful_TooLong(string CardNumber)
    {
        // arange
        var factory = new WebApplicationFactory<Program>();
        HttpClient client = factory.CreateClient();

        // act
        var response = await client.GetAsync($"api/CreditCard?cardNumber={CardNumber}");

        // assert
        Assert.Equal(HttpStatusCode.RequestUriTooLong, response.StatusCode);
    }

    [Theory]
    [InlineData("11111111")]
    [InlineData("2222222")]
    public async Task Get_CheckCard_Unsuccessful_TooShort(string CardNumber)
    {
        // arange
        var factory = new WebApplicationFactory<Program>();
        HttpClient client = factory.CreateClient();

        // act
        var response = await client.GetAsync($"api/CreditCard?cardNumber={CardNumber}");

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("111111w$$$$%$#^%&*%&(*@$@11")]
    [InlineData("222*^$%@^&^$*%&@2222")]
    public async Task Get_CheckCard_Unsuccessful_Invalid(string CardNumber)
    {
        // arange
        var factory = new WebApplicationFactory<Program>();
        HttpClient client = factory.CreateClient();

        // act
        var response = await client.GetAsync($"api/CreditCard?cardNumber={CardNumber}");

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}