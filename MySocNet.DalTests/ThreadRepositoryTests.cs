using NUnit.Framework;
using Rhino.Mocks;
using MySocNet.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Tests
{
    [TestFixture()]
    public class ThreadRepositoryTests
    {
        static ThreadRepository ThreadRepository;

        static ThreadRepositoryTests()
        {
            ThreadRepository = new ThreadRepository(Utils.MockContextProvider.MockContext);
        }

        [Test()]
        public void GetSubscribersCount()
        {
            int expected = 4;
            ConvThread thread = new ConvThread() { Id = 2 };

            int actual = ThreadRepository.GetSubscribersCount(thread);

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void GetAllTopicsTest()
        {
            List<string> expected = new List<string>() { "Music", "Sport" };

            List<string> actual = ThreadRepository.GetAllTopics();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test()]
        public void GetAllTopicsStartingWithTest()
        {
            List<string> expected = new List<string>() { "Music" };

            List<string> actual = ThreadRepository.GetAllTopicsStartingWith("M");

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test()]
        public void GetThreadsWithSubscribersCountByModeratorTest()
        {
            User moderator = new User() { Id = 1 };
            var expected = new List<KeyValuePair<int, int>>()
            {
                //         thread id, subscribers count
                new KeyValuePair<int, int>(1,1),
                new KeyValuePair<int, int>(2,4)
            };

            var actual = ThreadRepository.GetThreadsWithSubscribersCountByModerator(moderator)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test()]
        public void GetThreadsWithSubscribersCountBySubscriberTest()
        {
            User subscriber = new User() { Id = 2 };
            var expected = new List<KeyValuePair<int, int>>()
            {
                //         thread id, subscribers count
                new KeyValuePair<int, int>(2,4), //Boston hc is; Music; It's Boston...
                new KeyValuePair<int, int>(3,1), //u2 Default thread
                new KeyValuePair<int, int>(5,5)  //Skateboarding; Sport; It's more than sport
            };

            var actual = ThreadRepository.GetThreadsWithSubscribersCountBySubscriber(subscriber)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test()]
        public void GetThreadsWithSubscribersCountBySubscriberMatchingTest()
        {
            User subscriber = new User() { Id = 2 };
            var expected = new List<KeyValuePair<int, int>>()
            {
                //         thread id, subscribers count
                new KeyValuePair<int, int>(2,4), //0 Boston hc is; Music; It's Boston...
                new KeyValuePair<int, int>(3,1), //1 u2 Default thread
                new KeyValuePair<int, int>(5,5)  //2 Skateboarding; Sport; It's more than sport
            };

            var filter1 = new ThreadFilter() { Description = "It's" };              // 0 2
            var filter2 = new ThreadFilter() { Name = "Boston" };                   // 0
            var filter3 = new ThreadFilter() { Topic = "Sp" };                      // 2
            var filter4 = new ThreadFilter() { Description = "It's", Topic = "Mu" };// 0

            var actual1 = ThreadRepository.GetThreadsWithSubscribersCountBySubscriberMatching(subscriber, filter1)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            var actual2 = ThreadRepository.GetThreadsWithSubscribersCountBySubscriberMatching(subscriber, filter2)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            var actual3 = ThreadRepository.GetThreadsWithSubscribersCountBySubscriberMatching(subscriber, filter3)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            var actual4 = ThreadRepository.GetThreadsWithSubscribersCountBySubscriberMatching(subscriber, filter4)
                .Select(kvp => new KeyValuePair<int, int>(kvp.Key.Id, kvp.Value))
                .ToList();

            Assert.Multiple(() =>
            {
                CollectionAssert.AreEquivalent(new List<KeyValuePair<int, int>>() { expected[0], expected[2] }, actual1);
                CollectionAssert.AreEquivalent(new List<KeyValuePair<int, int>>() { expected[0] }, actual2);
                CollectionAssert.AreEquivalent(new List<KeyValuePair<int, int>>() { expected[2] }, actual3);
                CollectionAssert.AreEquivalent(new List<KeyValuePair<int, int>>() { expected[0] }, actual4);
            });
        }
    }
}