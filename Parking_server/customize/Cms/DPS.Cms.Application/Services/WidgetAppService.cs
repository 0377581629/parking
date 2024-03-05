using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Cms.Application.Shared.Dto.Widget;
using DPS.Cms.Application.Shared.Interfaces;
using DPS.Cms.Core.Widget;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace DPS.Cms.Application.Services
{
    [AbpAuthorize(CmsPermissions.Widget)]
    public class WidgetAppService : ZeroAppServiceBase, IWidgetAppService
    {
        private readonly IRepository<Widget> _widgetRepository;

        public WidgetAppService(IRepository<Widget> widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        private async Task<IQueryable<WidgetDto>> WidgetQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from o in _widgetRepository.GetAll()
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        e => EF.Functions.Like(e.Name, $"%{input.Filter}%"))
                    .WhereIf(id.HasValue, e => e.Id == id.Value)
                select new WidgetDto
                {
                    Id = o.Id,
                    Numbering = o.Numbering,
                    Code = o.Code,
                    Name = o.Name,
                    Note = o.Note,
                    Order = o.Order,
                    IsDefault = o.IsDefault,
                    IsActive = o.IsActive,

                    About = o.About,
                    ActionName = o.ActionName,
                    ControllerName = o.ControllerName,
                    JsBundleUrl = o.JsBundleUrl,
                    JsScript = o.JsScript,
                    JsPlain = o.JsPlain,
                    CssBundleUrl = o.CssBundleUrl,
                    CssScript = o.CssScript,
                    CssPlain = o.CssPlain,
                    ContentType = o.ContentType,
                    ContentCount = o.ContentCount,
                    AsyncLoad = o.AsyncLoad,
                };

            return query;
        }

        private class QueryInput
        {
            public GetAllWidgetInput Input { get; set; }
            public int? Id { get; set; }
        }

        public async Task<PagedResultDto<GetWidgetForViewDto>> GetAll(GetAllWidgetInput input)
        {
            var queryInput = new QueryInput
            {
                Input = input
            };

            var objQuery = await WidgetQuery(queryInput);

            var pagedAndFilteredObjs = objQuery
                .OrderBy(input.Sorting ?? "order asc")
                .PageBy(input);

            var objs = from o in pagedAndFilteredObjs
                select new GetWidgetForViewDto
                {
                    Widget = o
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetWidgetForViewDto>(
                totalCount,
                res
            );
        }

        [AbpAuthorize(CmsPermissions.Widget_Edit)]
        public async Task<GetWidgetForEditOutput> GetWidgetForEdit(EntityDto input)
        {
            var queryInput = new QueryInput
            {
                Id = input.Id
            };

            var objQuery = await WidgetQuery(queryInput);

            var obj = await objQuery.FirstOrDefaultAsync();

            var output = new GetWidgetForEditOutput
            {
                Widget = ObjectMapper.Map<CreateOrEditWidgetDto>(obj)
            };

            return output;
        }

        private async Task ValidateDataInput(CreateOrEditWidgetDto input)
        {
            var res = await _widgetRepository.GetAll()
                .Where(o => !o.IsDeleted && o.Code.Equals(input.Code))
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task CreateOrEdit(CreateOrEditWidgetDto input)
        {
            input.Code = input.Code.Replace(" ", "");

            await ValidateDataInput(input);
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(CmsPermissions.Widget_Create)]
        protected virtual async Task Create(CreateOrEditWidgetDto input)
        {
            var obj = ObjectMapper.Map<Widget>(input);
            await _widgetRepository.InsertAndGetIdAsync(obj);
            if (obj.IsDefault)
            {
                var otherObjs = await _widgetRepository.GetAllListAsync(o => o.Id != obj.Id);
                if (otherObjs.Any())
                {
                    foreach (var changeDefault in otherObjs)
                    {
                        changeDefault.IsDefault = false;
                    }
                }
            }
        }

        [AbpAuthorize(CmsPermissions.Widget_Edit)]
        protected virtual async Task Update(CreateOrEditWidgetDto input)
        {
            if (input.Id.HasValue)
            {
                var obj = await _widgetRepository.FirstOrDefaultAsync(o => o.Id == (int) input.Id);

                if (obj == null)
                    throw new UserFriendlyException(L("NotFound"));

                ObjectMapper.Map(input, obj);
                if (obj.IsDefault)
                {
                    var otherObjs = await _widgetRepository.GetAllListAsync(o => o.Id != obj.Id);
                    if (otherObjs.Any())
                    {
                        foreach (var changeDefault in otherObjs)
                        {
                            changeDefault.IsDefault = false;
                        }
                    }
                }

                await _widgetRepository.UpdateAsync(obj);
            }
        }

        [AbpAuthorize(CmsPermissions.Widget_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _widgetRepository.FirstOrDefaultAsync(o => o.Id == input.Id);
            if (obj == null)
                throw new UserFriendlyException(L("NotFound"));
            await _widgetRepository.DeleteAsync(obj.Id);
        }
    }
}