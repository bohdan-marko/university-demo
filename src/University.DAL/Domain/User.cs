﻿using University.DAL.Domain.Base;

namespace University.DAL.Domain;

public class User : BaseEntity
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}
