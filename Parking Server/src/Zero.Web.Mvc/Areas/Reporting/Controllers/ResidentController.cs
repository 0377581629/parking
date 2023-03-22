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
    public class ResidentController : ZeroControllerBase
    {
        private readonly ISettingManager _settingManager;

        public ResidentController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        private const string ReportServiceName = "residentReporting";

        public async Task<IActionResult> ResidentPaidReport()
        {
            var model = new ViewReportModel()
            {
                ReportName = "ResidentPaidReport",
                ServiceName = ReportServiceName,
                EndPoint = "residentPaidReport",
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Now,
                ReportLeftHeader = await _settingManager.GetSettingValueAsync(ZeroConst.LeftReportHeaderConfigKey),
                ReportRightHeader = await _settingManager.GetSettingValueAsync(ZeroConst.RightReportHeaderConfigKey)
            };
            return View(model);
        }
    }
}