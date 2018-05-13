using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MySocNet.Dal.Common;
using MySocNet.Dal;
using MySocNet.Bll.Dto;
using MySocNet.Dal.Entities;

namespace MySocNet.Bll.Dto.Utils
{
    static class AutomapperInitializer
    {
        private static bool automapperIsInited;

        public static bool IsAutomapperInited => automapperIsInited;

        /// <summary>
        /// All automapper init logic must be written here
        /// </summary>
        public static void InitAutoMapper()
        {
            if (automapperIsInited)
                return;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<MessageDto, Message>();
                cfg.CreateMap<NotificationDto, Notification>();
                cfg.CreateMap<ThreadDto, ConvThread>();
            });

            automapperIsInited = true;
        }
    }
}
