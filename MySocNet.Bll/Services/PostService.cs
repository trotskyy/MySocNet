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
    public class PostService : GenericService<PostDto, Post>, IPostService
    {
        IPostSelectService _postSelectService;

        public PostService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        public IPostSelectService PostSelectService
        {
            get
            {
                if (_postSelectService == null)
                    _postSelectService = new PostSelectService(_unitOfWorkFactory);
                return _postSelectService;
            }
        }

        public void Delete(PostDto post)
        {
            if(post == null)
                throw new ArgumentNullException();
            if (post.Id == 0)
                throw new IdNotSpecifiedException();

            ExecuteNonQuery(uow => {
                uow.PostRepository.Delete(post.MapToDbEntity());
                uow.SaveChanges();
            });
        }

        public void Write(PostDto post)
        {
            if (post == null)
                throw new ArgumentNullException();
            if (post.Text == null)
                throw new DtoValidationException("Post must contain text");
            if (post.ThreadId == 0)
                throw new DtoValidationException("Post must have thread Id");
            if(post.AuthorId == 0)
                throw new DtoValidationException("Post must have author Id");

            post.Published = DateTime.Now;

            ExecuteNonQuery(uow => {
                uow.PostRepository.Create(post.MapToDbEntity());
                uow.SaveChanges();
            });
        }
    }
}
