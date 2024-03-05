using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Threading;
using DPS.Park.Application.Shared.Importing.Card;
using DPS.Park.Core.Card;
using DPS.Park.Core.Vehicle;
using NUglify.Helpers;
using Z.EntityFramework.Extensions;
using Zero;
using Zero.Customize.DataExporting;
using Zero.Customize.DataImporting;
using Zero.Importing;
using Zero.Notifications;
using Zero.Storage;

namespace DPS.Park.Application.Importing.Card
{
    public class ImportCardFromExcelJob : BackgroundJob<ImportCardFromExcelJobArgs>, ITransientDependency
    {
        #region Constructor

        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private readonly IRepository<CardType> _cardTypeRepository;
        private readonly IRepository<Core.Card.Card> _cardRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        private const string NotifyPrefix = "Import_TheXe : ";

        public ImportCardFromExcelJob(
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager,
            IRepository<CardType> cardTypeRepository,
            IRepository<Core.Card.Card> cardRepository,
            IRepository<VehicleType> vehicleTypeRepository)
        {
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
            _cardTypeRepository = cardTypeRepository;
            _cardRepository = cardRepository;
            _vehicleTypeRepository = vehicleTypeRepository;

            LocalizationSourceName = ZeroConst.LocalizationSourceName;
        }

        #endregion

        public override void Execute(ImportCardFromExcelJobArgs args)
        {
            if (!string.IsNullOrEmpty(args.Lang))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(args.Lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(args.Lang);
            }

            SendNotification(args, ZeroEnums.ImportProcess.StartReadFile);

            var benchMark = Stopwatch.StartNew();
            var lstObj = GetObjListFromExcelOrNull(args);
            if (lstObj == null || !lstObj.Any())
            {
                return;
            }

            benchMark.Stop();
            Logger.Warn($"{NotifyPrefix} - GetObjListFromExcelOrNull - {benchMark.Elapsed.ToString()}");
            SendNotification(args, ZeroEnums.ImportProcess.EndReadFile);
            SendNotification(args, ZeroEnums.ImportProcess.Start);
            Import(args, lstObj);
        }

        private List<ImportCardDto> GetObjListFromExcelOrNull(ImportFromExcelJobArgs args)
        {
            using var uow = UnitOfWorkManager.Begin();
            using (CurrentUnitOfWork.SetTenantId(args.TenantId))
            {
                try
                {
                    var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                    var reader = new ListExcelDataReader<ImportCardDto>(Logger);
                    return reader.GetFromExcel(file.Bytes);
                }
                catch (Exception err)
                {
                    Logger.Error($"{NotifyPrefix} - GetObjListFromExcelOrNull", err);
                    return null;
                }
                finally
                {
                    uow.Complete();
                }
            }
        }

        private async Task<List<ImportCardDto>> Validate(int tenantId,
            IReadOnlyList<ImportCardDto> objs)
        {
            var lstValidated = new List<ImportCardDto>();
            var notEmptyPrefix = L("NotEmpty");

            foreach (var itm in objs)
            {
                if (string.IsNullOrEmpty(itm.Exception)) itm.Exception = "";
                var lstEmpty = new List<string>();

                if (string.IsNullOrEmpty(itm.CardTypeName))
                    lstEmpty.Add(L("CardTypeName"));
                if (string.IsNullOrEmpty(itm.VehicleTypeName))
                    lstEmpty.Add(L("VehicleTypeName"));
                if (string.IsNullOrEmpty(itm.Code))
                    lstEmpty.Add(L("Code"));
                if (string.IsNullOrEmpty(itm.CardNumber))
                    lstEmpty.Add(L("CardNumber"));

                if (!lstEmpty.Any()) continue;

                itm.Exception += $"{notEmptyPrefix} {string.Join(" ", lstEmpty)}";

                lstValidated.Add(itm);
            }

            return lstValidated;
        }

        private void Import(ImportCardFromExcelJobArgs args,
            IReadOnlyList<ImportCardDto> impObjs)
        {
            EntityFrameworkManager.ContextFactory = _ => _cardRepository.GetDbContext();
            using var uow = UnitOfWorkManager.Begin();

            using (CurrentUnitOfWork.SetTenantId(args.TenantId))
            {
                try
                {
                    AsyncHelper.RunSync(async () =>
                        await Create(impObjs, args)
                    );
                }
                catch (Exception e)
                {
                    SendNotification(args, ZeroEnums.ImportProcess.Fail);
                    Logger.Error($"{NotifyPrefix} - Import", e);
                }
                finally
                {
                    uow.Complete();
                }
            }
        }

        private async Task Create(IReadOnlyList<ImportCardDto> lstObj,
            ImportCardFromExcelJobArgs args)
        {
            try
            {
                if (lstObj != null && lstObj.Any())
                {
                    var lstInvalid = await Validate(args.TenantId ?? 0, lstObj);
                    if (!lstInvalid.Any())
                    {
                        var tenantId = args.TenantId;
                        var lstReject = new List<ImportCardDto>();
                        var excelRowCount = 0;

                        var lstCard =
                            await _cardRepository.GetAllListAsync(o =>
                                !o.IsDeleted && o.TenantId == args.TenantId);
                        var lstCardType =
                            await _cardTypeRepository.GetAllListAsync(o =>
                                !o.IsDeleted && o.TenantId == args.TenantId);
                        var lstVehicleType =
                            await _vehicleTypeRepository.GetAllListAsync(o =>
                                !o.IsDeleted && o.TenantId == args.TenantId);

                        foreach (var obj in lstObj)
                        {
                            excelRowCount++;
                            var cnt = 0;

                            if (lstCard.Any(o =>
                                    o.Code.Equals(obj.Code, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                obj.Exception += $"{L("ExistedData")} : {obj.Code} ;";
                                cnt++;
                            }

                            if (!lstCardType.Any(o =>
                                    o.Name.Equals(obj.CardTypeName,
                                        StringComparison.InvariantCultureIgnoreCase)))
                            {
                                obj.Exception +=
                                    $"{L("UnExistedCardType")} : {obj.CardTypeName} ;";
                                cnt++;
                            }

                            if (!lstVehicleType.Any(o =>
                                    o.Name.Equals(obj.VehicleTypeName,
                                        StringComparison.InvariantCultureIgnoreCase)))
                            {
                                obj.Exception +=
                                    $"{L("UnExistedVehicleType")} : {obj.VehicleTypeName} ;";
                                cnt++;
                            }

                            if (cnt > 0)
                            {
                                lstReject.Add(obj);
                            }
                        }

                        if (!lstReject.Any())
                        {
                            var lstNewCard = new List<Core.Card.Card>();
                            foreach (var obj in lstObj)
                            {
                                if (lstCardType.Any(o =>
                                        o.Name.Equals(obj.CardTypeName,
                                            StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    obj.CardTypeId = lstCardType.First(o =>
                                        o.Name.Equals(obj.CardTypeName,
                                            StringComparison.InvariantCultureIgnoreCase)).Id;
                                }

                                if (lstVehicleType.Any(o =>
                                        o.Name.Equals(obj.VehicleTypeName,
                                            StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    obj.VehicleTypeId = lstVehicleType.First(o =>
                                        o.Name.Equals(obj.VehicleTypeName,
                                            StringComparison.InvariantCultureIgnoreCase)).Id;
                                }

                                lstNewCard.Add(new Core.Card.Card
                                {
                                    TenantId = args.TenantId,
                                    CardTypeId = obj.CardTypeId,
                                    VehicleTypeId = obj.VehicleTypeId,
                                    Code = obj.Code,
                                    CardNumber = obj.CardNumber,
                                    Balance = obj.Balance,
                                    LicensePlate = obj.LicensePlate,
                                    Note = obj.Note,
                                    IsActive = true
                                });
                            }

                            if (lstNewCard.Any())
                            {
                                lstNewCard = lstNewCard.DistinctBy(o => o.Code).ToList();
                                if (excelRowCount != lstNewCard.Count)
                                {
                                    SendNotification(args, ZeroEnums.ImportProcess.ExcelHasDuplicateObjs);
                                    return;
                                }

                                await _cardRepository.GetDbContext()
                                    .BulkInsertAsync(lstNewCard);
                            }

                            SendNotification(args, ZeroEnums.ImportProcess.Success);
                        }
                        else
                        {
                            SendNotification(args, ZeroEnums.ImportProcess.HasInvalidObjs);
                            var invalidExporter =
                                new InvalidExporter<ImportCardDto>(_tempFileCacheManager, LocalizationManager);
                            var file = invalidExporter.ExportToFile(lstReject, "Loi_Import_TheXe");
                            await _appNotifier.SomeObjectCouldntBeImported(args.User, file.FileToken, file.FileType,
                                file.FileName);
                        }
                    }
                    else
                    {
                        SendNotification(args, ZeroEnums.ImportProcess.HasInvalidObjs);
                        var invalidExporter =
                            new InvalidExporter<ImportCardDto>(_tempFileCacheManager, LocalizationManager);
                        var file = invalidExporter.ExportToFile(lstInvalid, "Loi_Import_TheXe");
                        await _appNotifier.SomeObjectCouldntBeImported(args.User, file.FileToken, file.FileType,
                            file.FileName);
                    }
                }
            }
            catch (Exception e)
            {
                SendNotification(args, ZeroEnums.ImportProcess.Fail);
                Logger.Error($"{NotifyPrefix} - Create ", e);
            }
        }

        private void SendNotification(ImportFromExcelJobArgs args, ZeroEnums.ImportProcess process, string message = "")
        {
            using var uow = UnitOfWorkManager.Begin();
            using (CurrentUnitOfWork.SetTenantId(args.TenantId))
            {
                switch (process)
                {
                    case ZeroEnums.ImportProcess.Start:
                        AsyncHelper.RunSync(async () => await _appNotifier.SendMessageAsync(
                                args.User,
                                $"{NotifyPrefix} {L("ImportProcessStart")}"
                            )
                        );
                        break;
                    case ZeroEnums.ImportProcess.Success:

                        AsyncHelper.RunSync(async () => await _appNotifier.SendMessageAsync(
                            args.User,
                            $"{NotifyPrefix} {L("ImportFromFileSuccess")} {message}",
                            Abp.Notifications.NotificationSeverity.Success)
                        );
                        break;
                    case ZeroEnums.ImportProcess.Fail:
                        AsyncHelper.RunSync(async () => await _appNotifier.SendMessageAsync(
                            args.User,
                            $"{NotifyPrefix} {L("ImportFromFileFail")} {message}",
                            Abp.Notifications.NotificationSeverity.Error)
                        );
                        break;
                    case ZeroEnums.ImportProcess.HasInvalidObjs:
                        AsyncHelper.RunSync(async () => await _appNotifier.SendMessageAsync(
                                args.User,
                                $"{NotifyPrefix} {L("HasInvalidObjs")} {message}",
                                Abp.Notifications.NotificationSeverity.Error
                            )
                        );
                        break;
                    case ZeroEnums.ImportProcess.StartReadFile:
                        break;
                    case ZeroEnums.ImportProcess.EndReadFile:
                        break;
                    case ZeroEnums.ImportProcess.Empty:
                        break;
                    case ZeroEnums.ImportProcess.ExcelHasDuplicateObjs:
                        AsyncHelper.RunSync(async () => await _appNotifier.SendMessageAsync(
                                args.User,
                                $"{NotifyPrefix} {L("ExcelHasDuplicateObjs")} {message}",
                                Abp.Notifications.NotificationSeverity.Error
                            )
                        );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(process), process, null);
                }
            }

            uow.Complete();
        }
    }
}