using System;
using System.Collections.Generic;
using System.Linq;
using Cwiczenia_2.Model.Users;

namespace Cwiczenia_2.Service;

public class UserServ
{
    private readonly List<User> _users = new();

    public void AddUser(User user) => _users.Add(user);
    public List<User> GetAllUsers() => _users;
    public User? GetUserById(Guid id) => _users.FirstOrDefault(u => u.Id == id);
}