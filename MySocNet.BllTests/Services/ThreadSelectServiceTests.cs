using NUnit.Framework;
using Rhino.Mocks;
using MySocNet.Bll.Services;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Dto.Utils;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Bll.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;
using MySocNet.Dal.Filters;
using MySocNet.Dal.Abstract;

namespace MySocNet.Bll.Services.Tests
{
    [TestFixture()]
    public class ThreadSelectServiceTests
    {
        List<KeyValuePair<ConvThread, int>> mockData = new List<KeyValuePair<ConvThread, int>>
        {
            new KeyValuePair<ConvThread, int>(
                new ConvThread
                {
                    Id = 1,
                    AvatarPath = "img 1",
                    Description = "Description 1",
                    ModeratorId = 2,
                    Name = "Thread 1",
                    Topic = "Topic 1"
                }, 15),
            new KeyValuePair<ConvThread, int>(
                new ConvThread
                {
                    Id = 2,
                    AvatarPath = "img 2",
                    Description = "Description 2",
                    ModeratorId = 2,
                    Name = "Thread 2",
                    Topic = "Topic 2"
                }, 16)
        };

        private bool AreEquivalent(ThreadDto threadDto, ConvThread thread)
        {
            return threadDto.Id == thread.Id &&
                threadDto.AvatarPath == thread.AvatarPath &&
                threadDto.Description == thread.Description &&
                threadDto.ModeratorId == thread.ModeratorId &&
                threadDto.Name == thread.Name &&
                threadDto.Topic == thread.Topic;
        }

        public ThreadSelectServiceTests()
        {
            AutomapperInitializer.InitAutoMapper();
        }

        [Test]
        public void ThreadsWithSubscribersCountByModeratorTest()
        {
            var stubUnitofWork = MockRepository.GenerateStub<IUnitOfWork>();
            stubUnitofWork.Stub(uow => uow.ThreadRepository.GetThreadsWithSubscribersCountByModerator(new User()))
                .IgnoreArguments()
                .Return(mockData);

            var stubUnitOfWorkFactory = MockRepository.GenerateStub<IUnitOfWorkFactory>();
            stubUnitOfWorkFactory.Stub(uowf => uowf.GetUnitOfWork())
                .Return(stubUnitofWork);

            ThreadSelectService service = new ThreadSelectService(stubUnitOfWorkFactory);

            var actual = service.ThreadsWithSubscribersCountByModerator(new UserDto { Id = 1});

            Assert.Multiple(() => {
                Assert.That(mockData.Count == actual.Count);
                for (int i = 0; i < mockData.Count; i++)
                {
                    Assert.That(AreEquivalent(actual[i].Key, mockData[i].Key));
                }
            });
        }
    }
}