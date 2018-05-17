using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    public class MySocNetContext : DbContext
    {
        static MySocNetContext()
        {
            //for Seed()
            //Database.SetInitializer(new MySocNetContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.From)
                .WithMany(u => u.MessagesFromUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.To)
                .WithMany(u => u.MessagesToUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConvThread>()
                .HasRequired(t => t.Moderator)
                .WithMany(u => u.Threads)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersRelation>()
                .HasRequired(ur => ur.Publisher)
                .WithMany(u => u.Subscribers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersRelation>()
                .HasRequired(ur => ur.Subscriber)
                .WithMany(u => u.Subscriptions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConvThread>()
                .HasMany(t => t.Subscribers)
                .WithMany(u => u.ThreadSubscriptions);

            modelBuilder.Entity<Notification>()
                .HasRequired(n => n.Moderator)
                .WithMany(m => m.SentNotification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(n => n.User)
                .WithMany(u => u.ReceivedNotfications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Message> Messages { get; set; }
        public virtual IDbSet<ConvThread> Threads { get; set; }
        public virtual IDbSet<Post> Posts { get; set; }
        public virtual IDbSet<UsersRelation> UserRelations { get; set; }
        public virtual IDbSet<Notification> Notifications { get; set; }
    }
}
