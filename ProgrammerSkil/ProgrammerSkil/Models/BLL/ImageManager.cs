using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.ViewModel.Sample;
using ProgrammerSkil.Models.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAD_ExtentionMethods;
namespace ProgrammerSkil.Models.BLL
{
    public class ImageManager : IImageManager
    {
        private IImageaRepository _repo;
        public ImageManager(IImageaRepository Repo)
        {
            _repo = Repo;
        }

        //public ValidateResultViewModel ValidateFile(PostSampleViewModel sampleViewModel)
        //{
        //    bool IsValid = true;
        //    string Errors = "";


        //    var Files = sampleViewModel.FIles;
        //    if (Files == null) {
        //        IsValid = false;
        //        Errors += "-     فایل نباید خالی باشد. <br />";
        //    }
        //    //    return
        //    //TAD_ExtentionMethods.ExtentionMethods.IsImage(,)
        //}
    }
}