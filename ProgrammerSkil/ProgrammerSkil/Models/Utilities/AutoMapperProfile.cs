using AutoMapper;
using ProgrammerSkil.Models.Entity;
using ProgrammerSkil.Models.ViewModel.Cooperation;
using ProgrammerSkil.Models.ViewModel.FeedBack;
using ProgrammerSkil.Models.ViewModel.Sample;
using ProgrammerSkil.Models.ViewModel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TBL_Cooperation, CooperationDTO>();
            CreateMap<TBL_User, ViewModel.Cooperation.UserDTO>();
            CreateMap<TBL_UserProfile, ViewModel.Cooperation.UserProfileDTO>();

            //write  reverseto  if u want


            CreateMap<TBL_FeedBacks, FeedBackDTO>();
            CreateMap<TBL_User, ViewModel.FeedBack.UserDTO>();
            CreateMap<TBL_UserProfile, ViewModel.FeedBack.UserProfileDTO>();


            CreateMap<TBL_Setting, SettingDTO>();
            CreateMap<TBL_SocialMedia, SocialMediaDTO>();
            CreateMap<TBL_User, ViewModel.Setting.UserDTO>();
            CreateMap<TBL_UserProfile, ViewModel.Setting.UserProfileDTO>();


            CreateMap<TBL_Samples, SampleDTO>();
            CreateMap<TBL_User, ViewModel.Sample.UserDTO>();
            CreateMap<TBL_UserProfile, ViewModel.Sample.UserProfileDTO>();
            CreateMap<TBL_Images, ViewModel.Sample.ImageDTO>();



            CreateMap<TBL_User, ViewModel.User.DTO.AdminDTO>();
            CreateMap<TBL_Samples, ViewModel.User.DTO.SampleDTO>()
                         .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(c => c.Photo)));


            CreateMap<TBL_User, ViewModel.User.DTO.AdminDTO>();
            CreateMap<TBL_Samples, ViewModel.User.DTO.SampleDTO>()
                         .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(c => c.Photo)));




            CreateMap<TBL_User, ViewModel.User.ProgrammerDTO.ProgrammerDTO>();
            CreateMap<TBL_FeedBacks, ViewModel.User.ProgrammerDTO.FeedBackDTO>();
            CreateMap<TBL_Cooperation, ViewModel.User.ProgrammerDTO.CooperationDTO>();
            CreateMap<TBL_UserProfile, ViewModel.User.ProgrammerDTO.UserProFileDTO>()
                   .ForMember(d => d.Skills, o => o.MapFrom(s => s.SkillUserProfile.Select(v => v.Skill)));
                         




            //CreateMap<TBL_User, ViewModel.Sample.UserDTO>();
            //CreateMap<TBL_UserProfile, ViewModel.Sample.UserProfileDTO>();
            //CreateMap<TBL_Images, ViewModel.Sample.ImageDTO>();
        }

        public static void Run()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperProfile>();
            });
        }


    }
}