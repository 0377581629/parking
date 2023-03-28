using Abp.Application.Services.Dto;

namespace DPS.Park.Application.Shared.Dto.Student.StudentCard
{
    public class StudentCardDto: EntityDto<int?>
    {
        public int? TenantId { get; set; }
        
        public int StudentId { get; set; }
        
        public string StudentCode { get; set; }
        
        public string StudentName { get; set; }
        
        public string StudentEmail { get; set; }
        
        public string StudentPhoneNumber { get; set; }
        
        public int CardId { get; set; }
        
        public string CardCode { get; set; }
        
        public string CardNumber { get; set; }
        
        public string Note { get; set; }
    }
}