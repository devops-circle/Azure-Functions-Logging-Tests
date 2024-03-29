﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Func.InProcess.Net6.With.AI;
public interface IUserDataService
{
    Task<List<User>> GetUsersAsync();
}

public class UserDataService : IUserDataService
{
    private readonly ILogger<UserDataService> _logger;

    public UserDataService(ILogger<UserDataService> logger)
    {
        _logger = logger;
    }

    public Task<List<User>> GetUsersAsync()
    {
        _logger.LogTrace("GetUsersAsync called at: " + DateTime.Now.ToLongTimeString());
        List<User> users = new List<User>
        {
            new User() { Id = "1", Name = "John", Age = 41 },
            new User() { Id = "2", Name = "Jane", Age = 35 },
            new User() { Id = "3", Name = "Piet", Age = 29 }
        };

        // Sleep so we mock database querying...
        Thread.Sleep(1000);

        _logger.LogTrace("GetUsersAsync finished at: " + DateTime.Now.ToLongTimeString());

        return Task.FromResult(users);
    }
}

public class User
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Age { get; set; }
}