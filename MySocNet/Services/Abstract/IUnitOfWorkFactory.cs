using MySocNet.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
    }
}
