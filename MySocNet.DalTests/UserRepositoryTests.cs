using NUnit.Framework;
using MySocNet.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Tests
{
    [TestFixture()]
    public class UserRepositoryTests
    {
        public static UserRepository UserRepository;

        static UserRepositoryTests()
        {
            UserRepository = new UserRepository(Utils.MockContextProvider.MockContext);
        }

        [Test()]
        public void GetAllNonSubscriptionsOfMatchingTest()
        {
            var expectedIds = new List<int>() { 2, 3, 5, 6, 9 };
            var user = new User() { Id = 1 };

            var filter1 = new UserFilter() { AboutSelf = "hardcore" }; //2 6
            var filter2 = new UserFilter() { AgeFrom = 25 };           //5 6 9
            var filter3 = new UserFilter() { IsMale = false };         //6
            var filter4 = new UserFilter() { FullName = "An" };        //3 6 9
            var filter5 = new UserFilter() { FullName = "S M" };        //2

            var actualIds0 = UserRepository.GetAllNonSubscriptionsOfMatching(user, new UserFilter()).Select(u => u.Id).ToArray();
            var actualIds1 = UserRepository.GetAllNonSubscriptionsOfMatching(user, filter1).Select(u => u.Id).ToArray();
            var actualIds2 = UserRepository.GetAllNonSubscriptionsOfMatching(user, filter2).Select(u => u.Id).ToArray();
            var actualIds3 = UserRepository.GetAllNonSubscriptionsOfMatching(user, filter3).Select(u => u.Id).ToArray();
            var actualIds4 = UserRepository.GetAllNonSubscriptionsOfMatching(user, filter4).Select(u => u.Id).ToArray();
            var actualIds5 = UserRepository.GetAllNonSubscriptionsOfMatching(user, filter5).Select(u => u.Id).ToArray();


            Assert.Multiple(() =>
            {
                CollectionAssert.AreEquivalent(expectedIds, actualIds0);
                CollectionAssert.AreEquivalent(new int[] { 2, 6 }, actualIds1);
                CollectionAssert.AreEquivalent(new int[] { 5, 6, 9 }, actualIds2);
                CollectionAssert.AreEquivalent(new int[] { 6 }, actualIds3);
                CollectionAssert.AreEquivalent(new int[] { 3, 6, 9 }, actualIds4);
                CollectionAssert.AreEquivalent(new int[] { 2 }, actualIds5);
            });
        }

        [Test()]
        public void GetTopLastSubscribersOfTest()
        {
            int[] expectedIds = new int[] { 7, 5 };
            int[] actual = UserRepository.GetTopLastSubscribersOf(new User() { Id = 1 }, 2).Select(u => u.Id).ToArray();

            CollectionAssert.AreEquivalent(expectedIds, actual);
        }
    }
}