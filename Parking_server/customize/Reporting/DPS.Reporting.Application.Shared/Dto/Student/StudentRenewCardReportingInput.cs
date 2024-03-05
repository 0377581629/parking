using System;

namespace DPS.Reporting.Application.Shared.Dto.Student
{
    public class StudentRenewCardReportingInput
    {
        public string Filter { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int? CardTypeId { get; set; }
        
        public int? VehicleTypeId { get; set; }
    }
}