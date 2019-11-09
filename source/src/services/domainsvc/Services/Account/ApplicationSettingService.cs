namespace MLC.General.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MLC.General.Contract;
    using Domain =  MLC.General.Domain;
    using System.Linq;
    using System;
    using System.Linq.Expressions;
    using static MLC.Foundation.Core;
    using MLC.Foundation;

    public class ApplicationSettingService : BaseService, IApplicationSettingService 
    {
        private Domain.IApplicationSettingRepository _applicationSettingsRepository;

        public ApplicationSettingService(ApplicationContext appContext, Domain.IApplicationSettingRepository applicationSettingsRepository)
            : base(appContext)
        {
            _applicationSettingsRepository = applicationSettingsRepository;
        }

        public async Task<Result<IEnumerable<ApplicationSetting>>> GetAllApplicationSettings(string q = "")
        {
            var ApplicationSettingSpec = IsActiveApplicationSetting(q);

            return await Async(ApplicationSettingSpec)
                .Map(_applicationSettingsRepository.FindAllAsync)
              .Map(lstApplicationSetting => lstApplicationSetting.Select(p => p.ToContract()));
        }
      
        public async Task<Result<ApplicationSetting>> GetApplicationSetting(string id)
        {
            return await _applicationSettingsRepository.GetByIdAsync(id)
               .Map(pg => pg.ToContract());
        }

        public async Task<Result<ApplicationSetting>> GetApplicationSettingBySlugName(string slugName)
        {
            var ApplicationSettingSpec = IsActiveApplicationSetting(slugName);

            return await Async(ApplicationSettingSpec)
                 .Map(_applicationSettingsRepository.FindAllAsync)
               .Map(lstApplicationSetting => lstApplicationSetting.Select(p => p.ToContract()).FirstOrDefault());
        }

        public async Task<Result<ApplicationSetting>> Create(ApplicationSettingUpsert ApplicationSetting)
        {
            return  await Async(ApplicationSetting.ToDomain(_appContext.Email))
                     .Map(Validate)
                    .Map(_applicationSettingsRepository.SaveAsync)
                    .Map(domApplicationSetting => domApplicationSetting.ToContract());
        }

        public async Task<Result<ApplicationSetting>> Update(string id, ApplicationSettingUpsert ApplicationSetting)
        {
           
            return await Async(id)
                .Map(_applicationSettingsRepository.GetByIdAsync)
                .Map(domApplicationSetting => domApplicationSetting.Update(id, ApplicationSetting, _appContext.Email))
                .Map(_applicationSettingsRepository.SaveAsync)
                .Map(domApplicationSetting => domApplicationSetting.ToContract());
        }

        public async Task<Result<bool>> Delete(string path)
        {
            return await Async(path)
                .Map(_applicationSettingsRepository.DeleteAsync);
        }

        #region Private Methods
      
        private async Task<Result<Domain.ApplicationSetting>> Validate(Domain.ApplicationSetting ApplicationSetting)
        {
            var ApplicationSettingSpec = FilterByTitle(ApplicationSetting.CollegeName);
            var ApplicationSettingTitleExist = Spec<List<Domain.ApplicationSetting>>(pl => pl.Count <= 0);

          
            var domApplicationSettings = await Async(ApplicationSettingSpec)
               .Map(_applicationSettingsRepository.FindAllAsync);

           return domApplicationSettings.Map(ApplicationSettingTitleExist, () => Error.Of(ErrorCodes.InputExists)).Match(fail: e => e, pass: (p) => Result(ApplicationSetting));
        }

       
        private Specification<Domain.ApplicationSetting> IsActiveApplicationSetting(string q)
        {
            return new Specification<Domain.ApplicationSetting>(p => p.IsActive == true && p.IsDelete == false);
        }

        private Specification<Domain.ApplicationSetting> FilterByTitle(string title)
        {
            return new Specification<Domain.ApplicationSetting>(p => p.CollegeName == title && p.IsDelete == false);
        }

        #endregion

    }
}