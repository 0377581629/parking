using System.Collections.Generic;
using System.Threading.Tasks;
using DPS.Park.Application.Shared.Dto.Card.Card;
using Zero.Dto;

namespace DPS.Park.Application.Exporting.Card
{
    public interface ICardExcelExporter
    {
        Task<FileDto> ExportToFile(List<CardDto> cards);
    }
}