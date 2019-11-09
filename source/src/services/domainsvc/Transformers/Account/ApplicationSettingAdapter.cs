namespace MLC.General.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MLC.General.Contract;
    using Domain = MLC.General.Domain;


    public static class ApplicationSettingAdapter
    {
        public static ApplicationSetting ToContract(this Domain.ApplicationSetting applicationSetting)
        {
            return new ApplicationSetting()
            {
                Id = applicationSetting.Id,
                CollegeName = applicationSetting.CollegeName,
                Address = applicationSetting.Address,
                ContactNo = applicationSetting.ContactNo,
                Email = applicationSetting.Email,
                Map = applicationSetting.Map,
                Fax = applicationSetting.Fax,
                Logo = applicationSetting.Logo,

                IsDelete = applicationSetting.IsDelete,
                CreatedBy = applicationSetting.CreatedBy,
                CreatedOn = applicationSetting.CreatedOn,
                ModifiedOn = applicationSetting.ModifiedOn,
                ModifiedBy = applicationSetting.ModifiedBy
            };
        }

      
        public static Domain.ApplicationSetting Update(this Domain.ApplicationSetting domApplicationSetting,string id, ApplicationSettingUpsert applicationSetting, string userId)
        {
            return new Domain.ApplicationSetting()
            {
                Id = id,
                CollegeName = applicationSetting.CollegeName,
                Address = applicationSetting.Address,
                ContactNo = applicationSetting.ContactNo,
                Email = applicationSetting.Email,
                Map = applicationSetting.Map,
                Fax = applicationSetting.Fax,
                Logo = applicationSetting.Logo,

                CreatedBy = domApplicationSetting.CreatedBy,
                CreatedOn = domApplicationSetting.CreatedOn,
                ModifiedOn = DateTime.Now,
                ModifiedBy = userId
            };
        }

        public static Domain.ApplicationSetting ToDomain(this ApplicationSettingUpsert applicationSetting, string userId)
        {
            return new Domain.ApplicationSetting()
            {  
                CollegeName = applicationSetting.CollegeName,
                Address = applicationSetting.Address,
                ContactNo = applicationSetting.ContactNo,
                Email = applicationSetting.Email,
                Map = applicationSetting.Map,
                Fax = applicationSetting.Fax,
                Logo = applicationSetting.Logo,
                CreatedBy = userId
            };
        }
      
    }
}
