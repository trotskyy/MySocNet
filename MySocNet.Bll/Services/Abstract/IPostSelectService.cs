using MySocNet.Bll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IPostSelectService
    {
        PostDto ById(int id);

        /// <summary>
        /// Стена, как вк - это тоже тред
        /// </summary>
        /// <param name="wallOwner"></param>
        /// <returns></returns>
        List<PostDto> WallPosts(int wallOwnerId, int top = -1, int skip = -1);

        /// <summary>
        /// Стена, как вк - это тоже тред
        /// </summary>
        /// <param name="wallOwner"></param>
        /// <returns></returns>
        List<KeyValuePair<PostDto, string>> WallPosts(int wallOwnerId, int top = -1, int skip = -1, bool withAuthors = false);

        List<PostDto> ByThread(ThreadDto thread);
        List<PostDto> ByThreads(List<ThreadDto> threads);

        List<PostDto> ByAuthor(UserDto author);
        List<PostDto> ByAuthors(List<UserDto> authors);

        List<PostDto> ByAuthorsOrThreads(List<UserDto> authors, List<ThreadDto> threads);
        
        /// <summary>
        /// Получить посты для новосной ленты
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<PostDto> TopLatestFeedPosts(UserDto user, int skip = -1, int top = -1, bool withAuthors = false);

        List<PostDto> AllTopRecentPosts(int skip = -1, int top = -1);
    }
}
