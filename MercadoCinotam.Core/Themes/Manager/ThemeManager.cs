using Abp.Domain.Repositories;
using MercadoCinotam.Themes.Entities;
using System;
using System.Collections.Generic;

namespace MercadoCinotam.Themes.Manager
{
    public class ThemeManager : IThemeManager
    {
        private readonly IRepository<Theme> _themeRepository;
        private readonly IRepository<ThemePreview> _themePreviewRepository;
        public ThemeManager(IRepository<Theme> themeRepository, IRepository<ThemePreview> themePreviewRepository)
        {
            _themeRepository = themeRepository;
            _themePreviewRepository = themePreviewRepository;
        }

        public int SaveTheme(Theme theme)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Theme> GetThemes()
        {
            return _themeRepository.GetAllList();
        }

        public Theme GetTheme(int themeId)
        {
            return _themeRepository.FirstOrDefault(a => a.Id == themeId);
        }

        public IEnumerable<ThemePreview> GetThemePreviews(int themeId)
        {
            var previews = _themePreviewRepository.GetAllList(a => a.Theme.Id == themeId);
            return previews;
        }

        public Theme GetTheme(string activeThemeName)
        {
            return _themeRepository.FirstOrDefault(a => a.ThemeUniqueName.ToUpper() == activeThemeName.ToUpper());
        }
    }
}
