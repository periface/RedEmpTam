﻿using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Helpers.GenericTypes;
using Helpers.ReflectionHelpers;
using Helpers.TenancyHelpers;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoCinotam
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MercadoCinotamAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected MercadoCinotamAppServiceBase()
        {
            LocalizationSourceName = MercadoCinotamConsts.LocalizationSourceName;
        }
        public static IQueryable<T> GetOrderedQuery<T>(IQueryable<T> elements, RequestModel requestModel)
        {
            return requestModel.PropOrd.ToUpper() == "ASC" ? elements.OrderBy(requestModel.PropToSort) : elements.OrderByDescending(requestModel.PropToSort);
        }
        /// <summary>
        /// Generates a readyforuse datatable.js model
        /// </summary>
        /// <typeparam name="TQ"></typeparam>
        /// <param name="request">Request model from datatables.js</param>
        /// <param name="queryable">Query of the entity</param>
        /// <param name="defaultOrderableProp">A default orderable property (if the method doesnt find the requested prop use this has callback)</param>
        /// <param name="totalCount"></param>
        /// <returns>ListOfTQ</returns>
        public List<TQ> GenerateModel<TQ>(RequestModel request, IQueryable<TQ> queryable, string defaultOrderableProp, out int totalCount)
        {
            var pageIndex = request.start;
            var pageSize = request.length;
            var searchString = request.search["value"].ToUpper();
            totalCount = queryable.Count();
            if (pageSize == -1)
            {
                pageIndex = 0;
                request.length = totalCount;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                if (request.SearchInAll)
                {
                    queryable.Where(searchString);
                }
                else
                {
                    queryable = string.IsNullOrEmpty(request.PropToSearch) ? queryable.Where(searchString) : queryable.Where(request.PropToSearch, searchString);

                }
            }
            queryable = !string.IsNullOrEmpty(request.PropToSort) ? GetOrderedQuery(queryable, request) : queryable.OrderBy(defaultOrderableProp);
            var filteredByLength = queryable.Skip(pageIndex).Take(request.length).ToList();
            return filteredByLength;
        }
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public int TenantId
        {
            get
            {
                if (TenantHelper.TenantId == null && AbpSession.TenantId.HasValue)
                {
                    return AbpSession.TenantId.Value;
                }
                if (TenantHelper.TenantId != null)
                {
                    return (int)TenantHelper.TenantId;
                }
                throw new UserFriendlyException();
            }
        }
    }
}