using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Dto;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IThreadService
    {
        void Create(ThreadDto thread);

        void Delete(ThreadDto thread);

        IThreadSelectService Get { get; }
    }
}
