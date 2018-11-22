using AutoMapper;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.App_Start
{
    public class AutoMapConfigure
    {
        #region Public Methods and Operators

        public static void Configure()
        {
            Mapper.CreateMap<UserSetting, UserSettingDTO>().ReverseMap();
            Mapper.CreateMap<User, UserModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountModel>().ReverseMap();
        }

        #endregion

    }
}