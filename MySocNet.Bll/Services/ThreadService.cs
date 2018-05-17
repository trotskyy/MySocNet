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
    public class ThreadService : GenericService<ThreadDto, ConvThread>, IThreadService
    {
        private readonly IThreadSelectService _threadSelectService;

        public IThreadSelectService Get => _threadSelectService;

        public ThreadService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _threadSelectService = new ThreadSelectService(unitOfWorkFactory);
        }

        private void ValidateThread(ThreadDto thread)
        {
            if (thread == null)
                throw new ArgumentNullException();
            if (thread.Id == 0)
                throw new IdNotSpecifiedException();
        }

        public void Create(ThreadDto thread)
        {
            if (thread == null)
                throw new ArgumentNullException();
            if (thread.ModeratorId == 0)
                throw new DtoValidationException("Set ModeratorId");
            if (string.IsNullOrWhiteSpace(thread.Name))
                throw new DtoValidationException("Set Name");
            if (string.IsNullOrWhiteSpace(thread.Topic))
                throw new DtoValidationException("Set Topic");

            ExecuteNonQuery(uow => {
                uow.ThreadRepository.Create(thread.MapToDbEntity());
                uow.SaveChanges();
            });
        }

        public void Delete(ThreadDto thread)
        {
            ValidateThread(thread);

            ExecuteNonQuery(uow => {
                uow.ThreadRepository.Delete(thread.MapToDbEntity());
                uow.SaveChanges();
            });
        }
    }
}
