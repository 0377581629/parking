using System.ComponentModel;
using Zero.Extensions;

namespace DPS.Park.Application.Shared.Importing.Card
{
    public class ImportCardDto
    {
        public int CardId { get; set; }
        
        public int CardTypeId { get; set; }
        
        public int VehicleTypeId { get; set; }
        
        [DisplayName("A")]
        [InvalidExport]
        public string CardTypeName { get; set; }
        
        [DisplayName("B")]
        [InvalidExport]
        public string VehicleTypeName { get; set; }
        
        [DisplayName("C")]
        [InvalidExport]
        public string Code { get; set; }
        
        [DisplayName("D")]
        [InvalidExport]
        public string CardNumber { get; set; }
        
        [DisplayName("E")]
        [InvalidExport]
        public int Balance { get; set; }
        
        [DisplayName("F")]
        [InvalidExport]
        public string LicensePlate { get; set; }
        
        [DisplayName("G")]
        [InvalidExport]
        public string Note { get; set; }

        [InvalidExport]
        public string Exception { get; set; }
    }
}