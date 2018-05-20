using MySocNet.Bll.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IPostService
    {
        IPostSelectService Get { get; }

        void Write(PostDto post);

        void Delete(PostDto post);
    }
}