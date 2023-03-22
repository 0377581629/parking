namespace DPS.Reporting.Application.Shared.Dto.Resident
{
    public class ResidentPaidReportingOutput
    {
        public string ApartmentNumber { get; set; }
        public string OwnerFullName { get; set; }
        public int MoneyToPay { get; set; }
        public string IsPaid { get; set; }
    }
}