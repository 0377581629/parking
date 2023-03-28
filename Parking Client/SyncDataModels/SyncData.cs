using System;
using System.Collections.Generic;

namespace SyncDataModels
{
    public class SyncClientDto
    {
        public List<StudentData> ListStudentData { get; set; }

        public List<SecurityData> ListSecurityData { get; set; }
    }

    public class StudentData
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string AvatarBase64 { get; set; }

        public bool Male { get; set; }

        public string DoBStr { get; set; }

        public string CardNumber { get; set; }

        public string CardDateStr { get; set; }

        public DateTime? CardDate { get; set; }

        public string ClassName { get; set; }
    }

    public class SecurityData
    {
        public long Id { get; set; }
        public int StudentId { get; set; }

        public string CardNumber { get; set; }

        public int ParentStatus { get; set; }

        public string CamCode { get; set; }

        public string SecurityDateStr { get; set; }

        public DateTime SecurityDate { get; set; }

        public string PhotoBase64 { get; set; }

        public string PhotoUrl { get; set; }
    }
}