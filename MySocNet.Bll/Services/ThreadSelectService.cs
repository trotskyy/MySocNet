using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Dto.Utils;
using AutoMapper;

namespace MySocNet.Bll.Services
{
    public class ThreadSelectService : GenericService<ThreadDto, ConvThread>, IThreadSelectService
    {
        public ThreadSelectService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        private void ValidateThread(ThreadDto thread)
        {
            if (thread == null)
                throw new ArgumentNullException();
            if (thread.Id == 0)
                throw new IdNotSpecifiedException();
        }

        public List<string> AllTopics()
        {
            List<string> result = new List<string>();
            ExecuteNonQuery(uow => {
                result = uow.ThreadRepository.GetAllTopics();
            });
            return result;
        }

        public List<string> AllTopicsStartingWith(string start)
        {
            List<string> result = new List<string>();
            ExecuteNonQuery(uow => {
                result = uow.ThreadRepository.GetAllTopicsStartingWith(start);
            });
            return result;
        }

        public int SubscribersCount(ThreadDto thread)
        {
            ValidateThread(thread);

            int result = -1;
            ExecuteNonQuery(uow => {
                result = uow.ThreadRepository.GetSubscribersCount(thread.MapToDbEntity());
            });
            return result;
        }
        /// <summary>
        /// Separate method for List KeyValuePair ThreadDto int
        /// </summary>
        /// <returns></returns>
        private List<KeyValuePair<ThreadDto, int>> ExecuteSelectQueryWithKvp(
            Func<Dal.Abstract.IUnitOfWork,
            List<KeyValuePair<ConvThread, int>>> query)
        {
            var result = new List<KeyValuePair<ThreadDto, int>>();
            ExecuteNonQuery(uow => {
                var threads = query.Invoke(uow);

                result = Mapper.Map<List<KeyValuePair<ConvThread, int>>, List<KeyValuePair<ThreadDto, int>>>(threads);
            });
            return result;
        }

        public List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountByModerator(UserDto moderator)
        {
            ValidateUser(moderator);

            return ExecuteSelectQueryWithKvp(uow => uow.ThreadRepository
                .GetThreadsWithSubscribersCountByModerator(moderator.MapToDbEntity()));
        }

        public List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountByModeratorMatching(UserDto moderator, ThreadFilterDto filter)
        {
            ValidateUser(moderator);

            return ExecuteSelectQueryWithKvp(uow => uow.ThreadRepository
                .GetThreadsWithSubscribersCountByModeratorMatching(moderator.MapToDbEntity(),
                                                                   filter.MapToDbEntity()));
        }

        public List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountBySubscriber(UserDto subscriber)
        {
            ValidateUser(subscriber);

            return ExecuteSelectQueryWithKvp(uow => uow.ThreadRepository
                .GetThreadsWithSubscribersCountBySubscriber(subscriber.MapToDbEntity()));
        }

        public List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountBySubscriberMatching(UserDto subscriber, ThreadFilterDto filter)
        {
            return ExecuteSelectQueryWithKvp(uow => uow.ThreadRepository
                .GetThreadsWithSubscribersCountBySubscriberMatching(subscriber.MapToDbEntity(),
                                                                    filter.MapToDbEntity()));
        }
    }
}
