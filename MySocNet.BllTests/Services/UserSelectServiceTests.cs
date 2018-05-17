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
    public class UserSelectServiceTests
    {
        List<User> mockData = new List<User>()
        {
            new User()
            {
                Id = 1,
                AboutSelf = "About 1",
                AvatarPath = "./img1",
                CityOfBirth = "City 1",
                CurrentCity = "Town 1",
                CurrentState = "State 1",
                DateOfBirth = DateTime.Now,
                FirstName = "FName 1",
                IsMale = true,
                LastName = "LName 1",
                Login = "Login 1",
                PasswordHash = "Password 1",
                PasswordSalt = "Salt 1",
                StateOfBirth = "State 1",
                IsModerator = false
            },
            new User()
            {
                Id = 2,
                AboutSelf = "About 2",
                AvatarPath = "./img2",
                CityOfBirth = "City 2",
                CurrentCity = "Town 2",
                CurrentState = "State 2",
                DateOfBirth = DateTime.Now,
                FirstName = "FName 2",
                IsMale = true,
                LastName = "LName 2",
                Login = "Login 2",
                PasswordHash = "Password 2",
                PasswordSalt = "Salt 2",
                StateOfBirth = "State 2",
                IsModerator = false
            },
            new User()
            {
                Id = 3,
                AboutSelf = "About 3",
                AvatarPath = "./img3",
                CityOfBirth = "City 3",
                CurrentCity = "Town 3",
                CurrentState = "State 3",
                DateOfBirth = DateTime.Now,
                FirstName = "FName 3",
                IsMale = true,
                LastName = "LName 3",
                Login = "Login 3",
                PasswordHash = "Password 3",
                PasswordSalt = "Salt 3",
                StateOfBirth = "State 3",
                IsModerator = false
            },
        };
        /// <summary>
        /// If User and UserDto represent the same object. If it's mapped correctly
        /// </summary>
        /// <returns></returns>
        private bool AreEquivalent(UserDto userDto, User user)
        {
            return user.Id == userDto.Id &&
                user.AboutSelf == userDto.AboutSelf &&
                user.AvatarPath == userDto.AvatarPath &&
                user.CityOfBirth == userDto.CityOfBirth &&
                user.CurrentCity == userDto.CurrentCity &&
                user.CurrentState == userDto.CurrentState &&
                user.DateOfBirth == userDto.DateOfBirth &&
                user.FirstName == userDto.FirstName &&
                user.IsMale == userDto.IsMale &&
                user.LastName == userDto.LastName &&
                user.Login == userDto.Login &&
                //user.PasswordHash == userDto.PasswordHash &&
                //user.PasswordSalt == userDto.PasswordSalt &&
                user.StateOfBirth == userDto.StateOfBirth &&
                user.IsModerator == userDto.IsModerator;
        }

        public UserSelectServiceTests()
        {
            //TODO write tests to test how entities are mapped. just like this
            AutomapperInitializer.InitAutoMapper();
        }

        [Test()]
        public void AllNonSubscriptionsOfMatchingTest()
        {
            var stubUnitofWork = MockRepository.GenerateStub<IUnitOfWork>();
            stubUnitofWork.Stub(uow => uow.UserRepository.GetAllNonSubscriptionsOfMatching(new User(), new UserFilter()))
                .IgnoreArguments()
                .Return(mockData);

            var stubUnitOfWorkFactory = MockRepository.GenerateStub<IUnitOfWorkFactory>();
            stubUnitOfWorkFactory.Stub(uowf => uowf.GetUnitOfWork())
                .Return(stubUnitofWork);

            var stubSecurityProvider = MockRepository.GenerateStub<ISecurityProvider>();
            UserSelectService service = new UserSelectService(stubUnitOfWorkFactory, stubSecurityProvider);

            var actual = service.AllNonSubscriptionsOfMatching(new UserDto { Id = 1 }, new UserFilterDto { AgeFrom = 10 });

            Assert.Multiple(() => {
                Assert.That(mockData.Count == actual.Count);
                for (int i = 0; i < mockData.Count; i++)
                {
                    Assert.That(AreEquivalent(actual[i], mockData[i]));
                }
            });
        }
    }
}