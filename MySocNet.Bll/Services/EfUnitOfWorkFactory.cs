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
using MySocNet.Dal.Abstract;
using MySocNet.Dal;

namespace MySocNet.Bll.Services
{
    /// <summary>
    /// To retrieve Entity Framework UnitOfWork
    /// </summary>
    public class EfUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}
