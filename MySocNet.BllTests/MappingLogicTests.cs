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
using AutoMapper;

namespace MySocNet.BllTests
{
    [TestFixture]
    public class MappingLogicTests
    {
        #region AreEquivalent methods
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

        /// <summary>
        /// If Msg and MsgDto represent the same object. If it's mapped correctly
        /// </summary>
        /// <returns></returns>
        private bool AreEquivalent(MessageDto messageDto, Message message)
        {
            return messageDto.Id == message.Id &&
                messageDto.FromId == message.FromId &&
                messageDto.ToId == message.ToId &&
                messageDto.Text == message.Text &&
                messageDto.IsRead == message.IsRead &&
                messageDto.Sent == message.Sent;
        }
        #endregion

        public MappingLogicTests()
        {
            AutomapperInitializer.InitAutoMapper();
        }

        [Test]
        public void MapUserToUserDtoTest()
        {
            User user = new User()
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
            };

            UserDto userDto = Mapper.Map<User, UserDto>(user);

            Assert.That(AreEquivalent(userDto, user));
        }

        [Test]
        public void MapUserDtoToUserTest()
        {
            UserDto userDto = new UserDto()
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
                Passwod = "Password 1",
                StateOfBirth = "State 1",
                IsModerator = false
            };

            User user = Mapper.Map<UserDto, User>(userDto);

            Assert.That(AreEquivalent(userDto, user));
        }

        [Test]
        public void MapMessageToMessageDto()
        {
            Message message = new Message
            {
                Id = 1,
                FromId = 2,
                ToId = 3,
                Sent = DateTime.Now,
                IsRead = true,
                Text = "Text 1"
            };

            var MessageDto = Mapper.Map<Message, MessageDto>(message);

            Assert.That(AreEquivalent(MessageDto, message));
        }

        [Test]
        public void MapMessageDtoToMessage()
        {
            var messageDto = new MessageDto
            {
                Id = 1,
                FromId = 2,
                ToId = 3,
                Sent = DateTime.Now,
                IsRead = true,
                Text = "Text 1"
            };

            var message = Mapper.Map<MessageDto, Message>(messageDto);

            Assert.That(AreEquivalent(messageDto, message));
        }
    }
}
