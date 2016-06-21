﻿using Abp.Zero.EntityFramework;
using MercadoCinotam.Authorization.Roles;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.Users;
using System.Data.Common;

namespace MercadoCinotam.EntityFramework
{
    public class MercadoCinotamDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MercadoCinotamDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MercadoCinotamDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MercadoCinotamDbContext since ABP automatically handles it.
         */
        public MercadoCinotamDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MercadoCinotamDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
