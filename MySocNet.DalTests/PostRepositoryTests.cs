using NUnit.Framework;
using MySocNet.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal;

namespace MySocNet.Dal.Tests
{
    [TestFixture()]
    public class PostRepositoryTests
    {
        static PostRepository PostRepository;

        static PostRepositoryTests()
        {
            PostRepository = new PostRepository(Utils.MockContextProvider.MockContext);
        }

        [Test()]
        public void GetByAuthorsTest()
        {
            var authors = new List<User>() { new User() { Id = 1 }, new User() { Id = 2 } };
            var expectedPostIds = new int[] { 1, 2, 3, 4, 5, 8 };

            var actualIds = PostRepository.GetByAuthors(authors).Select(p => p.Id).ToArray();

            CollectionAssert.AreEquivalent(expectedPostIds, actualIds);
        }

        [Test()]
        public void GetByAuthorsOrThreadsTest()
        {
            var authors = new List<User>() { new User() { Id = 1 }, new User() { Id = 2 } };
            var threads = new ConvThread[] { new ConvThread() { Id = 1 }, new ConvThread() { Id = 2 } };
            var expectedPostIds = new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 15 };

            var actualIds = PostRepository.GetByAuthorsOrThreads(authors, threads).Select(p => p.Id).ToArray();

            CollectionAssert.AreEquivalent(expectedPostIds, actualIds);
        }

        [Test()]
        public void GetByThreadsTest()
        {
            var threads = new ConvThread[] { new ConvThread() { Id = 1 }, new ConvThread() { Id = 3 } };
            var expectedPostIds = new int[] { 1, 4, 6, 9 };

            var actualIds = PostRepository.GetByThreads(threads).Select(p => p.Id).ToArray();

            CollectionAssert.AreEquivalent(expectedPostIds, actualIds);
        }
    }
}