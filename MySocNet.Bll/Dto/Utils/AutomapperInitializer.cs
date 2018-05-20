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
        private static object lockObject = new object();

        private static bool automapperIsInited;

        public static bool IsAutomapperInited => automapperIsInited;

        private static Action<IMapperConfigurationExpression> _bllAutomapperConfigurationExpression =
            (cfg) => 
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

                cfg.CreateMap<NotificationTypeDto, NotificationType>();
            };

        /// <summary>
        /// Use in Global.asax
        /// </summary>
        /// <param name="additionalAutomapperLogic"></param>
        public static void InitAutoMapper(Action<IMapperConfigurationExpression> additionalAutomapperLogic)
        {
            lock (lockObject)
            {
                if (automapperIsInited)
                    return;

                additionalAutomapperLogic += _bllAutomapperConfigurationExpression;
                Mapper.Initialize(additionalAutomapperLogic);

                automapperIsInited = true;
            }
        }

        /// <summary>
        /// All automapper init logic must be written here
        /// </summary>
        public static void InitAutoMapper()
        {
            lock(lockObject)
            {
                if (automapperIsInited)
                    return;

                Mapper.Initialize(_bllAutomapperConfigurationExpression);

                automapperIsInited = true;
            }
        }
    }
}
