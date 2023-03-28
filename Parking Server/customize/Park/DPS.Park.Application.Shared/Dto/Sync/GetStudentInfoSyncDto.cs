namespace DPS.Park.Application.Shared.Dto.Sync
{
    public class GetStudentActiveInfoSyncDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AvatarBase64 { get; set; }
        public int Male { get; set; }
        public string DoBStr { get; set; }
        public string CardNumber { get; set; }
    }
}