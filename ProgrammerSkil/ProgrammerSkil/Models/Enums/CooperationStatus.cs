using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.Enums
{
    public enum CooperationStatus
    {
        [Range(0,2,ErrorMessage =" این وضعیت تعریف نشده")]
        [Display(Name ="بررسی نشده")]
        [Description("بررسی نشده")]
        NoChecked,
        [Display(Name = "رد شده")]
        [Description("رد شده")]
        rejected,
        [Display(Name = "بررسی شده")]
        [Description("بررسی شده")]
        Reviewed
    }
}