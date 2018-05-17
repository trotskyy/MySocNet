using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal;
using MySocNet.Dal.Entities;
using Rhino.Mocks;
using System.Data.Entity;

namespace MySocNet.Dal.Tests.Utils
{
    internal static class MyInsatnceExtansions
    {
        static int _threadCounter = 0;
        public static ConvThread CreateThread(this User u, string Name, string Description, string Topic)
        {
            var t = new ConvThread()
            {
                Id = ++_threadCounter,
                Description = Description,
                Moderator = u,
                ModeratorId = u.Id,
                Name = Name,
                Topic = Topic
            };
            t.Subscribers = new List<User>() { u };
            return t;
        }

        public static void SubscribeAt(this User u, ConvThread thread)
        {
            thread.Subscribers.Add(u);
            if (u.ThreadSubscriptions is null)
                u.ThreadSubscriptions = new List<ConvThread>();
            u.ThreadSubscriptions.Add(thread);
        }

        static int _userRelationsCounter = 0;
        public static UsersRelation SubscribeAt(this User u, User u2)
        {
            if (u.Subscriptions is null)
                u.Subscriptions = new List<UsersRelation>();
            if (u2.Subscribers is null)
                u2.Subscribers = new List<UsersRelation>();
            UsersRelation ur = new UsersRelation()
            {
                Id = ++_userRelationsCounter,
                Publisher = u2,
                PublisherId = u2.Id,
                Subscriber = u,
                SubscriberId = u.Id
            };
            u.Subscriptions.Add(ur);
            u2.Subscribers.Add(ur);
            return ur;
        }

        static int _postCounter = 0;
        static Random random;
        static DateTime GetRandomDateTime()
        {
            if (random is null)
                random = new Random();
            return new DateTime(random.Next(2015, 2018), random.Next(1, 12), random.Next(1, 25));
        }
        public static Post WritePost(this User u, ConvThread thread, string Text, DateTime Published = new DateTime())
        {
            return new Post()
            {
                Id = ++_postCounter,
                Author = u,
                AuthorId = u.Id,
                Published = Published == DateTime.MinValue ? GetRandomDateTime() : Published,
                Text = Text,
                Thread = thread,
                ThreadId = thread.Id
            };
        }

        static int _messageCounter = 0;
        public static Message WriteMessage(this User u, User to, string text, bool isRead = false)
        {
            return new Message()
            {
                Id = ++_messageCounter,
                From = u,
                FromId = u.Id,
                IsRead = isRead,
                Sent = GetRandomDateTime(),
                To = to,
                ToId = to.Id,
                Text = text
            };
        }
    }

    public static class MockContextProvider
    {
        static MySocNetContext _mockContext;

        static MockContextProvider()
        {
            //all Threads, Messages, Posts, Users and UserRelations
            IQueryable<User> users;
            IQueryable<ConvThread> threads;
            IQueryable<Message> messages;
            IQueryable<Post> posts;
            IQueryable<UsersRelation> userRelations;

            User u1 = new User()
            {
                Id = 1,
                FirstName = "Dave",
                LastName = "Milligun",
                IsMale = true,
                Login = "Dave",
                PasswordHash = "Dave666",
                AboutSelf = "hardcore guy",
                CityOfBirth = "Boston",
                StateOfBirth = "USA",
                CurrentCity = "Boston",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1995, 03, 08)
            };
            var u1Thread = u1.CreateThread("u1 Default Thread", "", "");
            var post01 = u1.WritePost(u1Thread, $"{u1.FirstName} {u1.LastName} joined MySocNet!");
            var hcThread = u1.CreateThread("Boston hardcore is..", "It's about Boston. Not LA", "Music");
            var post1 = u1.WritePost(hcThread, "Hi guyz! Wellcome to me thread!", new DateTime(2015, 01, 01, 15, 00, 00));
            var post2 = u1.WritePost(hcThread, "Now, I'm going to tell you about Freeze band...", new DateTime(2015, 01, 01, 15, 10, 00));
            User u2 = new User()
            {
                Id = 2,
                FirstName = "Steve",
                LastName = "Milligun",
                IsMale = true,
                Login = "Steve",
                PasswordHash  = "Steve777",
                AboutSelf = "hardcore guy's brother",
                CityOfBirth = "Boston",
                StateOfBirth = "USA",
                CurrentCity = "Boston",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1993, 07, 11)
            };
            var u2Thread = u2.CreateThread("u2 Default Thread", "", "");
            var post02 = u2.WritePost(u1Thread, $"{u2.FirstName} {u2.LastName} joined MySocNet!");
            var ur1 = u2.SubscribeAt(u1);
            var ur2 = u1.SubscribeAt(u2);
            u2.SubscribeAt(hcThread);
            var post3 = u2.WritePost(hcThread, "Sup, thread! Better listen to Void band!", new DateTime(2015, 01, 06, 12, 13, 00));
            var msg1 = u2.WriteMessage(u1, "Hi, bro! Let's go to pub!", true);
            var msg2 = u1.WriteMessage(u2, "Hi! I'm running downstairs now!!!", true);
            User u3 = new User()
            {
                Id = 3,
                FirstName = "Andrew",
                LastName = "Bucket",
                IsMale = true,
                Login = "ABucket",
                PasswordHash  = "qwerty",
                AboutSelf = "like surf rock and skateboarding",
                CityOfBirth = "Omaha",
                StateOfBirth = "USA",
                CurrentCity = "California",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1995, 12, 21)
            };
            var u3Thread = u3.CreateThread("u3 Default Thread", "", "");
            var post03 = u3.WritePost(u1Thread, $"{u3.FirstName} {u3.LastName} joined MySocNet!");
            var skateboardingThread = u3.CreateThread("Skateboarding", "It's more than sport", "Sport");
            var post4 = u3.WritePost(skateboardingThread, "Hi! Wellcome to skate thread!", new DateTime(2016, 10, 11));
            var post5 = u2.WritePost(skateboardingThread, "Wow! So glad to see skateboarding thread here! I'm subscribing now", new DateTime(2016, 10, 12, 11, 22, 00));
            u2.SubscribeAt(skateboardingThread);
            User u4 = new User()
            {
                Id = 4,
                FirstName = "Omar",
                LastName = "Al Naseredi",
                IsMale = true,
                Login = "OmarInShallah",
                PasswordHash  = "qwerty",
                AboutSelf = "found of Omar Souleyman and Jeffrey Richter",
                CityOfBirth = "Al Selooha",
                StateOfBirth = "Egypt",
                CurrentCity = "California",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1971, 07, 08)
            };
            var u4Thread = u4.CreateThread("u4 Default Thread", "", "");
            var post04 = u4.WritePost(u1Thread, $"{u4.FirstName} {u4.LastName} joined MySocNet!");
            var electroThread = u4.CreateThread("Electro music", "Dance!", "Music");
            var post6 = u4.WritePost(electroThread, "Omar Souleyman is a great electro...", new DateTime(2016, 01, 02));
            User u5 = new User()
            {
                Id = 5,
                FirstName = "Иван",
                LastName = "Ченкин",
                IsMale = true,
                Login = "Ivan",
                PasswordHash  = "qwerty",
                AboutSelf = "like russian shanson and vodka",
                CityOfBirth = "Москва",
                StateOfBirth = "Russia",
                CurrentCity = "California",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1985, 08, 08)
            };
            var u5Thread = u5.CreateThread("u5 Default Thread", "", "");
            var post05 = u5.WritePost(u5Thread, $"{u5.FirstName} {u5.LastName} joined MySocNet!");
            u5.SubscribeAt(electroThread);
            var ur3 = u5.SubscribeAt(u1);
            var msg3 = u5.WriteMessage(u1, "Привет! Как дела?", true);
            var msg4 = u1.WriteMessage(u5, "What?");
            var msg5 = u1.WriteMessage(u5, "I don't speak russian");
            u5.SubscribeAt(skateboardingThread);
            var post7 = u5.WritePost(skateboardingThread, "Listen to Koroziya Metalla! \\m/", new DateTime(2016, 01, 02));
            User u6 = new User()
            {
                Id = 6,
                FirstName = "Ann",
                LastName = "Minaeva",
                IsMale = false,
                Login = "Mina",
                PasswordHash  = "qwerty",
                AboutSelf = "crazy about crust-punk, new school hardcore and post-metal",
                CityOfBirth = "Kiev",
                StateOfBirth = "Ukraine",
                CurrentCity = "New York",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1989, 02, 03)
            };
            var ur4 = u6.SubscribeAt(u5);
            var ur6 = u5.SubscribeAt(u6);
            var msg6 = u5.WriteMessage(u6, "Darova! Vy booryat? Kak ya rad", true);
            var msg7 = u6.WriteMessage(u5, "Bolnoy ooblyudok!");
            var msg8 = u6.WriteMessage(u1, "Hello! Let's be friends!");
            var u6Thread = u6.CreateThread("u6 Default Thread", "", "");
            var post06 = u6.WritePost(u6Thread, $"{u6.FirstName} {u6.LastName} joined MySocNet!");
            u6.SubscribeAt(skateboardingThread);
            User u7 = new User()
            {
                Id = 7,
                FirstName = "Alex",
                LastName = "Maroon",
                IsMale = false,
                Login = "marr_a",
                PasswordHash  = "qwerty34",
                AboutSelf = "turbo mosher",
                CityOfBirth = "Kiev",
                StateOfBirth = "Ukraine",
                CurrentCity = "New York",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1989, 02, 03)
            };
            var u7Thread = u7.CreateThread("u7 Default Thread", "", "");
            var post07 = u7.WritePost(u7Thread, $"{u7.FirstName} {u7.LastName} joined MySocNet!");
            var ur7 = u7.SubscribeAt(u1);
            var ur8 = u1.SubscribeAt(u7);
            var ur9 = u7.SubscribeAt(u2);
            u6.SubscribeAt(hcThread);
            var post8 = u7.WritePost(hcThread, "MOOOOOOOOOOOSH!", new DateTime(2016, 10, 15));
            User u8 = new User()
            {
                Id = 8,
                FirstName = "George",
                LastName = "Miller",
                Login = "mil",
                PasswordHash  = "qwerty34",
                AboutSelf = "came to USA, 'cause it's a cradle of jazz, blues 'n' soul",
                CurrentCity = "New Orleans",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1924, 02, 03)
            };
            var u8Thread = u8.CreateThread("u8 Default Thread", "", "");
            var post08 = u8.WritePost(u8Thread, $"{u8.FirstName} {u8.LastName} joined MySocNet!");
            var oldMuzThread = u8.CreateThread("Blues, Jazz 'n'n Soul", "Old music is cool", "Music");
            var post9 = u8.WritePost(oldMuzThread, "Bluegrass is genre of music which comes from...", new DateTime(2015, 10, 11));
            var ur10 = u8.SubscribeAt(u3);
            var msg9 = u8.WriteMessage(u3, "MOOOOOOOOOOSH", false);
            User u9 = new User()
            {
                Id = 9,
                FirstName = "Antonio",
                LastName = "Pizza",
                IsMale = true,
                Login = "benitto666",
                PasswordHash  = "qwerty34",
                AboutSelf = "i like pizza and thrash metal",
                CityOfBirth = "Genoia",
                StateOfBirth = "Italy",
                CurrentCity = "LA",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1989, 02, 03)
            };
            var u9Thread = u9.CreateThread("u9 Default Thread", "", "");
            var post09 = u9.WritePost(u9Thread, $"{u9.FirstName} {u9.LastName} joined MySocNet!");
            u9.SubscribeAt(skateboardingThread);
            var post10 = u9.WritePost(skateboardingThread, "Tony Hawk is my brother!", new DateTime(2017, 09, 01));
            u9.SubscribeAt(hcThread);
            var ur11 = u9.SubscribeAt(u5);
            var ur12 = u9.SubscribeAt(u6);
            var ur13 = u7.SubscribeAt(u9);
            User u10 = new User()
            {
                Id = 10,
                FirstName = "Kortney",
                LastName = "Stza",
                IsMale = false,
                Login = "ilovefuckingcrust",
                PasswordHash  = "qwerty34",
                AboutSelf = "i like crack-rocksteady and just crack, i don't like taking shower",
                CityOfBirth = "Aidaho",
                StateOfBirth = "USA",
                CurrentCity = "LA",
                CurrentState = "USA",
                DateOfBirth = new DateTime(1999, 05, 25)
            };
            var u10Thread = u10.CreateThread("u10 Default Thread", "", "");
            var post010 = u10.WritePost(u10Thread, $"{u10.FirstName} {u10.LastName} joined MySocNet!");
            var post11 = u10.WritePost(u10Thread, "I'm drunk good nigth", new DateTime(2017, 10, 11));
            u10.SubscribeAt(oldMuzThread);
            u3.SubscribeAt(oldMuzThread);



            //all Threads, Messages, Posts, Users and UserRelations
            users = new List<User>() { u1, u2, u3, u4, u5, u6, u7, u8, u9, u10 }.AsQueryable();
            messages = new List<Message>() { msg1, msg2, msg3, msg4, msg5, msg6, msg7, msg8, msg9 }.AsQueryable();
            posts = new List<Post>() { post01, post02, post03, post04, post05, post06, post07, post08, post09, post10, post1, post2, post3, post4, post5, post6, post7, post8, post9 }.AsQueryable();
            threads = new List<ConvThread>() { u1Thread, u2Thread, u3Thread, u4Thread, u5Thread, u6Thread, u7Thread, u8Thread, u9Thread, u10Thread, electroThread, hcThread, oldMuzThread, skateboardingThread }.AsQueryable();
            userRelations = new List<UsersRelation>() { ur1, ur2, ur3, ur4, ur6, ur7, ur8, ur9, ur10, ur11, ur12, ur13 }.AsQueryable();

            // setting stubs for IDbSets

            IDbSet<User> usersMockSet = MockRepository.GenerateMock<IDbSet<User>, IQueryable>();
            IDbSet<Message> messagesMockSet = MockRepository.GenerateMock<IDbSet<Message>, IQueryable>();
            IDbSet<Post> postsMockSet = MockRepository.GenerateMock<IDbSet<Post>, IQueryable>();
            IDbSet<ConvThread> threadsMockSet = MockRepository.GenerateMock<IDbSet<ConvThread>, IQueryable>();
            IDbSet<UsersRelation> userRelationsMockSet = MockRepository.GenerateMock<IDbSet<UsersRelation>, IQueryable>();

            usersMockSet.Stub(m => m.Provider).Return(users.Provider);
            usersMockSet.Stub(m => m.Expression).Return(users.Expression);
            usersMockSet.Stub(m => m.ElementType).Return(users.ElementType);
            usersMockSet.Stub(m => m.GetEnumerator()).Return(users.GetEnumerator());

            messagesMockSet.Stub(m => m.Provider).Return(messages.Provider);
            messagesMockSet.Stub(m => m.Expression).Return(messages.Expression);
            messagesMockSet.Stub(m => m.ElementType).Return(messages.ElementType);
            messagesMockSet.Stub(m => m.GetEnumerator()).Return(messages.GetEnumerator());

            postsMockSet.Stub(m => m.Provider).Return(posts.Provider);
            postsMockSet.Stub(m => m.Expression).Return(posts.Expression);
            postsMockSet.Stub(m => m.ElementType).Return(posts.ElementType);
            postsMockSet.Stub(m => m.GetEnumerator()).Return(posts.GetEnumerator());

            threadsMockSet.Stub(m => m.Provider).Return(threads.Provider);
            threadsMockSet.Stub(m => m.Expression).Return(threads.Expression);
            threadsMockSet.Stub(m => m.ElementType).Return(threads.ElementType);
            threadsMockSet.Stub(m => m.GetEnumerator()).Return(threads.GetEnumerator());

            userRelationsMockSet.Stub(m => m.Provider).Return(userRelations.Provider);
            userRelationsMockSet.Stub(m => m.Expression).Return(userRelations.Expression);
            userRelationsMockSet.Stub(m => m.ElementType).Return(userRelations.ElementType);
            userRelationsMockSet.Stub(m => m.GetEnumerator()).Return(userRelations.GetEnumerator());

            // setting stubs for DbContext

            _mockContext = MockRepository.GenerateMock<MySocNetContext>();
            _mockContext.Stub(c => c.Messages).PropertyBehavior();
            _mockContext.Stub(c => c.Posts).PropertyBehavior();
            _mockContext.Stub(c => c.Threads).PropertyBehavior();
            _mockContext.Stub(c => c.UserRelations).PropertyBehavior();
            _mockContext.Stub(c => c.Users).PropertyBehavior();

            _mockContext.Messages = messagesMockSet;
            _mockContext.Posts = postsMockSet;
            _mockContext.Threads = threadsMockSet;
            _mockContext.UserRelations = userRelationsMockSet;
            _mockContext.Users = usersMockSet;
        }

        public static MySocNetContext MockContext => _mockContext;
    }
}

