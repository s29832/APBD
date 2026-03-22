using System;
using System.Collections.Generic;
using System.Linq;
using Cwiczenia_2.Model.Users;

namespace Cwiczenia_2.Service;

public class UserServ
{
    private readonly List<User> users = new();

    public void AddUser(User user) => users.Add(user);
    public List<User> GetAllUsers() => users;
    public User? GetUserById(string id) => users.FirstOrDefault(u => u.Id.ToString().StartsWith(id));
}