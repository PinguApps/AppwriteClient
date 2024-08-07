﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PinguApps.Appwrite.Shared.Requests;
using PinguApps.Appwrite.Shared.Responses;
using Refit;

namespace PinguApps.Appwrite.Client.Internals;

internal interface IAccountApi : IBaseApi
{
    [Get("/account")]
    Task<IApiResponse<User>> GetAccount([Header("x-appwrite-session")] string? session);

    [Post("/account")]
    Task<IApiResponse<User>> CreateAccount(CreateAccountRequest request);

    [Patch("/account/email")]
    Task<IApiResponse<User>> UpdateEmail([Header("x-appwrite-session")] string? session, UpdateEmailRequest request);

    [Patch("/account/name")]
    Task<IApiResponse<User>> UpdateName([Header("x-appwrite-session")] string? session, UpdateNameRequest request);

    [Patch("/account/password")]
    Task<IApiResponse<User>> UpdatePassword([Header("x-appwrite-session")] string? session, UpdatePasswordRequest request);

    [Patch("/account/phone")]
    Task<IApiResponse<User>> UpdatePhone([Header("x-appwrite-session")] string? session, UpdatePhoneRequest request);

    [Get("/account/prefs")]
    Task<IApiResponse<IReadOnlyDictionary<string, string>>> GetAccountPreferences([Header("x-appwrite-session")] string? session);

    [Patch("/account/prefs")]
    Task<IApiResponse<User>> UpdatePreferences([Header("x-appwrite-session")] string? session, UpdatePreferencesRequest request);

    [Post("/account/tokens/email")]
    Task<IApiResponse<Token>> CreateEmailToken(CreateEmailTokenRequest request);

    [Post("/account/sessions/token")]
    Task<IApiResponse<Session>> CreateSession(CreateSessionRequest request);

    [Get("/account/sessions/{sessionId}")]
    Task<IApiResponse<Session>> GetSession([Header("x-appwrite-session")] string? session, string sessionId);

    [Patch("/account/sessions/{sessionId}")]
    Task<IApiResponse<Session>> UpdateSession([Header("x-appwrite-session")] string? session, string sessionId);

    [Post("/account/verification")]
    Task<IApiResponse<Token>> CreateEmailVerification([Header("x-appwrite-session")] string? session, CreateEmailVerificationRequest request);

    [Put("/account/verification")]
    Task<IApiResponse<Token>> CreateEmailVerificationConfirmation(CreateEmailVerificationConfirmationRequest request);

    [Post("/account/jwt")]
    Task<IApiResponse<Jwt>> CreateJwt([Header("x-appwrite-session")] string? session);

    [Get("/account/logs")]
    [QueryUriFormat(System.UriFormat.Unescaped)]
    Task<IApiResponse<LogsList>> ListLogs([Header("x-appwrite-session")] string? session, [Query(CollectionFormat.Multi), AliasAs("queries[]")] IEnumerable<string> queries);

    [Post("/account/mfa/authenticators/{type}")]
    Task<IApiResponse<MfaType>> AddAuthenticator([Header("x-appwrite-session")] string? session, string type);

    [Put("/account/mfa/authenticators/{type}")]
    Task<IApiResponse<User>> VerifyAuthenticator([Header("x-appwrite-session")] string? session, string type, VerifyAuthenticatorRequest request);

    [Patch("/account/mfa")]
    Task<IApiResponse<User>> UpdateMfa([Header("x-appwrite-session")] string? session, UpdateMfaRequest request);

    [Delete("/account/mfa/authenticators/{type}")]
    Task<IApiResponse> DeleteAuthenticator([Header("x-appwrite-session")] string? session, string type, [Body] DeleteAuthenticatorRequest request);
}
