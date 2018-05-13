using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface ISocNetFacade
    {
        IUserService UserService { get; }
    }
}
