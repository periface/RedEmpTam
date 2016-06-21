﻿using MercadoCinotam.EntityFramework;
using EntityFramework.DynamicFilters;

namespace MercadoCinotam.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly MercadoCinotamDbContext _context;

        public InitialHostDbBuilder(MercadoCinotamDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
