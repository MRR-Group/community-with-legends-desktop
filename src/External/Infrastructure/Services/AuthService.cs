using System.Diagnostics;
using Application.Abstractions;
using Domain.ValueObjects;
using Flurl;
using Flurl.Http;

namespace Infrastructure.Services;

public class AuthService : ISignInService
{
    private Url api;

    public AuthService(Url api)
    {
        this.api = api;
    }

    public async Task SignIn(string name, Email email, Password password)
    {
        var response = await api
            .AppendPathSegment("auth")
            .AppendPathSegment("register")
            .PostJsonAsync(new { name = name, email = email, password = password });

        if (response.StatusCode != 201)
        {
            Debug.Print(response.ResponseMessage.ToString());
            throw new Exception("Failed to sign in");
        }
    }
}