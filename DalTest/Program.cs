using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal;
using System.Data.Entity;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Dto.Utils;
using MySocNet.Bll.Services;
using AutoMapper;
using System.Diagnostics;
using MySocNet.Dal.Entities;

namespace DalTest
{
    enum Roles
    {
        Moderator,
        User
    }

    class Program
    {
        static void Main(string[] args)
        {
            SHA256SecurityProvider securityProvider = new SHA256SecurityProvider();

            using (MySocNetContext dbContext = new MySocNetContext())
            {
                var allUsers = dbContext.Users.ToList();

                foreach (var u in allUsers)
                {
                    u.PasswordSalt = securityProvider.GenerateSalt(128 / 8);
                    u.PasswordHash = securityProvider.ComputeHash("qwerty", u.PasswordSalt);
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();
        }
    }
}
