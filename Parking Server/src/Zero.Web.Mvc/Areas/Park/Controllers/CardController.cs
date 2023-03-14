using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.BackgroundJobs;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using DPS.Park.Application.Importing.Card;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Application.Shared.Importing.Card;
using DPS.Park.Application.Shared.Interface.Card;
using Microsoft.AspNetCore.Mvc;
using Zero.Authorization;
using Zero.Customize;
using Zero.Storage;
using Zero.Web.Areas.Park.Models.Card;
using Zero.Web.Controllers;

namespace Zero.Web.Areas.Park.Controllers
{
    [Area("Park")]
    [AbpMvcAuthorize(ParkPermissions.Card)]
    public class CardController : ZeroControllerBase
    {
        private readonly ICardAppService _cardAppService;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public CardController(ICardAppService cardAppService, IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _cardAppService = cardAppService;
            _binaryObjectManager = binaryObjectManager;
            _backgroundJobManager = backgroundJobManager;
        }

        public ActionResult Index()
        {
            var viewModel = new CardViewModel
            {
                FilterText = ""
            };
            return View(viewModel);
        }

        [AbpMvcAuthorize(ParkPermissions.Card_Create, ParkPermissions.Card_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCardForEditOutput getCardForEditOutput;

            if (id.HasValue)
            {
                getCardForEditOutput = await _cardAppService.GetCardForEdit(new EntityDto {Id = (int) id});
            }
            else
            {
                getCardForEditOutput = new GetCardForEditOutput
                {
                    Card = new CreateOrEditCardDto()
                    {
                        Code = StringHelper.ShortIdentity(),
                        IsActive = true
                    }
                };
            }

            var viewModel = new CreateOrEditCardViewModel()
            {
                Card = getCardForEditOutput.Card,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [HttpPost]
        [AbpMvcAuthorize(ParkPermissions.Card_Create)]
        public async Task<JsonResult> ImportFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();

                if (file == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                if (file.Length > 1048576 * 100) //100 MB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                byte[] fileBytes;
                await using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var tenantId = AbpSession.TenantId;
                var fileObject = new BinaryObject(tenantId, fileBytes);

                await _binaryObjectManager.SaveAsync(fileObject);

                await _backgroundJobManager
                    .EnqueueAsync<ImportCardFromExcelJob, ImportCardFromExcelJobArgs>(
                        new ImportCardFromExcelJobArgs
                        {
                            TenantId = tenantId,
                            BinaryObjectId = fileObject.Id,
                            User = AbpSession.ToUserIdentifier(),
                            Lang = CultureInfo.CurrentUICulture.Name
                        }
                    );

                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [AbpMvcAuthorize(ParkPermissions.Card_Create)]
        public ActionResult ExportTemplate()
        {
            var path = $"{GlobalConfig.ImportSampleFolders}{ZeroImportConsts.Card}";
            var stream = System.IO.File.OpenRead(path);
            return new FileStreamResult(stream, System.Net.Mime.MediaTypeNames.Application.Octet)
                {FileDownloadName = $"{GlobalConfig.ImportSamplePrefix}{ZeroImportConsts.Card}"};
        }
    }
}