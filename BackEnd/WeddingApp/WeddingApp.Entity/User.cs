﻿using System;

namespace WeddingApp.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Boolean IsAdmin { get; set; }

        public string RefreshToken { get; set; }
    }
}