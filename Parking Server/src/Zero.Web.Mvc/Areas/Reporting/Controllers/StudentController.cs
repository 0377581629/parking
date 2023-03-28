using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Zero.Web.Areas.Reporting.Models;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Reporting.Controllers
{
    [Area("Reporting")]
    [AbpAuthorize]
    public class StudentController : ZeroControllerBase
    {
        private readonly ISettingManager _settingManager;

        public StudentController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<IActionResult> StudentRenewCardReport()
        {
            var model = new ViewReportModel()
            {
                ReportName = "StudentRenewCardReport",
                ServiceName = "studentReporting",
                EndPoint = "studentRenewCardReport",
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Now,
                ReportLeftHeader = await _settingManager.GetSettingValueAsync(ZeroConst.LeftReportHeaderConfigKey),
                ReportRightHeader = await _settingManager.GetSettingValueAsync(ZeroConst.RightReportHeaderConfigKey)
            };
            return View(model);
        }
    }
}