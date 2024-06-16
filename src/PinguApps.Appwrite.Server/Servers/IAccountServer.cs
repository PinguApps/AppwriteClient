﻿using System.Threading.Tasks;
using PinguApps.Appwrite.Shared;
using PinguApps.Appwrite.Shared.Responses;

namespace PinguApps.Appwrite.Server.Servers;
public interface IAccountServer
{
    Task<AppwriteResult<User>> Get(string? session);
}