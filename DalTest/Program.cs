using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal;
using System.Data.Entity;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Dto.Utils;
using AutoMapper;

namespace DalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //User u = new User()
            //{
            //    Id = 1,
            //    AboutSelf = "2323",
            //    Login = "user666",
            //    Passwod = "qwerty"
            //};
            //User u2 = new User()
            //{
            //    Id = 2,
            //    AboutSelf = "sdfafawf",
            //    Login = "us sus us sas",
            //    Passwod = "123123"
            //};
            //List<User> list = new List<User>() { u, u2 };

            //Message m = new Message()
            //{
            //    Id = 1,
            //    FromId = 2,
            //    Text = "Msg txt"
            //};

            //AutomapperInitializer.InitAutoMapper();
            //UserDto userDto = Mapper.Map<User, UserDto>(u);
            //MessageDto messageDto = Mapper.Map<Message, MessageDto>(m);

            //List<UserDto> list2 = Mapper.Map<List<User>, List<UserDto>>(list);

            //UserDto userDto2 = new UserDto()
            //{
            //    Id = 90,
            //    FirstName = "John",
            //    LastName = "Malcovic",
            //    IsMale = true
            //};
            //User u3 = Mapper.Map<UserDto, User>(userDto2);

            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();
        }
    }
}
