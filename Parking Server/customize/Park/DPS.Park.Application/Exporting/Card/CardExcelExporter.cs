using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using DPS.Park.Application.Shared.Dto.Card.Card;
using DPS.Park.Core.Card;
using DPS.Park.Core.Vehicle;
using Microsoft.EntityFrameworkCore;
using Zero.DataExporting.Excel.NPOI;
using Zero.Dto;
using Zero.Storage;

namespace DPS.Park.Application.Exporting.Card
{
    public class CardExcelExporter : NpoiExcelExporterBase, ICardExcelExporter
    {
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Core.Card.Card> _cardRepository;
        private readonly IRepository<CardType> _cardTypeRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public CardExcelExporter(IAbpSession abpSession, IRepository<Core.Card.Card> cardRepository,
            ITempFileCacheManager tempFileCacheManager, IRepository<CardType> cardTypeRepository,
            IRepository<VehicleType> vehicleTypeRepository) : base(tempFileCacheManager)
        {
            _abpSession = abpSession;
            _cardRepository = cardRepository;
            _cardTypeRepository = cardTypeRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<FileDto> ExportToFile(List<CardDto> cards)
        {
            return CreateExcelPackage(
                "DanhSach_TheXe.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("Card"));

                    AddHeader(
                        sheet,
                        L("CardType"),
                        L("VehicleType"),
                        L("Code"),
                        L("CardNumber"),
                        L("Balance"),
                        L("LicensePlate"),
                        L("Note")
                    );

                    AddObjects(
                        sheet, 2, cards,
                        _ => _.CardTypeName,
                        _ => _.VehicleTypeName,
                        _ => _.Code,
                        _ => _.CardNumber,
                        _ => _.Balance,
                        _ => _.LicensePlate,
                        _ => _.Note
                    );

                    for (var i = 0; i < 9; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}