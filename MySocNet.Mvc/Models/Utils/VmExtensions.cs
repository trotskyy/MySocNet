using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySocNet.Bll.Dto;
using AutoMapper;
using MySocNet.Mvc.Models.Common;

namespace MySocNet.Mvc.Models.Utils
{
    public static class VmExtensions
    {
        /*
         * User
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static UserDto MapToDto(this UserVm userVm)
        {
            return Mapper.Map<UserVm, UserDto>(userVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static UserVm MapToVm(this UserDto userDto)
        {
            return Mapper.Map<UserDto, UserVm>(userDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<UserVm> MapToVm(this List<UserDto> userDto)
        {
            return Mapper.Map<List<UserDto>, List<UserVm>>(userDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<UserDto> MapToDto(this List<UserVm> userVm)
        {
            return Mapper.Map<List<UserVm>, List<UserDto>>(userVm);
        }

        /*
         * Message
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static MessageDto MapToDto(this MessageVm MessageVm)
        {
            return Mapper.Map<MessageVm, MessageDto>(MessageVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static MessageVm MapToVm(this MessageDto MessageDto)
        {
            return Mapper.Map<MessageDto, MessageVm>(MessageDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<MessageVm> MapToVm(this List<MessageDto> MessageDto)
        {
            return Mapper.Map<List<MessageDto>, List<MessageVm>>(MessageDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<MessageDto> MapToDto(this List<MessageVm> MessageVm)
        {
            return Mapper.Map<List<MessageVm>, List<MessageDto>>(MessageVm);
        }

        /*
         * Post
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static PostDto MapToDto(this PostVm PostVm)
        {
            return Mapper.Map<PostVm, PostDto>(PostVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static PostVm MapToVm(this PostDto PostDto)
        {
            return Mapper.Map<PostDto, PostVm>(PostDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<PostVm> MapToVm(this List<PostDto> PostDto)
        {
            return Mapper.Map<List<PostDto>, List<PostVm>>(PostDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<PostDto> MapToDto(this List<PostVm> PostVm)
        {
            return Mapper.Map<List<PostVm>, List<PostDto>>(PostVm);
        }

        /*
         * Thread
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ThreadDto MapToDto(this ThreadVm ThreadVm)
        {
            return Mapper.Map<ThreadVm, ThreadDto>(ThreadVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ThreadVm MapToVm(this ThreadDto ThreadDto)
        {
            return Mapper.Map<ThreadDto, ThreadVm>(ThreadDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ThreadVm> MapToVm(this List<ThreadDto> ThreadDto)
        {
            return Mapper.Map<List<ThreadDto>, List<ThreadVm>>(ThreadDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ThreadDto> MapToDto(this List<ThreadVm> ThreadVm)
        {
            return Mapper.Map<List<ThreadVm>, List<ThreadDto>>(ThreadVm);
        }

        /*
         * ThreadFilter
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ThreadFilterDto MapToDto(this ThreadFilterVm ThreadFilterVm)
        {
            return Mapper.Map<ThreadFilterVm, ThreadFilterDto>(ThreadFilterVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ThreadFilterVm MapToVm(this ThreadFilterDto ThreadFilterDto)
        {
            return Mapper.Map<ThreadFilterDto, ThreadFilterVm>(ThreadFilterDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ThreadFilterVm> MapToVm(this List<ThreadFilterDto> ThreadFilterDto)
        {
            return Mapper.Map<List<ThreadFilterDto>, List<ThreadFilterVm>>(ThreadFilterDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ThreadFilterDto> MapToDto(this List<ThreadFilterVm> ThreadFilterVm)
        {
            return Mapper.Map<List<ThreadFilterVm>, List<ThreadFilterDto>>(ThreadFilterVm);
        }

        /*
         * UserFilter
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static UserFilterDto MapToDto(this UserFilterVm UserFilterVm)
        {
            return Mapper.Map<UserFilterVm, UserFilterDto>(UserFilterVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static UserFilterVm MapToVm(this UserFilterDto UserFilterDto)
        {
            return Mapper.Map<UserFilterDto, UserFilterVm>(UserFilterDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<UserFilterVm> MapToVm(this List<UserFilterDto> UserFilterDto)
        {
            return Mapper.Map<List<UserFilterDto>, List<UserFilterVm>>(UserFilterDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<UserFilterDto> MapToDto(this List<UserFilterVm> UserFilterVm)
        {
            return Mapper.Map<List<UserFilterVm>, List<UserFilterDto>>(UserFilterVm);
        }

        /*
         * Notification
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static NotificationDto MapToDto(this NotificationVm NotificationVm)
        {
            return Mapper.Map<NotificationVm, NotificationDto>(NotificationVm);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static NotificationVm MapToVm(this NotificationDto NotificationDto)
        {
            return Mapper.Map<NotificationDto, NotificationVm>(NotificationDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<NotificationVm> MapToVm(this List<NotificationDto> NotificationDto)
        {
            return Mapper.Map<List<NotificationDto>, List<NotificationVm>>(NotificationDto);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<NotificationDto> MapToDto(this List<NotificationVm> NotificationVm)
        {
            return Mapper.Map<List<NotificationVm>, List<NotificationDto>>(NotificationVm);
        }
    }
}