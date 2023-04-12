﻿using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using DPS.Cms.Application.Shared.Dto.Menu;
using DPS.Cms.Core.Menu;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Web.Areas.Cms.Models.Menu;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    [AbpMvcAuthorize(CmsPermissions.Menu)]
    public class MenuController : ZeroControllerBase
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IFileAppService _fileAppService;

        public MenuController(IRepository<Menu> menuRepository, IFileAppService fileAppService)
        {
            _menuRepository = menuRepository;
            _fileAppService = fileAppService;
        }

        public ActionResult Index()
        {
            var model = new MenuViewModel();
            return View(model);
        }

        [AbpMvcAuthorize(CmsPermissions.Menu_Create, CmsPermissions.Menu_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(CreateOrEditMenuInput input)
        {
            var model = new CreateOrEditMenuViewModel(null)
            {
                Code = StringHelper.ShortIdentity(),
                MenuGroupId = input.MenuGroupId
            };

            if (input.Id.HasValue)
            {
                var obj = await _menuRepository.GetAsync(input.Id.Value);
                model = ObjectMapper.Map<CreateOrEditMenuViewModel>(obj);
            }

            return PartialView("_CreateOrEditModal", model);
        }
    }
}