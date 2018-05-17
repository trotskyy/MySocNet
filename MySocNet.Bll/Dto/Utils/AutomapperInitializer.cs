using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MySocNet.Dal.Filters;
using MySocNet.Dal;
using MySocNet.Bll.Dto;
using MySocNet.Dal.Entities;

namespace MySocNet.Bll.Dto.Utils
{
    public static class AutomapperInitializer
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
                cfg.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.IsModerator, opt => opt.MapFrom(
                        src => src.IsModerator.HasValue ? src.IsModerator.Value : false));

                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Passwod, opt => opt.Ignore());

                cfg.CreateMap<MessageDto, Message>()
                    .ForMember(dest => dest.IsRead, opt => opt.Condition(src => src.IsRead != null))
                    .ForMember(dest => dest.Sent, opt => opt.Condition(src => src.Sent != null));

                cfg.CreateMap<NotificationDto, Notification>();

                cfg.CreateMap<ThreadDto, ConvThread>();

                cfg.CreateMap<UserFilterDto, UserFilter>();

                cfg.CreateMap<ThreadFilterDto, ThreadFilter>();
            });

            automapperIsInited = true;
        }
    }
}
