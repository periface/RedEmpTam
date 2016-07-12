using Abp.Domain.Repositories;
using Helpers.Helpers.HelperModels;
using MercadoCinotam.MainPageContentManager.Entities;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.Pyme.Manager;

namespace MercadoCinotam.Pyme
{
    public class PymeProvider : PymeManager
    {
        private readonly IRepository<MainPageContent> _mainPageContentRepository;
        public PymeProvider(
            IRepository<PymeInfo> pymeInfoRepository,
            IRepository<PymeContactInfo> pymeContactRepository,
            IRepository<MainPageContent> mainPageContentRepository, IRepository<MainPageContent> mainPageContentRepository1)
            : base(pymeInfoRepository,
                  pymeContactRepository,
                  mainPageContentRepository)
        {
            _mainPageContentRepository = mainPageContentRepository1;
        }

        public void SetMainPageContent(ThemeInfo theme, bool keepOldData)
        {
            if (keepOldData)
            {

                foreach (var themeRequiredField in theme.ThemeRequiredProperties)
                {
                    //Checks if exist in the current content config
                    var field = _mainPageContentRepository.FirstOrDefault(a => a.Key == themeRequiredField.Key);

                    if (field == null)
                    {
                        _mainPageContentRepository.InsertOrUpdateAndGetId(new MainPageContent()
                        {
                            Value = themeRequiredField.Value,
                            Key = themeRequiredField.Key,
                            ThemeReferenceId = theme.ThemeUniqueName,
                            ThemeReferenceName = theme.ThemeName,
                            IsStatic = true,
                            Type = themeRequiredField.Type
                        });
                    }
                }
            }
            else
            {
                foreach (var themeRequiredField in theme.ThemeRequiredProperties)
                {
                    //Checks if exist in the current content config
                    var field = _mainPageContentRepository.FirstOrDefault(a => a.Key == themeRequiredField.Key);

                    if (field == null)
                    {
                        _mainPageContentRepository.InsertOrUpdateAndGetId(new MainPageContent()
                        {
                            Value = themeRequiredField.Value,
                            Key = themeRequiredField.Key,
                            ThemeReferenceId = theme.ThemeUniqueName,
                            ThemeReferenceName = theme.ThemeName,
                            IsStatic = true,
                            Type = themeRequiredField.Type
                        });
                    }
                    else
                    {
                        field.Value = themeRequiredField.Value;
                    }
                }
            }
            CurrentUnitOfWork.SaveChanges();
        }
    }
}
