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

        public List<PostDto> TopLatestFeedPosts(UserDto user, int skip = -1, int top = -1, bool withAuthors = false)
        {
            ValidateUser(user);

            var posts = ExecuteSelectQuery(uow => uow.PostRepository.GetTopLatestFeedPosts(user.MapToDbEntity(), skip, top, withAuthors));

            if(withAuthors)
            {
                foreach (var p in posts)
                {
                    p.Author = ExecuteSelectQuery<User>(uow => uow.UserRepository.GetById(p.AuthorId)).MapToDtoEntity();
                    p.Thread = ExecuteSelectQuery(uow => uow.ThreadRepository.GetById(p.ThreadId)).MapToDtoEntity();
                }
            }

            return posts;
        }

        public PostDto ById(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException();

            return ExecuteSelectQuery(uow => uow.PostRepository.GetById(id));
        }

        public List<PostDto> AllTopRecentPosts(int skip = -1, int top = -1)
        {
            return ExecuteSelectQuery(uow => uow.PostRepository.GetAllTopRecentPosts(skip, top));
        }

        public List<PostDto> WallPosts(int wallOwnerId, int top = -1, int skip = -1)
        {
            return ExecuteSelectQuery(uow => {
                User wallOwner = uow.UserRepository.GetById(wallOwnerId);
                string wallName = Utils.WallThreadNameResolver.GetWallThreadName(wallOwner);
                return uow.PostRepository.GetPostsByThreadName(wallName, top, skip);
            });
        }

        public List<KeyValuePair<PostDto, string>> WallPosts(int wallOwnerId, int top = -1, int skip = -1, bool withAuthors = false)
        {
            List<KeyValuePair<PostDto, string>> result = new List<KeyValuePair<PostDto, string>>();
            ExecuteNonQuery(uow => {
                User wallOwner = uow.UserRepository.GetById(wallOwnerId);
                string wallName = Utils.WallThreadNameResolver.GetWallThreadName(wallOwner);

                var threads = uow.ThreadRepository.GetByModerator(wallOwner);

                var wall = threads.Where(t => t.Name == Utils.WallThreadNameResolver.GetWallThreadName(wallOwner))
                    .FirstOrDefault();


                var wallPosts = uow.PostRepository.GetByThread(wall);
                foreach (var p in wallPosts)
                {
                    string author = uow.UserRepository.GetById(p.AuthorId).FullName();
                    result.Add(new KeyValuePair<PostDto, string>(p.MapToDtoEntity(), author));
                }
            });
            return result;
        }
    }
}
