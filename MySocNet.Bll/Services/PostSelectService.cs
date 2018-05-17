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

namespace MySocNet.Bll.Services
{
    public class PostSelectService : GenericService<PostDto, Post>, IPostSelectService
    {
        public PostSelectService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        private void ValidateThread(ThreadDto thread)
        {
            if (thread == null)
                throw new ArgumentNullException();
            if (thread.Id == 0)
                throw new IdNotSpecifiedException();
        }

        public List<PostDto> ByAuthor(UserDto author)
        {
            ValidateUser(author);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetByAuthor(author.MapToDbEntity()));
        }

        public List<PostDto> ByAuthors(List<UserDto> authors)
        {
            List<UserDto> authorsDto = new List<UserDto>();
            foreach (var a in authors)
                ValidateUser(a);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetByAuthors(authors.MapToDbEntitiesList()));
        }

        public List<PostDto> ByAuthorsOrThreads(List<UserDto> authors, List<ThreadDto> threads)
        {
            foreach (var a in authors)
                ValidateUser(a);
            foreach (var t in threads)
                ValidateThread(t);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetByAuthorsOrThreads(authors.MapToDbEntitiesList(),
                                                                                      threads.MapToDbEntitiesList()));
        }

        public List<PostDto> ByThread(ThreadDto thread)
        {
            ValidateThread(thread);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetByThread(thread.MapToDbEntity()));
        }

        public List<PostDto> ByThreads(List<ThreadDto> threads)
        {
            foreach (var t in threads)
                ValidateThread(t);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetByThreads(threads.MapToDbEntitiesList()));
        }

        public List<PostDto> GetTopLatestFeedPosts(UserDto user, int skip = -1, int top = -1)
        {
            ValidateUser(user);

            return ExecuteSelectQuery(uow => uow.PostRepository.GetTopLatestFeedPosts(user.MapToDbEntity(), skip, top));
        }
    }
}
