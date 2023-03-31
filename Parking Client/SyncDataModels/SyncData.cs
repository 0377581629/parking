using System;
using System.Collections.Generic;

namespace SyncDataModels
{
    public class SyncClientDto
    {
        public List<SyncStudentDataDto> ListStudentData { get; set; }

        public List<SyncSecurityDataDto> ListSecurityData { get; set; }
    }

    public class SyncStudentDataDto
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        
        public string AvatarBase64 { get; set; }

        public bool Male { get; set; }

        public string DoBStr { get; set; }

        public string CardNumber { get; set; }
    }

    public class SyncSecurityDataDto
    {
        public long Id { get; set; }
        public int StudentId { get; set; }

        public string CardNumber { get; set; }

        public int ParentStatus { get; set; }

        public string SecurityDateStr { get; set; }

        public DateTime SecurityDate { get; set; }

        public string PhotoBase64 { get; set; }

        public string PhotoUrl { get; set; }
    }
}