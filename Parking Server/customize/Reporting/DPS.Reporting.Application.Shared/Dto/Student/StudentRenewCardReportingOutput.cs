namespace DPS.Reporting.Application.Shared.Dto.Student
{
    public class StudentRenewCardReportingOutput
    {
        public string StudentCode { get; set; }
        
        public string StudentName { get; set; }
      
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        
        public string VehicleType { get; set; }
        
        public string RenewCardTime { get; set; }
    }
}