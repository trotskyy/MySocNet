using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Bll.Dto;
using MySocNet.Dal.Common;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Dal.Abstract;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Dto.Utils;
using AutoMapper;

namespace MySocNet.Bll.Services
{
    public class UserSelectService : GenericService<UserDto, User>, IUserSelectService
    {
        ISecurityProvider _securityProvider;

        public UserSelectService(IUnitOfWorkFactory unitOfWorkFactory, ISecurityProvider securityProvider) : base(unitOfWorkFactory)
        {
            _securityProvider = securityProvider;
        }

        private void ValidateUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (user.Id == 0)
                throw new IdNotSpecifiedException();
        }

        private void ValidateUser(UserDto user, UserFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException();
            ValidateUser(user);
        }

        public List<UserDto> AllNonSubscriptionsOfMatching(UserDto subscriber, UserFilter filter)
        {
            ValidateUser(subscriber, filter);

            return ExecuteSelectQuery(unitOfWork => unitOfWork
                .UserRepository
                .GetAllNonSubscriptionsOfMatching(subscriber.MapToDbEntity(), filter));
        }

        public List<UserDto> AllSubscribersOf(UserDto publisher)
        {
            ValidateUser(publisher);

            return ExecuteSelectQuery(unitOfWork => unitOfWork
                .UserRepository
                .GetAllSubscribersOf(publisher.MapToDbEntity()));
        }

        public List<UserDto> AllSubscribersOfMatching(UserDto publisher, UserFilter filter)
        {
            ValidateUser(publisher, filter);

            return ExecuteSelectQuery(unitOfWork => unitOfWork
                .UserRepository
                .GetAllSubscribersOfMatching(publisher.MapToDbEntity(), filter));
        }

        public List<UserDto> AllSubscriptionsOf(UserDto subscriber)
        {
            ValidateUser(subscriber);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetAllSubscriptionsOf(subscriber.MapToDbEntity()));
        }

        public List<UserDto> AllSubscriptionsOfMatching(UserDto subscriber, UserFilter filter)
        {
            ValidateUser(subscriber, filter);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetAllSubscriptionsOfMatching(subscriber.MapToDbEntity(), filter));
        }

        public List<UserDto> AllUsersMatching(UserFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException();

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetAllUsersMatching(filter));
        }

        public int CountOfSubscribers(UserDto publisher)
        {
            ValidateUser(publisher);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetCountOfSubscribers(publisher.MapToDbEntity()));
        }

        public int CountOfSubscriptions(UserDto subscriber)
        {
            ValidateUser(subscriber);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetCountOfSubscriptions(subscriber.MapToDbEntity()));
        }

        public List<UserDto> TopLastSubscribersOf(UserDto publisher, int top)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");

            ValidateUser(publisher);
            
            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetTopLastSubscribersOf(publisher.MapToDbEntity(), top));
        }

        public List<UserDto> TopLastSubscribersOf(UserDto publisher, int skip, int top)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            if (skip <= 0)
                throw new ArgumentOutOfRangeException("skip");

            ValidateUser(publisher);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetTopLastSubscribersOf(publisher.MapToDbEntity(), skip, top));
        }

        public List<UserDto> TopLastSubscriptionsOf(UserDto subscriber, int top)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");

            ValidateUser(subscriber);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetTopLastSubscriptionsOf(subscriber.MapToDbEntity(), top));
        }

        public List<UserDto> TopLastSubscriptionsOf(UserDto subscriber, int skip, int top)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            if (skip <= 0)
                throw new ArgumentOutOfRangeException("skip");

            ValidateUser(subscriber);

            return ExecuteSelectQuery(uow => uow
                .UserRepository
                .GetTopLastSubscriptionsOf(subscriber.MapToDbEntity(), skip, top));
        }
    }
}
