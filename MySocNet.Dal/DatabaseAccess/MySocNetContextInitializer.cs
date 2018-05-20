using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    public class MySocNetContextInitializer : DropCreateDatabaseAlways<MySocNetContext>
    {
        protected override void Seed(MySocNetContext context)
        {
            User u1 = new User() { FirstName = "u1" };
            User u2 = new User() { FirstName = "u2" };
            Message m1 = new Message() { Text = "hello", From = u1, To = u2 };
            context.Users.Add(u1);
            context.Users.Add(u2);
            context.Messages.Add(m1);
            context.SaveChanges();
        }
    }
}
