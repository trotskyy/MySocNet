using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Dto;

namespace MySocNet.Bll.Dto.Utils
{
    public static class GeneralEntitiesExtensions
    {
        /*
         * User
         */
         /// <summary>
         /// Map using Automapper
         /// </summary>
        public static UserDto MapToDtoEntity(this User user)
        {
            return Mapper.Map<User, UserDto>(user);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<UserDto> MapToDtoEntitiesList(this List<User> users)
        {
            return Mapper.Map<List<User>, List<UserDto>>(users);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static User MapToDbEntity(this UserDto user)
        {
            return Mapper.Map<UserDto, User>(user);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<User> MapToDbEntitiesList(this List<UserDto> users)
        {
            return Mapper.Map<List<UserDto>, List<User>>(users);
        }

        /*
         * Message
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static MessageDto MapToDtoEntity(this Message Message)
        {
            return Mapper.Map<Message, MessageDto>(Message);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<MessageDto> MapToDtoEntitiesList(this List<Message> Messages)
        {
            return Mapper.Map<List<Message>, List<MessageDto>>(Messages);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static Message MapToDbEntity(this MessageDto Message)
        {
            return Mapper.Map<MessageDto, Message>(Message);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<Message> MapToDbEntitiesList(this List<MessageDto> Messages)
        {
            return Mapper.Map<List<MessageDto>, List<Message>>(Messages);
        }

        /*
         * Notification
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static NotificationDto MapToDtoEntity(this Notification Notification)
        {
            return Mapper.Map<Notification, NotificationDto>(Notification);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<NotificationDto> MapToDtoEntitiesList(this List<Notification> Notifications)
        {
            return Mapper.Map<List<Notification>, List<NotificationDto>>(Notifications);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static Notification MapToDbEntity(this NotificationDto Notification)
        {
            return Mapper.Map<NotificationDto, Notification>(Notification);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<Notification> MapToDbEntitiesList(this List<NotificationDto> Notifications)
        {
            return Mapper.Map<List<NotificationDto>, List<Notification>>(Notifications);
        }

        /*
         * Post
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static PostDto MapToDtoEntity(this Post Post)
        {
            return Mapper.Map<Post, PostDto>(Post);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<PostDto> MapToDtoEntitiesList(this List<Post> Posts)
        {
            return Mapper.Map<List<Post>, List<PostDto>>(Posts);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static Post MapToDbEntity(this PostDto Post)
        {
            return Mapper.Map<PostDto, Post>(Post);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<Post> MapToDbEntitiesList(this List<PostDto> Posts)
        {
            return Mapper.Map<List<PostDto>, List<Post>>(Posts);
        }

        /*
         * Thread
         */
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ThreadDto MapToDtoEntity(this ConvThread Thread)
        {
            return Mapper.Map<ConvThread, ThreadDto>(Thread);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ThreadDto> MapToDtoEntitiesList(this List<ConvThread> Threads)
        {
            return Mapper.Map<List<ConvThread>, List<ThreadDto>>(Threads);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static ConvThread MapToDbEntity(this ThreadDto Thread)
        {
            return Mapper.Map<ThreadDto, ConvThread>(Thread);
        }
        /// <summary>
        /// Map using Automapper
        /// </summary>
        public static List<ConvThread> MapToDbEntitiesList(this List<ThreadDto> Threads)
        {
            return Mapper.Map<List<ThreadDto>, List<ConvThread>>(Threads);
        }
    }
}
