using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Dal.Filters;
using MySocNet.Dal.Abstract;
using MySocNet.Dal;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Dto.Utils;
using AutoMapper;
using MySocNet.Bll.Exceptions;

namespace MySocNet.Bll.Services
{
    public class UserService : GenericService<UserDto, User>, IUserService
    {
        IUserSelectService _userSelectService;
        ISecurityProvider _securityProvider;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, ISecurityProvider securityProvider) : base(unitOfWorkFactory)
        {
            _securityProvider = securityProvider;
        }

        public IUserSelectService Get
        {
            get
            {
                if(_userSelectService == null)
                    _userSelectService = new UserSelectService(_unitOfWorkFactory, _securityProvider);
                return _userSelectService;
            }
        }

        public void CheckLoginAndPassword(UserDto user, out bool isLoginValid, out bool isPasswordValid)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (user.Login == null)
                throw new DtoValidationException("Login is null");
            if (user.Passwod == null)
                throw new DtoValidationException("Passwod is null");

            User userDb;

            try
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
                {
                    userDb = unitOfWork.UserRepository.GetByLogin(user.Login);
                }
            }
            catch (Exception ex)
            {
                throw new DomainModelException("watch inner exception", ex);
            }

            if(userDb == null)
            {
                isLoginValid = false;
                isPasswordValid = false;
                return;
            }

            isLoginValid = true;
            string passwordHash = _securityProvider.ComputeHash(user.Passwod, userDb.PasswordSalt);
            isPasswordValid = passwordHash == userDb.PasswordHash;
        }

        public void CreateAccount(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (user.Id != 0)
                user.Id = 0;
            if (user.Login == null)
                throw new DtoValidationException("Set Login!");
            if (user.Passwod == null)
                throw new DtoValidationException("Set password!");

            User userToAdd = user.MapToDbEntity();
            string wallName = Utils.WallThreadNameResolver.GetWallThreadName(userToAdd);
            //generate 128 bit salt
            userToAdd.PasswordSalt = _securityProvider.GenerateSalt(128 / 8);
            userToAdd.PasswordHash = _securityProvider.ComputeHash(user.Passwod, userToAdd.PasswordSalt);

            ExecuteNonQuery(uow => {
                uow.UserRepository.Create(userToAdd);
                uow.ThreadRepository.Create(new ConvThread { Name = wallName, Moderator = userToAdd }); //create wall
                uow.SaveChanges();
            });
        }

        public void CreateAccount(string login, string password)
        {
            UserDto user = new UserDto() { Login = login, Passwod = password };
            CreateAccount(user);
        }

        public void Subscribe(UserDto publisher, UserDto subscriber)
        {
            if (publisher == null || subscriber == null)
                throw new ArgumentNullException();
            if (publisher.Id == 0)
                throw new DtoValidationException("Publisher Id is not set");
            if (subscriber.Id == 0)
                throw new DtoValidationException("Subscriber Id is not set");
            if (publisher.Id == subscriber.Id)
                throw new DtoValidationException("Publisher and Subscriber are same people!");

            ExecuteNonQuery(unitOfWork =>
                {
                    User publisherFromDb = unitOfWork.UserRepository.GetById(publisher.Id);
                    if (publisherFromDb == null)
                        throw new DomainModelException("No such publisher");

                    User subscriberFromDb = unitOfWork.UserRepository.GetById(subscriber.Id);
                    if (subscriberFromDb == null)
                        throw new DomainModelException("No such subscriber");

                    UsersRelation usersRelation = new UsersRelation()
                    {
                        PublisherId = publisher.Id,
                        SubscriberId = subscriber.Id
                    };
                    unitOfWork.UsersRelationRepository.Create(usersRelation);
                    unitOfWork.SaveChanges();
                });
        }

        public void Subscribe(int publisherId, int subscriberId)
        {
            Subscribe(new UserDto(publisherId), new UserDto(subscriberId));
        }
    }
}
